record AccountCreatedEvent(
    Guid Id,
    string Name,
    string Email,
    Guid CompanyId) : IDomainEvent
{
    DateTime IDomainEvent.OccurredOn { get; } = DateTime.Now;
}
