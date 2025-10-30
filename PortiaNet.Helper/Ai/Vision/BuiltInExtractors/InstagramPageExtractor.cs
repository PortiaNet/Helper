using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class InstagramPageExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.InstagramPage;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "@sample_insta", Confidence = 0.5 };
}