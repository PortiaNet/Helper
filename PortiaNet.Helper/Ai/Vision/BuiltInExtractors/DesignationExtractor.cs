using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class DesignationExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.Designation;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "Lead Developer", Confidence = 0.5 };
}