using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortiaNet.Helper.Database.Model;

namespace PortiaNet.Helper.Database;

public class AuditableIdentityDbContext<TUser, TContext>(DbContextOptions<TContext> options) : IdentityDbContext<TUser>(options)
    where TUser : IdentityUser
    where TContext : DbContext
{
    public virtual int SaveChanges(string? userId)
    {
        OnBeforeSaveChanges(userId);
        var result = base.SaveChanges();
        return result;
    }

    public virtual async Task<int> SaveChangesAsync(string? userId)
    {
        OnBeforeSaveChanges(userId);
        var result = await base.SaveChangesAsync();
        return result;
    }

    #region Audit Trail

    public DbSet<Audit> AuditLogs { get; set; }

    #endregion Auding Trail

    private void OnBeforeSaveChanges(string? userId)
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Audit ||
                entry.State == EntityState.Detached ||
                entry.State == EntityState.Unchanged ||
                entry.GetType().GetCustomAttributes(typeof(NoAuditAttribute), false).Any())
                continue;
            var auditEntry = new AuditEntry(entry)
            {
                TableName = entry.Entity.GetType().Name,
                UserId = userId
            };
            auditEntries.Add(auditEntry);
            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = AuditType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }
        }
        foreach (var auditEntry in auditEntries)
        {
            AuditLogs.Add(auditEntry.ToAudit());
        }
    }
}