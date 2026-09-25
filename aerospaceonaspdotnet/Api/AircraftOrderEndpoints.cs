
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftOrderEndpoints
{
    public static IEndpointRouteBuilder MapAircraftOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraftOrder").WithTags("AircraftOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOperator_", AssignOperator_);
        group.MapPut("/unassignOperator_", UnassignOperator_);
        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);
        group.MapPut("/assignQuote", AssignQuote);
        group.MapPut("/unassignQuote", UnassignQuote);
        group.MapPut("/assignPurchaseAgreement", AssignPurchaseAgreement);
        group.MapPut("/unassignPurchaseAgreement", UnassignPurchaseAgreement);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftOrderRequest request,
        IAircraftOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftOrder( request );

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
        AircraftOrderRequest request,
        IAircraftOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftOrder( request );

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
        IAircraftOrderService service,
        CancellationToken cancellationToken) {

        var aircraftOrder = await service.Get(identifier, cancellationToken);
        return aircraftOrder is null ? Results.NotFound() : Results.Ok( aircraftOrder );
    }


    private static async Task<IResult> GetAll(
        IAircraftOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOperator_(
        AssociationRequest request,
        IAircraftOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOperator_(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOperator_(
    AssociationRequest request,
    IAircraftOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOperator_(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IAircraftOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IAircraftOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignQuote(
        AssociationRequest request,
        IAircraftOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignQuote(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignQuote(
    AssociationRequest request,
    IAircraftOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignQuote(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPurchaseAgreement(
        AssociationRequest request,
        IAircraftOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPurchaseAgreement(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPurchaseAgreement(
    AssociationRequest request,
    IAircraftOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPurchaseAgreement(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static AircraftOrder mapRequestToAircraftOrder( AircraftOrderRequest request ) {
        var model = new AircraftOrder
        {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
        return model;
    }

}
