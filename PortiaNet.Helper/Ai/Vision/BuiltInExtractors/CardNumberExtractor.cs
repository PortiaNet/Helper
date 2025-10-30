using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class CardNumberExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.CardNumber;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "1234 5678 9012 3456", Confidence = 0.5 };
}