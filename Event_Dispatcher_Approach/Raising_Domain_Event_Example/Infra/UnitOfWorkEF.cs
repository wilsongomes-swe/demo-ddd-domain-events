class UnitOfWorkEF : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly ILogger<UnitOfWorkEF> _logger;

    public UnitOfWorkEF(AppDbContext context, IDomainEventDispatcher domainEventDispatcher, ILogger<UnitOfWorkEF> logger)
    {
        _context = context;
        _domainEventDispatcher = domainEventDispatcher;
        _logger = logger;
    }

    public async Task Commit()
    {
        await RaiseDomainEvents();
        await _context.SaveChangesAsync();
    }

    private async Task RaiseDomainEvents()
    {
        var modifiedAggregatesChangeTrackers = _context.ChangeTracker
            .Entries<Aggregate>()
            .Where(x => x.Entity.Events is IReadOnlyCollection<IDomainEvent> events && events.Any());
        _logger.LogInformation("Commit: {number} aggregates modifieds", modifiedAggregatesChangeTrackers.Count());
        
        var domainEvents = modifiedAggregatesChangeTrackers
            .SelectMany(x => x.Entity.Events)
            .ToList();
        _logger.LogInformation("Commit: {number} domain events raised", domainEvents.Count());

        foreach(var @event in domainEvents)
            await _domainEventDispatcher.Dispatch((dynamic) @event);
        
        modifiedAggregatesChangeTrackers.ToList().ForEach(aggregates => aggregates.Entity.ClearEvents());
    }
}
