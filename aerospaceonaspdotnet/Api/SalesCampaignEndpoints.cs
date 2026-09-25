
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class SalesCampaignEndpoints
{
    public static IEndpointRouteBuilder MapSalesCampaignEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/salesCampaign").WithTags("SalesCampaigns");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRegion", AssignRegion);
        group.MapPut("/unassignRegion", UnassignRegion);
        group.MapPut("/assignOperator_", AssignOperator_);
        group.MapPut("/unassignOperator_", UnassignOperator_);

    group.MapPut("/addToQuotes", AddToQuotes);
    group.MapPut("/removeFromQuotes", RemoveFromQuotes);


        return app;
    }

    private static async Task<IResult> Create(
        SalesCampaignRequest request,
        ISalesCampaignService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesCampaign( request );

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
        SalesCampaignRequest request,
        ISalesCampaignService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesCampaign( request );

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
        ISalesCampaignService service,
        CancellationToken cancellationToken) {

        var salesCampaign = await service.Get(identifier, cancellationToken);
        return salesCampaign is null ? Results.NotFound() : Results.Ok( salesCampaign );
    }


    private static async Task<IResult> GetAll(
        ISalesCampaignService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SalesCampaignResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISalesCampaignService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRegion(
        AssociationRequest request,
        ISalesCampaignService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRegion(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRegion(
    AssociationRequest request,
    ISalesCampaignService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRegion(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOperator_(
        AssociationRequest request,
        ISalesCampaignService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOperator_(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOperator_(
    AssociationRequest request,
    ISalesCampaignService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOperator_(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToQuotes(
        MultipleAssociationRequest request,
        ISalesCampaignService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToQuotes(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromQuotes(
        MultipleAssociationRequest request,
        ISalesCampaignService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromQuotes(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static SalesCampaign mapRequestToSalesCampaign( SalesCampaignRequest request ) {
        var model = new SalesCampaign
        {
            Id = request.Id,
            CampaignCode = request.CampaignCode,
            Status = request.Status,
        };
        return model;
    }

}
