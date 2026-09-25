
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class Operator_Endpoints
{
    public static IEndpointRouteBuilder MapOperator_Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/operator_").WithTags("Operator_s");

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
        Operator_Request request,
        IOperator_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOperator_( request );

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
        Operator_Request request,
        IOperator_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOperator_( request );

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
        IOperator_Service service,
        CancellationToken cancellationToken) {

        var operator_ = await service.Get(identifier, cancellationToken);
        return operator_ is null ? Results.NotFound() : Results.Ok( operator_ );
    }


    private static async Task<IResult> GetAll(
        IOperator_Service service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( Operator_Response.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOperator_Service service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSalesRegion(
        AssociationRequest request,
        IOperator_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSalesRegion(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSalesRegion(
    AssociationRequest request,
    IOperator_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSalesRegion(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAircraftOrders(
        MultipleAssociationRequest request,
        IOperator_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAircraftOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAircraftOrders(
        MultipleAssociationRequest request,
        IOperator_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAircraftOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOperatedAircraft(
        MultipleAssociationRequest request,
        IOperator_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOperatedAircraft(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOperatedAircraft(
        MultipleAssociationRequest request,
        IOperator_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOperatedAircraft(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Operator_ mapRequestToOperator_( Operator_Request request ) {
        var model = new Operator_
        {
            Id = request.Id,
            Name = request.Name,
            IcaoDesignator = request.IcaoDesignator,
            OperatorType = request.OperatorType,
        };
        return model;
    }

}
