using Raising_Domain_Event_Example.Domain.Company;

class CreateIntegrationEventCreatedCompanyHandler : IDomainEventHandler<CompanyCreatedEvent>
{
    private readonly ILogger<CreateIntegrationEventCreatedCompanyHandler> _logger;

    public CreateIntegrationEventCreatedCompanyHandler(ILogger<CreateIntegrationEventCreatedCompanyHandler> logger) => _logger = logger;

    public Task Handle(CompanyCreatedEvent domainEvent)
    {
        _logger.LogInformation("Let`s communicate other microservices: Company created event handled for {CompanyId}", 
            domainEvent.CompanyId);
        return Task.CompletedTask;
    }
}
