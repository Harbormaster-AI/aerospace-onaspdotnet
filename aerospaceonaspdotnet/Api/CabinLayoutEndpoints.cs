
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class CabinLayoutEndpoints
{
    public static IEndpointRouteBuilder MapCabinLayoutEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cabinLayout").WithTags("CabinLayouts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);

    group.MapPut("/addToAircraft", AddToAircraft);
    group.MapPut("/removeFromAircraft", RemoveFromAircraft);

    group.MapPut("/addToOptions", AddToOptions);
    group.MapPut("/removeFromOptions", RemoveFromOptions);


        return app;
    }

    private static async Task<IResult> Create(
        CabinLayoutRequest request,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCabinLayout( request );

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
        CabinLayoutRequest request,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCabinLayout( request );

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
        ICabinLayoutService service,
        CancellationToken cancellationToken) {

        var cabinLayout = await service.Get(identifier, cancellationToken);
        return cabinLayout is null ? Results.NotFound() : Results.Ok( cabinLayout );
    }


    private static async Task<IResult> GetAll(
        ICabinLayoutService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CabinLayoutResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    ICabinLayoutService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAircraft(
        MultipleAssociationRequest request,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAircraft(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAircraft(
        MultipleAssociationRequest request,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAircraft(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOptions(
        MultipleAssociationRequest request,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOptions(
        MultipleAssociationRequest request,
        ICabinLayoutService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CabinLayout mapRequestToCabinLayout( CabinLayoutRequest request ) {
        var model = new CabinLayout
        {
            Id = request.Id,
            LayoutCode = request.LayoutCode,
            TotalSeats = request.TotalSeats,
            ClassLayout = request.ClassLayout,
        };
        return model;
    }

}
