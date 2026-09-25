
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AvionicsSuiteEndpoints
{
    public static IEndpointRouteBuilder MapAvionicsSuiteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/avionicsSuite").WithTags("AvionicsSuites");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSupplier", AssignSupplier);
        group.MapPut("/unassignSupplier", UnassignSupplier);

    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);

    group.MapPut("/addToSoftwareLoads", AddToSoftwareLoads);
    group.MapPut("/removeFromSoftwareLoads", RemoveFromSoftwareLoads);


        return app;
    }

    private static async Task<IResult> Create(
        AvionicsSuiteRequest request,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAvionicsSuite( request );

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
        AvionicsSuiteRequest request,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAvionicsSuite( request );

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
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {

        var avionicsSuite = await service.Get(identifier, cancellationToken);
        return avionicsSuite is null ? Results.NotFound() : Results.Ok( avionicsSuite );
    }


    private static async Task<IResult> GetAll(
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AvionicsSuiteResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupplier(
        AssociationRequest request,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSupplier(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSupplier(
    AssociationRequest request,
    IAvionicsSuiteService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSupplier(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSoftwareLoads(
        MultipleAssociationRequest request,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSoftwareLoads(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSoftwareLoads(
        MultipleAssociationRequest request,
        IAvionicsSuiteService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSoftwareLoads(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AvionicsSuite mapRequestToAvionicsSuite( AvionicsSuiteRequest request ) {
        var model = new AvionicsSuite
        {
            Id = request.Id,
            SuiteName = request.SuiteName,
            SoftwareBaseline = request.SoftwareBaseline,
        };
        return model;
    }

}
