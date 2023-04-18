interface IDomainEventDispatcher { 
    Task Dispatch<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent; 
}
