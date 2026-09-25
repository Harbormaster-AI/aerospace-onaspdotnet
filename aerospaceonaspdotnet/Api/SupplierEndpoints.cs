
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class SupplierEndpoints
{
    public static IEndpointRouteBuilder MapSupplierEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/supplier").WithTags("Suppliers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToManufacturers", AddToManufacturers);
    group.MapPut("/removeFromManufacturers", RemoveFromManufacturers);

    group.MapPut("/addToComponents", AddToComponents);
    group.MapPut("/removeFromComponents", RemoveFromComponents);

    group.MapPut("/addToEngineTypes", AddToEngineTypes);
    group.MapPut("/removeFromEngineTypes", RemoveFromEngineTypes);

    group.MapPut("/addToAvionicsSuites", AddToAvionicsSuites);
    group.MapPut("/removeFromAvionicsSuites", RemoveFromAvionicsSuites);

    group.MapPut("/addToApus", AddToApus);
    group.MapPut("/removeFromApus", RemoveFromApus);

    group.MapPut("/addToLandingGears", AddToLandingGears);
    group.MapPut("/removeFromLandingGears", RemoveFromLandingGears);


        return app;
    }

    private static async Task<IResult> Create(
        SupplierRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSupplier( request );

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
        SupplierRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSupplier( request );

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
        ISupplierService service,
        CancellationToken cancellationToken) {

        var supplier = await service.Get(identifier, cancellationToken);
        return supplier is null ? Results.NotFound() : Results.Ok( supplier );
    }


    private static async Task<IResult> GetAll(
        ISupplierService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SupplierResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToManufacturers(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToManufacturers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromManufacturers(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromManufacturers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToComponents(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToComponents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromComponents(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromComponents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEngineTypes(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEngineTypes(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEngineTypes(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEngineTypes(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAvionicsSuites(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAvionicsSuites(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAvionicsSuites(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAvionicsSuites(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToApus(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApus(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApus(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApus(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLandingGears(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLandingGears(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLandingGears(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLandingGears(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Supplier mapRequestToSupplier( SupplierRequest request ) {
        var model = new Supplier
        {
            Id = request.Id,
            Name = request.Name,
            SupplierType = request.SupplierType,
            ApprovalStatus = request.ApprovalStatus,
        };
        return model;
    }

}
