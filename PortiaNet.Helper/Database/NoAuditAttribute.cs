namespace PortiaNet.Helper.Database
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NoAuditAttribute : Attribute
    {
    }
}
