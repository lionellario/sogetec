using Microsoft.AspNetCore.Routing;

namespace Sogetec.Chassis.Endpoints;

public interface IEndpoint
{
    void Configure(IEndpointRouteBuilder app);
}
