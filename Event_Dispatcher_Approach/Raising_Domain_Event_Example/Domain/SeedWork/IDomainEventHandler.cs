interface IDomainEventHandler<T> where T : IDomainEvent {
    Task Handle(T domainEvent);
}
