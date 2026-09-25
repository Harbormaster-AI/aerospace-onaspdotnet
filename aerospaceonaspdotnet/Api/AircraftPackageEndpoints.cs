
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftPackageEndpoints
{
    public static IEndpointRouteBuilder MapAircraftPackageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraftPackage").WithTags("AircraftPackages");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToOptions", AddToOptions);
    group.MapPut("/removeFromOptions", RemoveFromOptions);

    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftPackageRequest request,
        IAircraftPackageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftPackage( request );

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
        AircraftPackageRequest request,
        IAircraftPackageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftPackage( request );

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
        IAircraftPackageService service,
        CancellationToken cancellationToken) {

        var aircraftPackage = await service.Get(identifier, cancellationToken);
        return aircraftPackage is null ? Results.NotFound() : Results.Ok( aircraftPackage );
    }


    private static async Task<IResult> GetAll(
        IAircraftPackageService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftPackageResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftPackageService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToOptions(
        MultipleAssociationRequest request,
        IAircraftPackageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOptions(
        MultipleAssociationRequest request,
        IAircraftPackageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        IAircraftPackageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        IAircraftPackageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AircraftPackage mapRequestToAircraftPackage( AircraftPackageRequest request ) {
        var model = new AircraftPackage
        {
            Id = request.Id,
            Name = request.Name,
            PackageType = request.PackageType,
        };
        return model;
    }

}
