using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PortiaNet.Helper.Database.Model
{
    [Index(nameof(TableName), nameof(PrimaryKey), nameof(DateTime))]
    public class Audit
    {
        [MaxLength(250)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(450)]
        public string? UserId { get; set; } = string.Empty;

        [MaxLength(15)]
        public string Type { get; set; } = string.Empty;

        [MaxLength(100)]
        public string TableName { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        public string OldValues { get; set; } = string.Empty;

        public string NewValues { get; set; } = string.Empty;

        public string AffectedColumns { get; set; } = string.Empty;

        [MaxLength(200)]
        public string PrimaryKey { get; set; } = string.Empty;
    }
}
