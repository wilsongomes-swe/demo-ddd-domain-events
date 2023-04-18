interface IDomainEventDispatcher { 
    Task Dispatch(IDomainEvent domainEvent); 
}
