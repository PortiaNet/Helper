using PortiaNet.Helper.Ai.Vision;
namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class CardholderNameExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.CardholderName;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
    {
        try
        {
            // TODO: Replace with real OCR/AI logic
            // For now, simulate extraction
            if (imageBytes.Length == 0)
                throw new ArgumentException("Image data is empty.");

            // Simulate extraction
            return new CardFieldExtraction
            {
                Value = "John Doe",
                Confidence = 0.85
            };
        }
        catch (Exception ex)
        {
            throw new FieldExtractionException(FieldType, "Failed to extract cardholder name.", ex);
        }
    }
}