using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raising_Domain_Event_Example.Domain.Company;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("PocApp"));

builder.Services.AddMediatR(typeof(CreateAccountUseCase));

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEF>();

builder.Services.AddSingleton<IDomainEventHandler<AccountCreatedEvent>, CreateIntegrationEventCreatedAccountHandler>();
builder.Services.AddSingleton<IDomainEventHandler<AccountCreatedEvent>, SendEmailForAccountCreatedEventListener>();
builder.Services.AddSingleton<IDomainEventHandler<CompanyCreatedEvent>, CreateIntegrationEventCreatedCompanyHandler>();

// the below handler is a generic one, if the event raised does not have a specific handler, it will be handled by this one
// can be used to log all events raised, or to publish all events direct to the event bus (RabbitMQ, Kafka, etc)
// builder.Services.AddSingleton(typeof(IDomainEventHandler<>), typeof(GenericCreateIntegrationEventHandler<>));

builder.Services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/accounts", 
    async ([FromBody] CreateAccountInput input, IMediator mediator) => await mediator.Send(input))
    .WithOpenApi();

app.MapGet("/accounts",
    async (AppDbContext context) => await context.Accounts.ToListAsync())
    .WithOpenApi();

app.MapGet("/companies",
    async (AppDbContext context) => await context.Companies.ToListAsync())
    .WithOpenApi();

app.Run();
