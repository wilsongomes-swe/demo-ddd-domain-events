class GenericCreateIntegrationEventHandler<T> : IDomainEventHandler<T>
    where T : IDomainEvent
{
    private readonly ILogger<GenericCreateIntegrationEventHandler<T>> _logger;

    public GenericCreateIntegrationEventHandler(ILogger<GenericCreateIntegrationEventHandler<T>> logger) => _logger = logger;

    public Task Handle(T domainEvent)
    {
        _logger.LogInformation("[Generic One] Generic one: {eventName} - {@event}", domainEvent.GetType().Name, domainEvent);
        return Task.CompletedTask;
    }
}
