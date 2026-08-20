using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Application.Features.Locations;
using DirectoryService.Application.Validation;
using DirectoryService.Contracts.Requests.Departments;
using DirectoryService.Domain.Entity;
using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Features.Departments.UseCases;

public sealed record CreateDepartmentCommand(CreateDepartmentRequest Request) : ICommand;

public sealed class CreateDepartmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/departments", async Task<EndpointResult<Guid>> (
            [FromBody] CreateDepartmentRequest request,
            [FromServices] CreateDepartmentHandler handler,
            CancellationToken ct) =>
        {
            var command = new CreateDepartmentCommand(request);

            return await handler.Handle(command ,ct);
        })
        .WithTags("Departments");
    }
}

public sealed class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Request.Name).MustBeValueObject(DepartmentName.Create);
        RuleFor(x => x.Request.Identifier).MustBeValueObject(Identifier.Create);
        RuleFor(x => x.Request.LocationIds)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Список локаций не должен быть пустым.")
            .Must(HaveUniqueLocationIds)
            .WithMessage("Все локации должны быть уникальными.");
    }

    private static bool HaveUniqueLocationIds(IEnumerable<Guid> locationIds)
    {
        HashSet<Guid> uniqueLocationIds = [];

        return locationIds.All(uniqueLocationIds.Add);
    }
}

public sealed class CreateDepartmentHandler(
    ITransactionManager transactionManager,
    IDepartmentRepository departmentRepository,
    ILocationRepository locationRepository,
    ILogger<CreateDepartmentHandler> logger,
    IValidator<CreateDepartmentCommand> validator) : ICommandHandler<Guid, CreateDepartmentCommand>
{
    private readonly ITransactionManager _transactionManager = transactionManager;
    private readonly IDepartmentRepository _repository = departmentRepository;
    private readonly ILocationRepository _locationRepository = locationRepository;
    private readonly ILogger<CreateDepartmentHandler> _logger = logger;
    private readonly IValidator<CreateDepartmentCommand> _validator = validator;

    public async Task<Result<Guid, Error>> Handle(CreateDepartmentCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToError();
        }

        var id = DepartmentId.NewId();

        var name = DepartmentName.Create(command.Request.Name).Value;

        var identifier = Identifier.Create(command.Request.Identifier).Value;

        var departmentResult = await _repository.GetByAsync(l => l.Identifier.Value == identifier.Value, ct);
        if (departmentResult.IsSuccess)
            return GeneralErrors.AlreadyExists("department", name.Value);

        DepartmentId? parentId;
        string path;
        Depth depth;

        if (command.Request.ParentId is null)
        {
            parentId = null;
            depth = Depth.Create(0).Value;
            path = identifier.Value;
        }
        else
        {
            parentId = DepartmentId.Create((Guid)command.Request.ParentId);

            var parentResult = await _repository.GetByAsync(l => l.Id == parentId, ct);
            if (parentResult.IsFailure)
                return GeneralErrors.NotFound(parentId, "department");

            depth = Depth.Create((short)(parentResult.Value.Depth.Value + 1)).Value;
            path = $"{parentResult.Value.Path}.{identifier.Value}";
        }

        var locationIdsResult = await _locationRepository.AnyByIds(command.Request.LocationIds, ct);
        if (locationIdsResult.IsFailure)
            return locationIdsResult.Error;

        var locationsDepartment = command.Request.LocationIds.Select(i => new DepartmentLocation(id, LocationId.Create(i)));

        var Department = new Department(id, name, identifier, parentId, path, depth, locationsDepartment);

        await _repository.Add(Department, ct);

        var saveResult = await _transactionManager.SaveChangesAsync(ct);
        if (saveResult.IsFailure)
            return saveResult.Error;

        _logger.LogInformation("created Department with id {id}", Department.Id);

        return (Guid)Department.Id;
    }
}
