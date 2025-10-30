using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class BankNameExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.BankName;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "Sample Bank", Confidence = 0.5 };
}