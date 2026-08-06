using Microsoft.AspNetCore.Routing;

namespace DirectoryService.Application.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}