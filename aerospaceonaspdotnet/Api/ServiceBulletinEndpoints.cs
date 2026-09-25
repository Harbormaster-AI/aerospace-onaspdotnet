
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class ServiceBulletinEndpoints
{
    public static IEndpointRouteBuilder MapServiceBulletinEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/serviceBulletin").WithTags("ServiceBulletins");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToWorkOrders", AddToWorkOrders);
    group.MapPut("/removeFromWorkOrders", RemoveFromWorkOrders);

    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);


        return app;
    }

    private static async Task<IResult> Create(
        ServiceBulletinRequest request,
        IServiceBulletinService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToServiceBulletin( request );

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
        ServiceBulletinRequest request,
        IServiceBulletinService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToServiceBulletin( request );

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
        IServiceBulletinService service,
        CancellationToken cancellationToken) {

        var serviceBulletin = await service.Get(identifier, cancellationToken);
        return serviceBulletin is null ? Results.NotFound() : Results.Ok( serviceBulletin );
    }


    private static async Task<IResult> GetAll(
        IServiceBulletinService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ServiceBulletinResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IServiceBulletinService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToWorkOrders(
        MultipleAssociationRequest request,
        IServiceBulletinService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkOrders(
        MultipleAssociationRequest request,
        IServiceBulletinService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        IServiceBulletinService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        IServiceBulletinService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ServiceBulletin mapRequestToServiceBulletin( ServiceBulletinRequest request ) {
        var model = new ServiceBulletin
        {
            Id = request.Id,
            BulletinNumber = request.BulletinNumber,
            Category = request.Category,
        };
        return model;
    }

}
