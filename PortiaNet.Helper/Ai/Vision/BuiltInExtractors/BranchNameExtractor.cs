using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class BranchNameExtractor : IFieldExtractor
{
    public CardFieldType FieldType => CardFieldType.BranchName;

    public CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options)
        => new CardFieldExtraction { Value = "Main Branch", Confidence = 0.5 };
}