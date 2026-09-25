
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class ProductionCertificateEndpoints
{
    public static IEndpointRouteBuilder MapProductionCertificateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productionCertificate").WithTags("ProductionCertificates");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignManufacturer", AssignManufacturer);
        group.MapPut("/unassignManufacturer", UnassignManufacturer);


        return app;
    }

    private static async Task<IResult> Create(
        ProductionCertificateRequest request,
        IProductionCertificateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionCertificate( request );

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
        ProductionCertificateRequest request,
        IProductionCertificateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionCertificate( request );

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
        IProductionCertificateService service,
        CancellationToken cancellationToken) {

        var productionCertificate = await service.Get(identifier, cancellationToken);
        return productionCertificate is null ? Results.NotFound() : Results.Ok( productionCertificate );
    }


    private static async Task<IResult> GetAll(
        IProductionCertificateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductionCertificateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductionCertificateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignManufacturer(
        AssociationRequest request,
        IProductionCertificateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignManufacturer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignManufacturer(
    AssociationRequest request,
    IProductionCertificateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignManufacturer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ProductionCertificate mapRequestToProductionCertificate( ProductionCertificateRequest request ) {
        var model = new ProductionCertificate
        {
            Id = request.Id,
            CertificateNumber = request.CertificateNumber,
            Authority = request.Authority,
        };
        return model;
    }

}
