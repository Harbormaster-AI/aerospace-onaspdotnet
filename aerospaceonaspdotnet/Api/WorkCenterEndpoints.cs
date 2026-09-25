
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class WorkCenterEndpoints
{
    public static IEndpointRouteBuilder MapWorkCenterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workCenter").WithTags("WorkCenters");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProductionLine", AssignProductionLine);
        group.MapPut("/unassignProductionLine", UnassignProductionLine);


        return app;
    }

    private static async Task<IResult> Create(
        WorkCenterRequest request,
        IWorkCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkCenter( request );

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
        WorkCenterRequest request,
        IWorkCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkCenter( request );

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
        IWorkCenterService service,
        CancellationToken cancellationToken) {

        var workCenter = await service.Get(identifier, cancellationToken);
        return workCenter is null ? Results.NotFound() : Results.Ok( workCenter );
    }


    private static async Task<IResult> GetAll(
        IWorkCenterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WorkCenterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWorkCenterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProductionLine(
        AssociationRequest request,
        IWorkCenterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProductionLine(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProductionLine(
    AssociationRequest request,
    IWorkCenterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProductionLine(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static WorkCenter mapRequestToWorkCenter( WorkCenterRequest request ) {
        var model = new WorkCenter
        {
            Id = request.Id,
            Name = request.Name,
            Capability = request.Capability,
        };
        return model;
    }

}
