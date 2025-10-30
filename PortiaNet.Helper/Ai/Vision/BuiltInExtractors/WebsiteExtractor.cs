using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class WebsiteExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.Website;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "https://sample.com", Confidence = 0.5 };
}