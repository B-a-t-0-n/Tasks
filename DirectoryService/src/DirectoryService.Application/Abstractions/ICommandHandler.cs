using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Application.Abstractions;

public interface ICommandHandler<TResponce, in TCommand> where TCommand : ICommand
{
    Task<Result<TResponce, Error>> Handle(TCommand command, CancellationToken cancellation = default);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<UnitResult<Error>> Handle(TCommand command, CancellationToken cancellation = default);
}
