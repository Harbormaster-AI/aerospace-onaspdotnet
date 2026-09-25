
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class InventoryItemEndpoints
{
    public static IEndpointRouteBuilder MapInventoryItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventoryItem").WithTags("InventoryItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignComponent", AssignComponent);
        group.MapPut("/unassignComponent", UnassignComponent);
        group.MapPut("/assignWarehouse", AssignWarehouse);
        group.MapPut("/unassignWarehouse", UnassignWarehouse);


        return app;
    }

    private static async Task<IResult> Create(
        InventoryItemRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryItem( request );

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
        InventoryItemRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryItem( request );

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
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var inventoryItem = await service.Get(identifier, cancellationToken);
        return inventoryItem is null ? Results.NotFound() : Results.Ok( inventoryItem );
    }


    private static async Task<IResult> GetAll(
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InventoryItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignComponent(
        AssociationRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignComponent(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignComponent(
    AssociationRequest request,
    IInventoryItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignComponent(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWarehouse(
        AssociationRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWarehouse(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWarehouse(
    AssociationRequest request,
    IInventoryItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWarehouse(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static InventoryItem mapRequestToInventoryItem( InventoryItemRequest request ) {
        var model = new InventoryItem
        {
            Id = request.Id,
            QuantityOnHand = request.QuantityOnHand,
            QuantityReserved = request.QuantityReserved,
            LotNumber = request.LotNumber,
        };
        return model;
    }

}
