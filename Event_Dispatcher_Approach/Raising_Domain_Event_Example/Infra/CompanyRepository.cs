using Raising_Domain_Event_Example.Domain.Company;

class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context) => _context = context;

    public async Task Insert(Company company) => await _context.Companies.AddAsync(company);
}
