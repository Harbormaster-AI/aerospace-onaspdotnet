
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class SoftwareLoadEndpoints
{
    public static IEndpointRouteBuilder MapSoftwareLoadEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/softwareLoad").WithTags("SoftwareLoads");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignConnectedAircraft", AssignConnectedAircraft);
        group.MapPut("/unassignConnectedAircraft", UnassignConnectedAircraft);
        group.MapPut("/assignAvionicsSuite", AssignAvionicsSuite);
        group.MapPut("/unassignAvionicsSuite", UnassignAvionicsSuite);


        return app;
    }

    private static async Task<IResult> Create(
        SoftwareLoadRequest request,
        ISoftwareLoadService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSoftwareLoad( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        SoftwareLoadRequest request,
        ISoftwareLoadService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSoftwareLoad( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISoftwareLoadService service,
        CancellationToken cancellationToken) {

        var softwareLoad = await service.Get(identifier, cancellationToken);
        return softwareLoad is null ? Results.NotFound() : Results.Ok( softwareLoad );
    }


    private static async Task<IResult> GetAll(
        ISoftwareLoadService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SoftwareLoadResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISoftwareLoadService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectedAircraft(
        AssociationRequest request,
        ISoftwareLoadService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignConnectedAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignConnectedAircraft(
    AssociationRequest request,
    ISoftwareLoadService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignConnectedAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAvionicsSuite(
        AssociationRequest request,
        ISoftwareLoadService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAvionicsSuite(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAvionicsSuite(
    AssociationRequest request,
    ISoftwareLoadService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAvionicsSuite(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static SoftwareLoad mapRequestToSoftwareLoad( SoftwareLoadRequest request ) {
        var model = new SoftwareLoad
        {
            Id = request.Id,
            Version = request.Version,
            LoadType = request.LoadType,
        };
        return model;
    }

}
