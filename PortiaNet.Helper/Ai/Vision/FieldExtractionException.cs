using PortiaNet.Helper.Ai.Vision;

namespace PortiaNet.Helper.Ai.Vision;

public class FieldExtractionException(CardFieldType fieldType, string message, Exception? inner = null)
    : Exception($"[{fieldType}] {message}", inner)
{
    public CardFieldType FieldType { get; } = fieldType;
}