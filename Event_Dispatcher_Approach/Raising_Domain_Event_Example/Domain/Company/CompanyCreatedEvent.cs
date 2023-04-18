namespace Raising_Domain_Event_Example.Domain.Company;

internal class CompanyCreatedEvent : IDomainEvent
{
    public Guid CompanyId { get; }
    public string Name { get; }
    public string Address { get; }
    public DateTime OccurredOn { get; }

    public CompanyCreatedEvent(Guid id, string name, string address)
    {
        OccurredOn = DateTime.Now;
        CompanyId = id;
        Name = name;
        Address = address;
    }
}