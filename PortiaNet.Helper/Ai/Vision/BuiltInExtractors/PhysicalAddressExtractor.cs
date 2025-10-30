using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class PhysicalAddressExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.PhysicalAddress;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "123 Main St, City, Country", Confidence = 0.5 };
}