using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class PersonNameExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.PersonName;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "Jane Smith", Confidence = 0.5 };
}