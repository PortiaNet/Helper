using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class CardBrandExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.CardBrand;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "VISA", Confidence = 0.5 };
}