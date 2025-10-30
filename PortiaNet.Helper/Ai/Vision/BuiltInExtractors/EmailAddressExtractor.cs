using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Linq;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class EmailAddressExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.EmailAddress;

    private static InferenceSession? _ocrSession;
    private static string? _lastModelPath;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
    {
        try
        {
            EnsureOcrSession(options.OcrModelPath);

            // Example: Model expects 48x320
            const int modelHeight = 48;
            const int modelWidth = 320; // Replace with your model's expected width

            // 1. Convert image bytes to Bitmap
            using var ms = new MemoryStream(imageBytes.ToArray());
            using var bitmap = new Bitmap(ms);
            bitmap.Save(@"D:\Sample Images\00 - Original Image.bmp");
            // 2. Resize to model input size
            using var resized = ResizeBitmap(bitmap, modelWidth, modelHeight);
            resized.Save(@"D:\Sample Images\01 - Resized Image.bmp");

            // 3. Preprocess (optional, or use resized directly)
            var preprocessed = PreprocessImage(resized);
            preprocessed.Save(@"D:\Sample Images\02 - Grayscale Image.bmp");

            // 4. Run OCR model
            var extractedText = RunOcr(preprocessed);

            // 5. Use regex to find email addresses
            var email = ExtractEmail(extractedText);

            if (email == null)
                return null;

            return new CardFieldExtraction
            {
                Value = email,
                Confidence = 0.95 // You may adjust this based on OCR confidence if available
            };
        }
        catch (Exception ex)
        {
            throw new FieldExtractionException(FieldType, "Failed to extract email address.", ex);
        }
    }

    private static void EnsureOcrSession(string modelPath)
    {
        if (_ocrSession != null && _lastModelPath == modelPath) return;
        if (!File.Exists(modelPath))
            throw new InvalidOperationException($"OCR model not found at '{modelPath}'.");
        _ocrSession = new InferenceSession(modelPath);
        _lastModelPath = modelPath;

        // DEBUG: List input names
        //var inputNames = _ocrSession.InputMetadata.Keys.ToList();
        //Console.WriteLine("ONNX Model Input Names: " + string.Join(", ", inputNames));
    }

    private static Bitmap PreprocessImage(Bitmap bitmap)
    {
        // Convert to grayscale (improve OCR accuracy)
        var gray = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
        using var g = Graphics.FromImage(gray);
        var colorMatrix = new ColorMatrix(
            [
                [0.3f, 0.3f, 0.3f, 0, 0],
                [0.59f, 0.59f, 0.59f, 0, 0],
                [0.11f, 0.11f, 0.11f, 0, 0],
                [0, 0, 0, 1, 0],
                [0, 0, 0, 0, 1]
            ]);
        var attributes = new ImageAttributes();
        attributes.SetColorMatrix(colorMatrix);
        g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);

        return gray;
    }

    private static Bitmap ResizeBitmap(Bitmap src, int width, int height)
    {
        var dest = new Bitmap(width, height);
        using var g = Graphics.FromImage(dest);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.DrawImage(src, 0, 0, width, height);
        return dest;
    }

    private static readonly string[] LabelMap =
    [
        " ", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        "@", ".", "_", "-", "+", "/", ":", ",", ";", "(", ")", "[", "]", "{", "}", "<", ">", "!", "?", "#", "$", "%", "&", "*", "'", "\"", "\\", "|", "^", "~", "`", "="
        // Add more as needed, and a blank token if your model uses CTC (often at index 0)
    ];

    private static string RunOcr_old(Bitmap bitmap)
    {
        // Convert Bitmap to tensor (depends on your ONNX model's input requirements)
        // This is a placeholder; you must adapt it to your model's expected input
        // For example, you may need to resize, normalize, and convert to float32

        // Example: Convert to byte array (RGB)
        var inputTensor = ImageToTensor(bitmap);

        // Prepare input for ONNX
        var inputMeta = _ocrSession!.InputMetadata;
        var inputName = inputMeta.Keys.First();
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor(inputName, inputTensor)
        };

        //var dims = inputMeta[inputName].Dimensions; // e.g. [1, 3, 48, 320]

        // Run inference
        using var results = _ocrSession!.Run(inputs);

        var result = results.First();
        Console.WriteLine(result.Name);
        Console.WriteLine(result.Value.GetType());

        var output = results.First().AsEnumerable<string>().FirstOrDefault() ?? string.Empty;
        return output;
    }

    private static string RunOcr(Bitmap bitmap)
    {
        var inputTensor = ImageToTensor(bitmap);

        var inputMeta = _ocrSession!.InputMetadata;
        var inputName = inputMeta.Keys.First();
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor(inputName, inputTensor)
        };

        using var results = _ocrSession!.Run(inputs);
        var result = results.First();
        if (result.Value is DenseTensor<float> tensor)
        {
            // Assume shape [1, seqLen, numClasses] or [1, numClasses, seqLen]
            var dims = tensor.Dimensions.ToArray();
            int seqLen, numClasses;
            bool classesLast;

            if (dims.Length == 3)
            {
                // Try to detect layout
                if (dims[1] > dims[2])
                {
                    // [1, seqLen, numClasses]
                    seqLen = dims[1];
                    numClasses = dims[2];
                    classesLast = true;
                }
                else
                {
                    // [1, numClasses, seqLen]
                    numClasses = dims[1];
                    seqLen = dims[2];
                    classesLast = false;
                }
            }
            else
            {
                throw new InvalidOperationException("Unexpected tensor shape for OCR output.");
            }

            // CTC decoding: argmax over classes, collapse repeats, remove blanks (assume blank index 0)
            const int blankIndex = 0; // Adjust if your model uses a different blank index
            var sb = new System.Text.StringBuilder();
            var prevCharIdx = -1;

            for (var t = 0; t < seqLen; t++)
            {
                var maxIdx = -1;
                var maxVal = float.MinValue;
                for (var c = 0; c < numClasses; c++)
                {
                    var val = classesLast
                        ? tensor[0, t, c]
                        : tensor[0, c, t];
                    if (val > maxVal)
                    {
                        maxVal = val;
                        maxIdx = c;
                    }
                }
                // CTC: skip repeated and blank
                if (maxIdx != blankIndex && maxIdx != prevCharIdx)
                {
                    if (maxIdx >= 0 && maxIdx < LabelMap.Length)
                        sb.Append(LabelMap[maxIdx]);
                }
                prevCharIdx = maxIdx;
            }
            return sb.ToString();
        }
        throw new InvalidOperationException($"Unexpected OCR model output type: {result.Value.GetType()}");
    }

    private static DenseTensor<float> ImageToTensor(Bitmap bitmap)
    {
        // Example: Convert to [1, 3, H, W] tensor (RGB) with float values in [0,1]
        var width = bitmap.Width;
        var height = bitmap.Height;
        var tensor = new DenseTensor<float>(new[] { 1, 3, height, width });
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var color = bitmap.GetPixel(x, y);
                tensor[0, 0, y, x] = color.R / 255f;
                tensor[0, 1, y, x] = color.G / 255f;
                tensor[0, 2, y, x] = color.B / 255f;
            }
        }
        return tensor;
    }

    private static string? ExtractEmail(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        // Simple email regex
        var match = Regex.Match(text, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
        return match.Success ? match.Value : null;
    }
}