using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class OperatorEndpoints
{
    public static IEndpointRouteBuilder MapOperatorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/operator").WithTags("Operators");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSalesRegion", AssignSalesRegion);
        group.MapPut("/unassignSalesRegion", UnassignSalesRegion);

    group.MapPut("/addToAircraftOrders", AddToAircraftOrders);
    group.MapPut("/removeFromAircraftOrders", RemoveFromAircraftOrders);

    group.MapPut("/addToOperatedAircraft", AddToOperatedAircraft);
    group.MapPut("/removeFromOperatedAircraft", RemoveFromOperatedAircraft);


        return app;
    }

    private static async Task<IResult> Create(
        OperatorRequest request,
        IOperatorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOperator( request );

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
        OperatorRequest request,
        IOperatorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOperator( request );

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
        IOperatorService service,
        CancellationToken cancellationToken) {

        var operator = await service.Get(identifier, cancellationToken);
        return operator is null ? Results.NotFound() : Results.Ok( operator );
    }


    private static async Task<IResult> GetAll(
        IOperatorService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OperatorResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOperatorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSalesRegion(
        AssociationRequest request,
        IOperatorService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSalesRegion(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSalesRegion(
    AssociationRequest request,
    IOperatorService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSalesRegion(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAircraftOrders(
        MultipleAssociationRequest request,
        IOperatorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAircraftOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAircraftOrders(
        MultipleAssociationRequest request,
        IOperatorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAircraftOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOperatedAircraft(
        MultipleAssociationRequest request,
        IOperatorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOperatedAircraft(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOperatedAircraft(
        MultipleAssociationRequest request,
        IOperatorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOperatedAircraft(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Operator mapRequestToOperator( OperatorRequest request ) {
        var model = new Operator
        {
            Id = request.Id,
            Name = request.Name,
            IcaoDesignator = request.IcaoDesignator,
            OperatorType = request.OperatorType,
        };
        return model;
    }

}
