namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;
public class LandlineExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.Landline;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "+1 234 567 8903", Confidence = 0.5 };
}