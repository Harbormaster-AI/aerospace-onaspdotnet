
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class WarrantyEndpoints
{
    public static IEndpointRouteBuilder MapWarrantyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/warranty").WithTags("Warrantys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAircraft", AssignAircraft);
        group.MapPut("/unassignAircraft", UnassignAircraft);


        return app;
    }

    private static async Task<IResult> Create(
        WarrantyRequest request,
        IWarrantyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWarranty( request );

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
        WarrantyRequest request,
        IWarrantyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWarranty( request );

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
        IWarrantyService service,
        CancellationToken cancellationToken) {

        var warranty = await service.Get(identifier, cancellationToken);
        return warranty is null ? Results.NotFound() : Results.Ok( warranty );
    }


    private static async Task<IResult> GetAll(
        IWarrantyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WarrantyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWarrantyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraft(
        AssociationRequest request,
        IWarrantyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraft(
    AssociationRequest request,
    IWarrantyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Warranty mapRequestToWarranty( WarrantyRequest request ) {
        var model = new Warranty
        {
            Id = request.Id,
            CoverageMonths = request.CoverageMonths,
            WarrantyType = request.WarrantyType,
        };
        return model;
    }

}
