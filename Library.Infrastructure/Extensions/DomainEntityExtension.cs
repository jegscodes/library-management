using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Infrastructure.Extensions;

static class DomainEntityExtension
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
                             r.TargetEntry != null &&
                             r.TargetEntry.Metadata.IsOwned() &&
                             (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
