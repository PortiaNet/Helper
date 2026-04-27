namespace PortiaNet.Helper.Database
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class NoAuditAttribute : Attribute
    {
    }
}
