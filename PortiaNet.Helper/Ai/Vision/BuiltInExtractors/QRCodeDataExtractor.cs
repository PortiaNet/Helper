using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class QRCodeDataExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.QRCodeData;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "https://sample.com/qr", Confidence = 0.5 };
}