using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using PortiaNet.Helper.Database.Model;
using System.Collections.Concurrent;
using System.Reflection;

namespace PortiaNet.Helper.Database;

public class AuditableIdentityDbContext<TUser, TContext>(DbContextOptions<TContext> options) : IdentityDbContext<TUser>(options)
    where TUser : IdentityUser
    where TContext : DbContext
{
    private static readonly ConcurrentDictionary<Type, AuditEntityMetadata> EntityMetadataCache = new();

    public List<string> NonAuditedColumns { get; set; } = [];

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
        var nonAuditedColumnsLookup = BuildNonAuditedColumnsLookup();
        var auditEntries = new List<AuditEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Audit ||
                entry.State == EntityState.Detached ||
                entry.State == EntityState.Unchanged ||
                IsEntityExcluded(entry))
                continue;

            var auditEntry = new AuditEntry(entry)
            {
                TableName = entry.Entity.GetType().Name,
                UserId = userId
            };

            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                if (IsPropertyExcluded(entry, property, nonAuditedColumnsLookup))
                {
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

            if (auditEntry.AuditType != AuditType.None)
            {
                auditEntries.Add(auditEntry);
            }
        }

        foreach (var auditEntry in auditEntries)
        {
            AuditLogs.Add(auditEntry.ToAudit());
        }
    }

    private bool IsEntityExcluded(EntityEntry entry)
    {
        var metadata = GetOrCreateEntityMetadata(entry.Entity.GetType());
        return metadata.IsEntityExcluded;
    }

    private bool IsPropertyExcluded(EntityEntry entry, PropertyEntry property, NonAuditedColumnsLookup lookup)
    {
        var metadata = GetOrCreateEntityMetadata(entry.Entity.GetType());
        var propertyName = property.Metadata.Name;
        if (metadata.NonAuditedProperties.Contains(propertyName))
        {
            return true;
        }

        if (!lookup.HasAny)
        {
            return false;
        }

        var entityName = entry.Entity.GetType().Name;
        return lookup.GlobalColumns.Contains(propertyName) ||
               (lookup.PerEntityColumns.TryGetValue(entityName, out var entityColumns) && entityColumns.Contains(propertyName));
    }

    private static AuditEntityMetadata GetOrCreateEntityMetadata(Type entityType)
    {
        return EntityMetadataCache.GetOrAdd(entityType, static type =>
        {
            var isEntityExcluded = Attribute.IsDefined(type, typeof(NoAuditAttribute), inherit: true);
            var nonAuditedProperties = type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(property => Attribute.IsDefined(property, typeof(NoAuditAttribute), inherit: true))
                .Select(property => property.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            return new AuditEntityMetadata(isEntityExcluded, nonAuditedProperties);
        });
    }

    private NonAuditedColumnsLookup BuildNonAuditedColumnsLookup()
    {
        if (NonAuditedColumns.Count == 0)
        {
            return NonAuditedColumnsLookup.Empty;
        }

        var globalColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var perEntityColumns = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var rawColumn in NonAuditedColumns)
        {
            if (string.IsNullOrWhiteSpace(rawColumn))
            {
                continue;
            }

            var column = rawColumn.Trim();
            var dotIndex = column.IndexOf('.');
            if (dotIndex <= 0 || dotIndex >= column.Length - 1)
            {
                globalColumns.Add(column);
                continue;
            }

            var entityName = column[..dotIndex].Trim();
            var propertyName = column[(dotIndex + 1)..].Trim();
            if (entityName.Length == 0 || propertyName.Length == 0)
            {
                globalColumns.Add(column);
                continue;
            }

            if (!perEntityColumns.TryGetValue(entityName, out var entityColumns))
            {
                entityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                perEntityColumns[entityName] = entityColumns;
            }

            entityColumns.Add(propertyName);
        }

        return new NonAuditedColumnsLookup(globalColumns, perEntityColumns);
    }

    private sealed record AuditEntityMetadata(bool IsEntityExcluded, HashSet<string> NonAuditedProperties);

    private sealed record NonAuditedColumnsLookup(HashSet<string> GlobalColumns, Dictionary<string, HashSet<string>> PerEntityColumns)
    {
        public static NonAuditedColumnsLookup Empty { get; } = new([], new(StringComparer.OrdinalIgnoreCase));
        public bool HasAny => GlobalColumns.Count > 0 || PerEntityColumns.Count > 0;
    }
}