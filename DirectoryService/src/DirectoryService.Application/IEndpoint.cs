using Microsoft.AspNetCore.Routing;

namespace DirectoryService.Application;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
