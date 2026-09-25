
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class ConnectedAircraftEndpoints
{
    public static IEndpointRouteBuilder MapConnectedAircraftEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/connectedAircraft").WithTags("ConnectedAircrafts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAircraft", AssignAircraft);
        group.MapPut("/unassignAircraft", UnassignAircraft);

    group.MapPut("/addToFlightHealthEvents", AddToFlightHealthEvents);
    group.MapPut("/removeFromFlightHealthEvents", RemoveFromFlightHealthEvents);

    group.MapPut("/addToSoftwareLoads", AddToSoftwareLoads);
    group.MapPut("/removeFromSoftwareLoads", RemoveFromSoftwareLoads);


        return app;
    }

    private static async Task<IResult> Create(
        ConnectedAircraftRequest request,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConnectedAircraft( request );

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
        ConnectedAircraftRequest request,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConnectedAircraft( request );

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
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {

        var connectedAircraft = await service.Get(identifier, cancellationToken);
        return connectedAircraft is null ? Results.NotFound() : Results.Ok( connectedAircraft );
    }


    private static async Task<IResult> GetAll(
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ConnectedAircraftResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraft(
        AssociationRequest request,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraft(
    AssociationRequest request,
    IConnectedAircraftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToFlightHealthEvents(
        MultipleAssociationRequest request,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFlightHealthEvents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFlightHealthEvents(
        MultipleAssociationRequest request,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFlightHealthEvents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSoftwareLoads(
        MultipleAssociationRequest request,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSoftwareLoads(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSoftwareLoads(
        MultipleAssociationRequest request,
        IConnectedAircraftService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSoftwareLoads(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ConnectedAircraft mapRequestToConnectedAircraft( ConnectedAircraftRequest request ) {
        var model = new ConnectedAircraft
        {
            Id = request.Id,
            CommunicationsProvider = request.CommunicationsProvider,
            ConnectivityStatus = request.ConnectivityStatus,
        };
        return model;
    }

}
