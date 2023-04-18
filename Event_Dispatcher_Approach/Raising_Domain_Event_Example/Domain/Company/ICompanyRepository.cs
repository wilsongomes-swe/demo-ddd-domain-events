namespace Raising_Domain_Event_Example.Domain.Company;

interface ICompanyRepository
{
    Task Insert(Company company);
}
