namespace PortiaNet.Helper.Ai.Vision;

public class VisionProcessingOptions
{
    public bool EnablePreprocessing { get; set; } = true;

    public double PreprocessingStrength { get; set; } = 0.5; // 0 = fast, 1 = max quality

    public bool EnableRotationCorrection { get; set; } = true;

    public bool EnableSkewCorrection { get; set; } = true;

    public string OcrModelPath { get; set; } = "Ai/Models/ocr.onnx";
}