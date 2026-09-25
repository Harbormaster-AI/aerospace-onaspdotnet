
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftFamilyEndpoints
{
    public static IEndpointRouteBuilder MapAircraftFamilyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraftFamily").WithTags("AircraftFamilys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProgram", AssignProgram);
        group.MapPut("/unassignProgram", UnassignProgram);

    group.MapPut("/addToAircraftModels", AddToAircraftModels);
    group.MapPut("/removeFromAircraftModels", RemoveFromAircraftModels);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftFamilyRequest request,
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftFamily( request );

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
        AircraftFamilyRequest request,
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftFamily( request );

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
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {

        var aircraftFamily = await service.Get(identifier, cancellationToken);
        return aircraftFamily is null ? Results.NotFound() : Results.Ok( aircraftFamily );
    }


    private static async Task<IResult> GetAll(
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftFamilyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProgram(
        AssociationRequest request,
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProgram(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProgram(
    AssociationRequest request,
    IAircraftFamilyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProgram(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAircraftModels(
        MultipleAssociationRequest request,
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAircraftModels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAircraftModels(
        MultipleAssociationRequest request,
        IAircraftFamilyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAircraftModels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AircraftFamily mapRequestToAircraftFamily( AircraftFamilyRequest request ) {
        var model = new AircraftFamily
        {
            Id = request.Id,
            Name = request.Name,
            FamilyCode = request.FamilyCode,
        };
        return model;
    }

}
