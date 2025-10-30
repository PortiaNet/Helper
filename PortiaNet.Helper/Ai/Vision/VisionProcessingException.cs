namespace PortiaNet.Helper.Ai.Vision;

public class VisionProcessingException(string message, Exception? inner = null) : Exception(message, inner);