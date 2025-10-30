using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace PortiaNet.Helper.Database.Model
{
    public class AuditEntry(EntityEntry entry)
    {
        public EntityEntry Entry { get; } = entry;
        public string? UserId { get; set; }
        public string TableName { get; set; } = string.Empty;
        public Dictionary<string, object?> KeyValues { get; } = [];
        public Dictionary<string, object?> OldValues { get; } = [];
        public Dictionary<string, object?> NewValues { get; } = [];
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = [];
        public Audit ToAudit()
        {
            var audit = new Audit
            {
                UserId = UserId,
                Type = AuditType.ToString(),
                TableName = TableName,
                DateTime = DateTime.UtcNow,
                PrimaryKey = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(NewValues),
                AffectedColumns = ChangedColumns.Count == 0 ? string.Empty : JsonConvert.SerializeObject(ChangedColumns)
            };
            return audit;
        }
    }
}
