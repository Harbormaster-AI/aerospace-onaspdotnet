
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftEndpoints
{
    public static IEndpointRouteBuilder MapAircraftEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraft").WithTags("Aircrafts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);
        group.MapPut("/assignOperator_", AssignOperator_);
        group.MapPut("/unassignOperator_", UnassignOperator_);
        group.MapPut("/assignRegistration", AssignRegistration);
        group.MapPut("/unassignRegistration", UnassignRegistration);
        group.MapPut("/assignWarranty", AssignWarranty);
        group.MapPut("/unassignWarranty", UnassignWarranty);
        group.MapPut("/assignConnectedAircraft", AssignConnectedAircraft);
        group.MapPut("/unassignConnectedAircraft", UnassignConnectedAircraft);
        group.MapPut("/assignCabinLayout", AssignCabinLayout);
        group.MapPut("/unassignCabinLayout", UnassignCabinLayout);

    group.MapPut("/addToMaintenanceRecords", AddToMaintenanceRecords);
    group.MapPut("/removeFromMaintenanceRecords", RemoveFromMaintenanceRecords);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraft( request );

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
        AircraftRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraft( request );

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
        IAircraftService service,
        CancellationToken cancellationToken) {

        var aircraft = await service.Get(identifier, cancellationToken);
        return aircraft is null ? Results.NotFound() : Results.Ok( aircraft );
    }


    private static async Task<IResult> GetAll(
        IAircraftService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IAircraftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOperator_(
        AssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOperator_(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOperator_(
    AssociationRequest request,
    IAircraftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOperator_(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRegistration(
        AssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRegistration(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRegistration(
    AssociationRequest request,
    IAircraftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRegistration(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWarranty(
        AssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWarranty(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWarranty(
    AssociationRequest request,
    IAircraftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWarranty(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectedAircraft(
        AssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignConnectedAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignConnectedAircraft(
    AssociationRequest request,
    IAircraftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignConnectedAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCabinLayout(
        AssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCabinLayout(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCabinLayout(
    AssociationRequest request,
    IAircraftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCabinLayout(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToMaintenanceRecords(
        MultipleAssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMaintenanceRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMaintenanceRecords(
        MultipleAssociationRequest request,
        IAircraftService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMaintenanceRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Aircraft mapRequestToAircraft( AircraftRequest request ) {
        var model = new Aircraft
        {
            Id = request.Id,
            Msn = request.Msn,
            DeliveryDate = request.DeliveryDate,
        };
        return model;
    }

}
