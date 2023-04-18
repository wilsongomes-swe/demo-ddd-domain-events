using MediatR;
using Raising_Domain_Event_Example.Domain.Company;

record CreateAccountInput(string Name, string Email, string Company, string Address) : IRequest<CreateAccountOutput>;
record CreateAccountOutput(Guid Id);

class CreateAccountUseCase : IRequestHandler<CreateAccountInput, CreateAccountOutput>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAccountUseCase> _logger;

    public CreateAccountUseCase(
        ILogger<CreateAccountUseCase> logger, 
        IAccountRepository repository, 
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _accountRepository = repository;
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CreateAccountOutput> Handle(CreateAccountInput input, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating account/company for {email}", input.Email);
        var company = new Company(input.Company, input.Address);
        var account = new Account(input.Name, input.Email, company.Id);
        await _companyRepository.Insert(company);
        await _accountRepository.Insert(account);
        await _unitOfWork.Commit();
        _logger.LogInformation("Created account/company for {email} - transaction commited", input.Email);
        return new CreateAccountOutput(account.Id);
    }
}
