
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class MaintenanceAppointmentEndpoints
{
    public static IEndpointRouteBuilder MapMaintenanceAppointmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/maintenanceAppointment").WithTags("MaintenanceAppointments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAircraft", AssignAircraft);
        group.MapPut("/unassignAircraft", UnassignAircraft);
        group.MapPut("/assignMroFacility", AssignMroFacility);
        group.MapPut("/unassignMroFacility", UnassignMroFacility);
        group.MapPut("/assignWorkOrder", AssignWorkOrder);
        group.MapPut("/unassignWorkOrder", UnassignWorkOrder);


        return app;
    }

    private static async Task<IResult> Create(
        MaintenanceAppointmentRequest request,
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenanceAppointment( request );

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
        MaintenanceAppointmentRequest request,
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenanceAppointment( request );

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
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {

        var maintenanceAppointment = await service.Get(identifier, cancellationToken);
        return maintenanceAppointment is null ? Results.NotFound() : Results.Ok( maintenanceAppointment );
    }


    private static async Task<IResult> GetAll(
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MaintenanceAppointmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraft(
        AssociationRequest request,
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraft(
    AssociationRequest request,
    IMaintenanceAppointmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMroFacility(
        AssociationRequest request,
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMroFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMroFacility(
    AssociationRequest request,
    IMaintenanceAppointmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMroFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkOrder(
        AssociationRequest request,
        IMaintenanceAppointmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkOrder(
    AssociationRequest request,
    IMaintenanceAppointmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static MaintenanceAppointment mapRequestToMaintenanceAppointment( MaintenanceAppointmentRequest request ) {
        var model = new MaintenanceAppointment
        {
            Id = request.Id,
            AppointmentDate = request.AppointmentDate,
            Status = request.Status,
        };
        return model;
    }

}
