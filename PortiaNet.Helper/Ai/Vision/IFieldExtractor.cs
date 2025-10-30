using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision;

public interface IFieldExtractor
{
    CardFieldType FieldType { get; }
    CardFieldExtraction? Extract(ReadOnlySpan<byte> imageBytes, VisionProcessingOptions options);
}