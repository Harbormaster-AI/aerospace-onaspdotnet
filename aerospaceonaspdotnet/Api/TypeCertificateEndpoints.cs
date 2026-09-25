
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class TypeCertificateEndpoints
{
    public static IEndpointRouteBuilder MapTypeCertificateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/typeCertificate").WithTags("TypeCertificates");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProgram", AssignProgram);
        group.MapPut("/unassignProgram", UnassignProgram);


        return app;
    }

    private static async Task<IResult> Create(
        TypeCertificateRequest request,
        ITypeCertificateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTypeCertificate( request );

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
        TypeCertificateRequest request,
        ITypeCertificateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTypeCertificate( request );

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
        ITypeCertificateService service,
        CancellationToken cancellationToken) {

        var typeCertificate = await service.Get(identifier, cancellationToken);
        return typeCertificate is null ? Results.NotFound() : Results.Ok( typeCertificate );
    }


    private static async Task<IResult> GetAll(
        ITypeCertificateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TypeCertificateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITypeCertificateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProgram(
        AssociationRequest request,
        ITypeCertificateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProgram(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProgram(
    AssociationRequest request,
    ITypeCertificateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProgram(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static TypeCertificate mapRequestToTypeCertificate( TypeCertificateRequest request ) {
        var model = new TypeCertificate
        {
            Id = request.Id,
            CertificateNumber = request.CertificateNumber,
            Authority = request.Authority,
        };
        return model;
    }

}
