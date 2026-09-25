
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class SalesRegionEndpoints
{
    public static IEndpointRouteBuilder MapSalesRegionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/salesRegion").WithTags("SalesRegions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToOperators", AddToOperators);
    group.MapPut("/removeFromOperators", RemoveFromOperators);

    group.MapPut("/addToSalesCampaigns", AddToSalesCampaigns);
    group.MapPut("/removeFromSalesCampaigns", RemoveFromSalesCampaigns);


        return app;
    }

    private static async Task<IResult> Create(
        SalesRegionRequest request,
        ISalesRegionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesRegion( request );

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
        SalesRegionRequest request,
        ISalesRegionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesRegion( request );

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
        ISalesRegionService service,
        CancellationToken cancellationToken) {

        var salesRegion = await service.Get(identifier, cancellationToken);
        return salesRegion is null ? Results.NotFound() : Results.Ok( salesRegion );
    }


    private static async Task<IResult> GetAll(
        ISalesRegionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SalesRegionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISalesRegionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToOperators(
        MultipleAssociationRequest request,
        ISalesRegionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOperators(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOperators(
        MultipleAssociationRequest request,
        ISalesRegionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOperators(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSalesCampaigns(
        MultipleAssociationRequest request,
        ISalesRegionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSalesCampaigns(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSalesCampaigns(
        MultipleAssociationRequest request,
        ISalesRegionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSalesCampaigns(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static SalesRegion mapRequestToSalesRegion( SalesRegionRequest request ) {
        var model = new SalesRegion
        {
            Id = request.Id,
            Name = request.Name,
            RegionCode = request.RegionCode,
        };
        return model;
    }

}
