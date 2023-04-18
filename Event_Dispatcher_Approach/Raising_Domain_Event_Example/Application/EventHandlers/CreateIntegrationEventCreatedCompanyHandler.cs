using Raising_Domain_Event_Example.Domain.Company;

class CreateIntegrationEventCreatedCompanyHandler : IDomainEventHandler<CompanyCreatedEvent>
{
    private readonly ILogger<CreateIntegrationEventCreatedCompanyHandler> _logger;

    public CreateIntegrationEventCreatedCompanyHandler(ILogger<CreateIntegrationEventCreatedCompanyHandler> logger) => _logger = logger;

    public Task Handle(CompanyCreatedEvent domainEvent)
    {
        _logger.LogInformation("Let`s communicate other microservices: Comany created event handled for {email}", 
            domainEvent.CompanyId);
        return Task.CompletedTask;
    }
}
