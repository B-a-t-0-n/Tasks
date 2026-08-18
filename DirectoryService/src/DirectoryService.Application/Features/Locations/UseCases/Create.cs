using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Application.Validation;
using DirectoryService.Contracts.DTOs;
using DirectoryService.Contracts.Requests.Locations;
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

namespace DirectoryService.Application.Features.Locations.UseCases;

public sealed record CreateLocationCommand(CreateLocationRequest Request) : ICommand;

public sealed class CreateLocationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/locations", async Task<EndpointResult<Guid>> (
            [FromBody] CreateLocationRequest request,
            [FromServices] CreateLocationHandler handler,
            CancellationToken ct) =>
        {
            var command = new CreateLocationCommand(request);

            return await handler.Handle(command ,ct);
        })
        .WithTags("Locations");
    }
}

public sealed class CreateLocationValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Request.Name).MustBeValueObject(LocationName.Create);
        RuleFor(x => x.Request.Address).MustBeValueObject(a => Address.Create(a.Street, a.City, a.PostalCode, a.Region, a.Country));
        RuleFor(x => x.Request.Timezone).MustBeValueObject(IANACode.Create);
    }
}

public sealed class CreateLocationHandler(
    ILocationRepository repository,
    ILogger<CreateLocationHandler> logger,
    IValidator<CreateLocationCommand> validator) : ICommandHandler<Guid ,CreateLocationCommand>
{
    private readonly ILocationRepository _repository = repository;
    private readonly ILogger<CreateLocationHandler> _logger = logger;
    private readonly IValidator<CreateLocationCommand> _validator = validator;

    public async Task<Result<Guid,Error>> Handle(CreateLocationCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToError();
        }

        var id = LocationId.NewId();

        var name = LocationName.Create(command.Request.Name).Value;

        var locationResult = await _repository.GetByAsync(l => l.Name == name, ct);
        if (locationResult.IsFailure)
            return locationResult.Error;

        var address = Address.Create(
            command.Request.Address.Street,
            command.Request.Address.City,
            command.Request.Address.PostalCode,
            command.Request.Address.Region,
            command.Request.Address.Country).Value;

        var timezone = IANACode.Create(command.Request.Timezone).Value;

        var location = new Location(id, name, address, timezone);

        var result = _repository.Add(location, ct);

        _logger.LogInformation("created location with id {id}", location.Id);

        return (Guid)location.Id;
    }
}


