using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class CompanyNameExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.CompanyName;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "Sample Company", Confidence = 0.5 };
}