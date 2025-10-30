using PortiaNet.Helper.Ai.Vision;
using System.Text.RegularExpressions;

namespace PortiaNet.Helper.Ai.Vision;

public class VisionProcessor
{
    private readonly List<IFieldExtractor> _extractors = [];
    private readonly VisionProcessingOptions _options;

    public VisionProcessor(VisionProcessingOptions? options = null)
    {
        _options = options ?? new VisionProcessingOptions();

        // Register built-in extractors
        RegisterExtractor(new BuiltInExtractors.CardNumberExtractor());
        RegisterExtractor(new BuiltInExtractors.CardholderNameExtractor());
        RegisterExtractor(new BuiltInExtractors.ExpiryDateExtractor());
        RegisterExtractor(new BuiltInExtractors.CVVExtractor());
        RegisterExtractor(new BuiltInExtractors.CardBrandExtractor());
        RegisterExtractor(new BuiltInExtractors.BankNameExtractor());
        RegisterExtractor(new BuiltInExtractors.PhoneNumberExtractor());
        RegisterExtractor(new BuiltInExtractors.MobileNumberExtractor());
        RegisterExtractor(new BuiltInExtractors.WhatsAppNumberExtractor());
        RegisterExtractor(new BuiltInExtractors.InstagramPageExtractor());
        RegisterExtractor(new BuiltInExtractors.CompanyNameExtractor());
        RegisterExtractor(new BuiltInExtractors.PersonNameExtractor());
        RegisterExtractor(new BuiltInExtractors.PersonTitleExtractor());
        RegisterExtractor(new BuiltInExtractors.DesignationExtractor());
        RegisterExtractor(new BuiltInExtractors.QRCodeDataExtractor());
        RegisterExtractor(new BuiltInExtractors.PhysicalAddressExtractor());
        RegisterExtractor(new BuiltInExtractors.LandlineExtractor());
        RegisterExtractor(new BuiltInExtractors.EmailAddressExtractor());
        RegisterExtractor(new BuiltInExtractors.WebsiteExtractor());
        RegisterExtractor(new BuiltInExtractors.BranchNameExtractor());
    }

    public void RegisterExtractor(IFieldExtractor extractor)
        => _extractors.Add(extractor);

    public CardExtractionResult ProcessCard(string frontBase64, string? backBase64 = null)
    {
        try
        {
            var frontBytes = ParseBase64Image(frontBase64);
            var backBytes = backBase64 != null ? ParseBase64Image(backBase64) : null;
            return ProcessCard(frontBytes, backBytes);
        }
        catch (Exception ex)
        {
            throw new VisionProcessingException("Failed to process Base64 image input.", ex);
        }
    }

    public CardExtractionResult ProcessCard(Stream frontStream, Stream? backStream = null)
    {
        try
        {
            var frontBytes = ReadAllBytes(frontStream);
            var backBytes = backStream != null ? ReadAllBytes(backStream) : null;
            return ProcessCard(frontBytes, backBytes);
        }
        catch (Exception ex)
        {
            throw new VisionProcessingException("Failed to process image stream input.", ex);
        }
    }

    public CardExtractionResult ProcessCard(byte[] frontBytes, byte[]? backBytes = null)
    {
        // Preprocessing, rotation/skew correction, etc. would be called here
        // For now, just a stub
        var result = new CardExtractionResult();

        foreach (var extractor in _extractors)
        {
            try
            {
                var field = extractor.Extract(frontBytes, _options);
                if (field != null)
                    result.Fields[extractor.FieldType] = field;
            }
            catch (FieldExtractionException fex)
            {
                // Log or collect extraction errors as needed
                result.Notes += $"[{extractor.FieldType}] {fex.Message}\n";
            }
            catch (Exception ex)
            {
                // Wrap unknown exceptions for traceability
                result.Notes += $"[{extractor.FieldType}] Unexpected error: {ex.Message}\n";
            }
        }

        result.OverallConfidence = result.Fields.Count > 0
            ? result.Fields.Values.Average(f => f.Confidence)
            : 0.0;

        return result;
    }

    private static byte[] ParseBase64Image(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
            throw new VisionProcessingException("Input Base64 string is null or empty.");

        // Remove data URI scheme if present
        var match = Regex.Match(base64, @"^data:image/(?<type>.+?);base64,(?<data>.+)$");
        if (match.Success)
            base64 = match.Groups["data"].Value;

        try
        {
            return Convert.FromBase64String(base64);
        }
        catch (FormatException ex)
        {
            throw new VisionProcessingException("Invalid Base64 image data.", ex);
        }
    }

    private static byte[] ReadAllBytes(Stream stream)
    {
        if (stream == null)
            throw new VisionProcessingException("Input stream is null.");
        if (!stream.CanRead)
            throw new VisionProcessingException("Input stream is not readable.");
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        if (memory.Length == 0)
            throw new VisionProcessingException("Input stream is empty.");
        return memory.ToArray();
    }

    // Batch processing
    public List<CardExtractionResult> ProcessBatch(IEnumerable<(string frontBase64, string? backBase64)> cards)
        => cards.Select(c => ProcessCard(c.frontBase64, c.backBase64)).ToList();
}