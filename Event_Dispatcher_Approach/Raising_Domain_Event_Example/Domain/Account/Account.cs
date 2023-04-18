class Account : Aggregate
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid CompanyId { get; set; }

    public Account(string name, string email, Guid companyId) : base()
    {
        Name = name;
        Email = email;
        CompanyId = companyId;

        RaiseEvent(new AccountCreatedEvent(Id, Name, Email, companyId));
    }
}
