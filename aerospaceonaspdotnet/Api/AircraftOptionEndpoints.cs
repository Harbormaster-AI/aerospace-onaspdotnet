
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftOptionEndpoints
{
    public static IEndpointRouteBuilder MapAircraftOptionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraftOption").WithTags("AircraftOptions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);

    group.MapPut("/addToPackages", AddToPackages);
    group.MapPut("/removeFromPackages", RemoveFromPackages);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftOptionRequest request,
        IAircraftOptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftOption( request );

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
        AircraftOptionRequest request,
        IAircraftOptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftOption( request );

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
        IAircraftOptionService service,
        CancellationToken cancellationToken) {

        var aircraftOption = await service.Get(identifier, cancellationToken);
        return aircraftOption is null ? Results.NotFound() : Results.Ok( aircraftOption );
    }


    private static async Task<IResult> GetAll(
        IAircraftOptionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftOptionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftOptionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        IAircraftOptionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        IAircraftOptionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPackages(
        MultipleAssociationRequest request,
        IAircraftOptionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPackages(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPackages(
        MultipleAssociationRequest request,
        IAircraftOptionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPackages(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AircraftOption mapRequestToAircraftOption( AircraftOptionRequest request ) {
        var model = new AircraftOption
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            OptionCategory = request.OptionCategory,
        };
        return model;
    }

}
