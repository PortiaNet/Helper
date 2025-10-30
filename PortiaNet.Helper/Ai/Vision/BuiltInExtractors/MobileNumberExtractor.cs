using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class MobileNumberExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.MobileNumber;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "+1 234 567 8901", Confidence = 0.5 };
}