using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class CVVExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.CVV;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "123", Confidence = 0.5 };
}