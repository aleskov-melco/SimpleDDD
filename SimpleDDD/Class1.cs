using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleDDD
{
    /// <summary>
    /// Represents an aggregate instance location in the abstract aggregate pool inside the domain
    /// </summary>
    public interface IAggregateAddress
    {
        /// <summary>
        /// Name of the aggregate kind, usually it is a name of the corresponding C# class representing the aggregate.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// An aggregate instance identifier inside its kind. Different kinds can reuse the same Id.
        /// It is recommended to take business identifiers instead of artificial ones like GUID
        /// </summary>
        string Id { get; }
    }

    
    public interface IDomainEvent
    {
        string Id { get; }
        long Version { get; set; }
        /// <summary>
        ///     Gets the identifier of the source originating the event.
        /// </summary>
        IAggregateAddress Source { get; }
        DateTimeOffset Occured { get; }
    }
    
    public interface IEventSourced
    {
        string Id { get; }
        void Apply(IDomainEvent evt);
        long Version { get; }
    }
    
    public interface ICommand
    {
        string Id { get; }
        IAggregateAddress Destination { get; }
    }
    
    public interface ICommandHandler<in TCommand, TResult>
    {
        TResult Execute(TCommand command);
    }
    
    public interface ICommandHandler<in TCommand>
    {
        Task Execute(TCommand command);
    }
    
    public interface IAggregate : IEventSourced, ICommandHandler<ICommand, IReadOnlyCollection<IDomainEvent>>
    {
    }
}