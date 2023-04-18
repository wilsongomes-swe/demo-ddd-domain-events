class CreateIntegrationEventCreatedAccountHandler : IDomainEventHandler<AccountCreatedEvent>
{
    private readonly ILogger<CreateIntegrationEventCreatedAccountHandler> _logger;

    public CreateIntegrationEventCreatedAccountHandler(ILogger<CreateIntegrationEventCreatedAccountHandler> logger) => _logger = logger;

    public Task Handle(AccountCreatedEvent domainEvent)
    {
        _logger.LogInformation("Let`s communicate other microservices: Account created event handled for {email}", 
            domainEvent.Email);
        return Task.CompletedTask;
    }
}
