using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class ExpiryDateExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.ExpiryDate;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "12/34", Confidence = 0.5 };
}