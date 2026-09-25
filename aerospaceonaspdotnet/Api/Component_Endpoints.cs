
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class Component_Endpoints
{
    public static IEndpointRouteBuilder MapComponent_Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/component_").WithTags("Component_s");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSupplier", AssignSupplier);
        group.MapPut("/unassignSupplier", UnassignSupplier);


        return app;
    }

    private static async Task<IResult> Create(
        Component_Request request,
        IComponent_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComponent_( request );

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
        Component_Request request,
        IComponent_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComponent_( request );

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
        IComponent_Service service,
        CancellationToken cancellationToken) {

        var component_ = await service.Get(identifier, cancellationToken);
        return component_ is null ? Results.NotFound() : Results.Ok( component_ );
    }


    private static async Task<IResult> GetAll(
        IComponent_Service service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( Component_Response.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IComponent_Service service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupplier(
        AssociationRequest request,
        IComponent_Service service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSupplier(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSupplier(
    AssociationRequest request,
    IComponent_Service service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSupplier(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Component_ mapRequestToComponent_( Component_Request request ) {
        var model = new Component_
        {
            Id = request.Id,
            PartNumber = request.PartNumber,
            Name = request.Name,
            ComponentCategory = request.ComponentCategory,
            SerializationMethod = request.SerializationMethod,
        };
        return model;
    }

}
