
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class FlightHealthEventEndpoints
{
    public static IEndpointRouteBuilder MapFlightHealthEventEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/flightHealthEvent").WithTags("FlightHealthEvents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignConnectedAircraft", AssignConnectedAircraft);
        group.MapPut("/unassignConnectedAircraft", UnassignConnectedAircraft);


        return app;
    }

    private static async Task<IResult> Create(
        FlightHealthEventRequest request,
        IFlightHealthEventService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFlightHealthEvent( request );

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
        FlightHealthEventRequest request,
        IFlightHealthEventService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFlightHealthEvent( request );

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
        IFlightHealthEventService service,
        CancellationToken cancellationToken) {

        var flightHealthEvent = await service.Get(identifier, cancellationToken);
        return flightHealthEvent is null ? Results.NotFound() : Results.Ok( flightHealthEvent );
    }


    private static async Task<IResult> GetAll(
        IFlightHealthEventService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FlightHealthEventResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFlightHealthEventService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectedAircraft(
        AssociationRequest request,
        IFlightHealthEventService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignConnectedAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignConnectedAircraft(
    AssociationRequest request,
    IFlightHealthEventService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignConnectedAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static FlightHealthEvent mapRequestToFlightHealthEvent( FlightHealthEventRequest request ) {
        var model = new FlightHealthEvent
        {
            Id = request.Id,
            EventCode = request.EventCode,
            Severity = request.Severity,
        };
        return model;
    }

}
