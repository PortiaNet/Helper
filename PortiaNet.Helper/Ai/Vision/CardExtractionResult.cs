namespace PortiaNet.Helper.Ai.Vision;

public class CardExtractionResult
{
    public Dictionary<CardFieldType, CardFieldExtraction> Fields { get; set; } = new();
    public double OverallConfidence { get; set; }
    public string? ImageQuality { get; set; }
    public string? Notes { get; set; }
}

public class CardFieldExtraction
{
    public string? Value { get; set; }
    public double Confidence { get; set; }
}