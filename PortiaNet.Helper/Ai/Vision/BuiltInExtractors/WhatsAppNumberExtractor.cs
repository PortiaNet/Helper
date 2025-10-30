using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class WhatsAppNumberExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.WhatsAppNumber;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "+1 234 567 8902", Confidence = 0.5 };
}