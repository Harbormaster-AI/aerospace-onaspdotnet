
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftModelEndpoints
{
    public static IEndpointRouteBuilder MapAircraftModelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraftModel").WithTags("AircraftModels");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignFamily", AssignFamily);
        group.MapPut("/unassignFamily", UnassignFamily);

    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);

    group.MapPut("/addToEngineTypes", AddToEngineTypes);
    group.MapPut("/removeFromEngineTypes", RemoveFromEngineTypes);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftModelRequest request,
        IAircraftModelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftModel( request );

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
        AircraftModelRequest request,
        IAircraftModelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftModel( request );

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
        IAircraftModelService service,
        CancellationToken cancellationToken) {

        var aircraftModel = await service.Get(identifier, cancellationToken);
        return aircraftModel is null ? Results.NotFound() : Results.Ok( aircraftModel );
    }


    private static async Task<IResult> GetAll(
        IAircraftModelService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftModelResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftModelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFamily(
        AssociationRequest request,
        IAircraftModelService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFamily(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFamily(
    AssociationRequest request,
    IAircraftModelService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFamily(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        IAircraftModelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        IAircraftModelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEngineTypes(
        MultipleAssociationRequest request,
        IAircraftModelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEngineTypes(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEngineTypes(
        MultipleAssociationRequest request,
        IAircraftModelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEngineTypes(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AircraftModel mapRequestToAircraftModel( AircraftModelRequest request ) {
        var model = new AircraftModel
        {
            Id = request.Id,
            Name = request.Name,
            ModelDesignation = request.ModelDesignation,
            AircraftType = request.AircraftType,
        };
        return model;
    }

}
