
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class EngineTypeEndpoints
{
    public static IEndpointRouteBuilder MapEngineTypeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/engineType").WithTags("EngineTypes");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSupplier", AssignSupplier);
        group.MapPut("/unassignSupplier", UnassignSupplier);

    group.MapPut("/addToCompatibleModels", AddToCompatibleModels);
    group.MapPut("/removeFromCompatibleModels", RemoveFromCompatibleModels);


        return app;
    }

    private static async Task<IResult> Create(
        EngineTypeRequest request,
        IEngineTypeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEngineType( request );

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
        EngineTypeRequest request,
        IEngineTypeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEngineType( request );

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
        IEngineTypeService service,
        CancellationToken cancellationToken) {

        var engineType = await service.Get(identifier, cancellationToken);
        return engineType is null ? Results.NotFound() : Results.Ok( engineType );
    }


    private static async Task<IResult> GetAll(
        IEngineTypeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EngineTypeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEngineTypeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupplier(
        AssociationRequest request,
        IEngineTypeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSupplier(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSupplier(
    AssociationRequest request,
    IEngineTypeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSupplier(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCompatibleModels(
        MultipleAssociationRequest request,
        IEngineTypeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompatibleModels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompatibleModels(
        MultipleAssociationRequest request,
        IEngineTypeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompatibleModels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static EngineType mapRequestToEngineType( EngineTypeRequest request ) {
        var model = new EngineType
        {
            Id = request.Id,
            EngineModelCode = request.EngineModelCode,
            MaxThrustKn = request.MaxThrustKn,
            Category = request.Category,
        };
        return model;
    }

}
