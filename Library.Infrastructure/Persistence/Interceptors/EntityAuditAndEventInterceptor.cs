namespace Library.Infrastructure.Persistence.Interceptors;

sealed class EntityAuditAndEventInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public EntityAuditAndEventInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        await _mediator.DispatchDomainEventsAsync(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                // TODO
                entry.Entity.CreatedBy = string.Empty;
                entry.Entity.CreatedOn = DateTime.Now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                // TODO
                entry.Entity.ModifiedBy = string.Empty;
                entry.Entity.ModifiedOn = DateTime.Now;
            }
        }
    }
}
