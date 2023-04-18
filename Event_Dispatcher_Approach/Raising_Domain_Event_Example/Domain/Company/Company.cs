namespace Raising_Domain_Event_Example.Domain.Company;

class Company : Aggregate
{
    public string Name { get; private set; }
    public string Address { get; private set; }

    public Company(string name, string address) : base()
    {
        Name = name;
        Address = address;
        RaiseEvent(new CompanyCreatedEvent(Id, Name, Address));
    }
}
