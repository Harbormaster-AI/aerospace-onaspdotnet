
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class LandingGearEndpoints
{
    public static IEndpointRouteBuilder MapLandingGearEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/landingGear").WithTags("LandingGears");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSupplier", AssignSupplier);
        group.MapPut("/unassignSupplier", UnassignSupplier);

    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);


        return app;
    }

    private static async Task<IResult> Create(
        LandingGearRequest request,
        ILandingGearService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLandingGear( request );

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
        LandingGearRequest request,
        ILandingGearService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLandingGear( request );

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
        ILandingGearService service,
        CancellationToken cancellationToken) {

        var landingGear = await service.Get(identifier, cancellationToken);
        return landingGear is null ? Results.NotFound() : Results.Ok( landingGear );
    }


    private static async Task<IResult> GetAll(
        ILandingGearService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LandingGearResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILandingGearService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupplier(
        AssociationRequest request,
        ILandingGearService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSupplier(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSupplier(
    AssociationRequest request,
    ILandingGearService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSupplier(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        ILandingGearService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        ILandingGearService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static LandingGear mapRequestToLandingGear( LandingGearRequest request ) {
        var model = new LandingGear
        {
            Id = request.Id,
            SupplierPartNumber = request.SupplierPartNumber,
            GearType = request.GearType,
        };
        return model;
    }

}
