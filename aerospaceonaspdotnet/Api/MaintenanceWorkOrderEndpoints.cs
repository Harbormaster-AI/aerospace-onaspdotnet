
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class MaintenanceWorkOrderEndpoints
{
    public static IEndpointRouteBuilder MapMaintenanceWorkOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/maintenanceWorkOrder").WithTags("MaintenanceWorkOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAircraft", AssignAircraft);
        group.MapPut("/unassignAircraft", UnassignAircraft);
        group.MapPut("/assignAirworthinessDirective", AssignAirworthinessDirective);
        group.MapPut("/unassignAirworthinessDirective", UnassignAirworthinessDirective);
        group.MapPut("/assignServiceBulletin", AssignServiceBulletin);
        group.MapPut("/unassignServiceBulletin", UnassignServiceBulletin);


        return app;
    }

    private static async Task<IResult> Create(
        MaintenanceWorkOrderRequest request,
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenanceWorkOrder( request );

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
        MaintenanceWorkOrderRequest request,
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenanceWorkOrder( request );

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
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {

        var maintenanceWorkOrder = await service.Get(identifier, cancellationToken);
        return maintenanceWorkOrder is null ? Results.NotFound() : Results.Ok( maintenanceWorkOrder );
    }


    private static async Task<IResult> GetAll(
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MaintenanceWorkOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraft(
        AssociationRequest request,
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraft(
    AssociationRequest request,
    IMaintenanceWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAirworthinessDirective(
        AssociationRequest request,
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAirworthinessDirective(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAirworthinessDirective(
    AssociationRequest request,
    IMaintenanceWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAirworthinessDirective(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignServiceBulletin(
        AssociationRequest request,
        IMaintenanceWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignServiceBulletin(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignServiceBulletin(
    AssociationRequest request,
    IMaintenanceWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignServiceBulletin(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static MaintenanceWorkOrder mapRequestToMaintenanceWorkOrder( MaintenanceWorkOrderRequest request ) {
        var model = new MaintenanceWorkOrder
        {
            Id = request.Id,
            WorkOrderNumber = request.WorkOrderNumber,
            Status = request.Status,
        };
        return model;
    }

}
