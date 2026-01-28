namespace DirectoryService.Web.EndpointsSettings;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
