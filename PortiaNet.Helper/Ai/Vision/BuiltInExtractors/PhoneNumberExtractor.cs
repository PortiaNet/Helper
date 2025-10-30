using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class PhoneNumberExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.PhoneNumber;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "+1 234 567 8900", Confidence = 0.5 };
}