using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortiaNet.Helper.SecurityHelper;

namespace PortiaNet.Helper.Database;

public class SecureAuditableIdentityDbContext<TUser, TContext> : AuditableIdentityDbContext<TUser, TContext>
    where TUser : IdentityUser
    where TContext : DbContext
{
    public readonly string EncryptionPrefix = "ENC::";

    /// <summary>
    /// Creates a new instance of <code>SecureAuditableIdentityDbContext</code>
    /// </summary>
    /// <param name="options">The configuration of the DbContext</param>
    /// <param name="encDecHelper">An instance of <code>EncryptionDecryptionHelper</code> class for encryption and decryption methods.</param>
    public SecureAuditableIdentityDbContext(DbContextOptions<TContext> options, EncryptionDecryptionHelper encDecHelper)
        : base(options)
    {
        EncDecHelper = encDecHelper;
    }

    public readonly EncryptionDecryptionHelper EncDecHelper;

    public override int SaveChanges()
    {
        EncryptSensitiveData();
        return base.SaveChanges();
    }

    public override int SaveChanges(string? userId)
    {
        EncryptSensitiveData();
        return base.SaveChanges(userId);
    }

    public Task<int> SaveChangesAsync()
    {
        EncryptSensitiveData();
        return base.SaveChangesAsync();
    }

    public override Task<int> SaveChangesAsync(string? userId)
    {
        EncryptSensitiveData();
        return base.SaveChangesAsync(userId);
    }

    private void EncryptSensitiveData()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            foreach (var property in entry.Entity.GetType().GetProperties()
                         .Where(p => Attribute.IsDefined(p, typeof(EncryptAttribute))))
            {
                var value = property.GetValue(entry.Entity) as string;

                if (!string.IsNullOrEmpty(value) && !IsEncrypted(value))
                {
                    property.SetValue(entry.Entity, EncryptionPrefix + EncDecHelper.EncryptString(value));
                }
            }
        }
    }

    public void DecryptSensitiveData<T>(T entity)
    {
        if(entity == null)
            return;

        foreach (var property in entity.GetType().GetProperties()
                     .Where(p => Attribute.IsDefined(p, typeof(EncryptAttribute))))
        {
            var value = property.GetValue(entity) as string;
            if (!string.IsNullOrEmpty(value) && IsEncrypted(value))
            {
                property.SetValue(entity, EncDecHelper.DecryptString(value[EncryptionPrefix.Length..]));
            }
        }
    }

    private bool IsEncrypted(string text) => text.StartsWith(EncryptionPrefix);
}