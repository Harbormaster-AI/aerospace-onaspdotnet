
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AerospaceManufacturerEndpoints
{
    public static IEndpointRouteBuilder MapAerospaceManufacturerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aerospaceManufacturer").WithTags("AerospaceManufacturers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToPrograms", AddToPrograms);
    group.MapPut("/removeFromPrograms", RemoveFromPrograms);

    group.MapPut("/addToPlants", AddToPlants);
    group.MapPut("/removeFromPlants", RemoveFromPlants);

    group.MapPut("/addToSuppliers", AddToSuppliers);
    group.MapPut("/removeFromSuppliers", RemoveFromSuppliers);

    group.MapPut("/addToProductionCertificates", AddToProductionCertificates);
    group.MapPut("/removeFromProductionCertificates", RemoveFromProductionCertificates);


        return app;
    }

    private static async Task<IResult> Create(
        AerospaceManufacturerRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAerospaceManufacturer( request );

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
        AerospaceManufacturerRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAerospaceManufacturer( request );

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
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {

        var aerospaceManufacturer = await service.Get(identifier, cancellationToken);
        return aerospaceManufacturer is null ? Results.NotFound() : Results.Ok( aerospaceManufacturer );
    }


    private static async Task<IResult> GetAll(
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AerospaceManufacturerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPrograms(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPrograms(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPrograms(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPrograms(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPlants(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlants(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSuppliers(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSuppliers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSuppliers(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSuppliers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProductionCertificates(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProductionCertificates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProductionCertificates(
        MultipleAssociationRequest request,
        IAerospaceManufacturerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProductionCertificates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AerospaceManufacturer mapRequestToAerospaceManufacturer( AerospaceManufacturerRequest request ) {
        var model = new AerospaceManufacturer
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
        return model;
    }

}
