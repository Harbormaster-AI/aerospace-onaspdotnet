
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class MROFacilityEndpoints
{
    public static IEndpointRouteBuilder MapMROFacilityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/mROFacility").WithTags("MROFacilitys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToAppointments", AddToAppointments);
    group.MapPut("/removeFromAppointments", RemoveFromAppointments);

    group.MapPut("/addToWorkOrders", AddToWorkOrders);
    group.MapPut("/removeFromWorkOrders", RemoveFromWorkOrders);


        return app;
    }

    private static async Task<IResult> Create(
        MROFacilityRequest request,
        IMROFacilityService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMROFacility( request );

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
        MROFacilityRequest request,
        IMROFacilityService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMROFacility( request );

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
        IMROFacilityService service,
        CancellationToken cancellationToken) {

        var mROFacility = await service.Get(identifier, cancellationToken);
        return mROFacility is null ? Results.NotFound() : Results.Ok( mROFacility );
    }


    private static async Task<IResult> GetAll(
        IMROFacilityService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MROFacilityResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMROFacilityService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAppointments(
        MultipleAssociationRequest request,
        IMROFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAppointments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAppointments(
        MultipleAssociationRequest request,
        IMROFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAppointments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWorkOrders(
        MultipleAssociationRequest request,
        IMROFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkOrders(
        MultipleAssociationRequest request,
        IMROFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static MROFacility mapRequestToMROFacility( MROFacilityRequest request ) {
        var model = new MROFacility
        {
            Id = request.Id,
            Name = request.Name,
            ApprovalScope = request.ApprovalScope,
            Address = request.Address,
        };
        return model;
    }

}
