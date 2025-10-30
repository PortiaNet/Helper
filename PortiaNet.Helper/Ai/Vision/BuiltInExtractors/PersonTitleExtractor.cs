using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class PersonTitleExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.PersonTitle;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "Manager", Confidence = 0.5 };
}