
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class BuildScheduleEndpoints
{
    public static IEndpointRouteBuilder MapBuildScheduleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/buildSchedule").WithTags("BuildSchedules");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToProductionOrders", AddToProductionOrders);
    group.MapPut("/removeFromProductionOrders", RemoveFromProductionOrders);


        return app;
    }

    private static async Task<IResult> Create(
        BuildScheduleRequest request,
        IBuildScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBuildSchedule( request );

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
        BuildScheduleRequest request,
        IBuildScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBuildSchedule( request );

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
        IBuildScheduleService service,
        CancellationToken cancellationToken) {

        var buildSchedule = await service.Get(identifier, cancellationToken);
        return buildSchedule is null ? Results.NotFound() : Results.Ok( buildSchedule );
    }


    private static async Task<IResult> GetAll(
        IBuildScheduleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BuildScheduleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBuildScheduleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProductionOrders(
        MultipleAssociationRequest request,
        IBuildScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProductionOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProductionOrders(
        MultipleAssociationRequest request,
        IBuildScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProductionOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BuildSchedule mapRequestToBuildSchedule( BuildScheduleRequest request ) {
        var model = new BuildSchedule
        {
            Id = request.Id,
            ScheduleNumber = request.ScheduleNumber,
            Status = request.Status,
        };
        return model;
    }

}
