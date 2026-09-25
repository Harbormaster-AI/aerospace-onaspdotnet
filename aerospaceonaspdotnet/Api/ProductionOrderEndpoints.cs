
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class ProductionOrderEndpoints
{
    public static IEndpointRouteBuilder MapProductionOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productionOrder").WithTags("ProductionOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);
        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);
        group.MapPut("/assignAircraftOrder", AssignAircraftOrder);
        group.MapPut("/unassignAircraftOrder", UnassignAircraftOrder);


        return app;
    }

    private static async Task<IResult> Create(
        ProductionOrderRequest request,
        IProductionOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionOrder( request );

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
        ProductionOrderRequest request,
        IProductionOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionOrder( request );

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
        IProductionOrderService service,
        CancellationToken cancellationToken) {

        var productionOrder = await service.Get(identifier, cancellationToken);
        return productionOrder is null ? Results.NotFound() : Results.Ok( productionOrder );
    }


    private static async Task<IResult> GetAll(
        IProductionOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductionOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductionOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IProductionOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IProductionOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IProductionOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IProductionOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraftOrder(
        AssociationRequest request,
        IProductionOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraftOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraftOrder(
    AssociationRequest request,
    IProductionOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraftOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ProductionOrder mapRequestToProductionOrder( ProductionOrderRequest request ) {
        var model = new ProductionOrder
        {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            Status = request.Status,
        };
        return model;
    }

}
