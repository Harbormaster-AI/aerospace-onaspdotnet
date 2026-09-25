
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class PurchaseAgreementEndpoints
{
    public static IEndpointRouteBuilder MapPurchaseAgreementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchaseAgreement").WithTags("PurchaseAgreements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAircraftOrder", AssignAircraftOrder);
        group.MapPut("/unassignAircraftOrder", UnassignAircraftOrder);


        return app;
    }

    private static async Task<IResult> Create(
        PurchaseAgreementRequest request,
        IPurchaseAgreementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPurchaseAgreement( request );

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
        PurchaseAgreementRequest request,
        IPurchaseAgreementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPurchaseAgreement( request );

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
        IPurchaseAgreementService service,
        CancellationToken cancellationToken) {

        var purchaseAgreement = await service.Get(identifier, cancellationToken);
        return purchaseAgreement is null ? Results.NotFound() : Results.Ok( purchaseAgreement );
    }


    private static async Task<IResult> GetAll(
        IPurchaseAgreementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PurchaseAgreementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPurchaseAgreementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraftOrder(
        AssociationRequest request,
        IPurchaseAgreementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraftOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraftOrder(
    AssociationRequest request,
    IPurchaseAgreementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraftOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PurchaseAgreement mapRequestToPurchaseAgreement( PurchaseAgreementRequest request ) {
        var model = new PurchaseAgreement
        {
            Id = request.Id,
            AgreementNumber = request.AgreementNumber,
            EffectiveDate = request.EffectiveDate,
        };
        return model;
    }

}
