
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AirworthinessDirectiveEndpoints
{
    public static IEndpointRouteBuilder MapAirworthinessDirectiveEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/airworthinessDirective").WithTags("AirworthinessDirectives");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToWorkOrders", AddToWorkOrders);
    group.MapPut("/removeFromWorkOrders", RemoveFromWorkOrders);


        return app;
    }

    private static async Task<IResult> Create(
        AirworthinessDirectiveRequest request,
        IAirworthinessDirectiveService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAirworthinessDirective( request );

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
        AirworthinessDirectiveRequest request,
        IAirworthinessDirectiveService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAirworthinessDirective( request );

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
        IAirworthinessDirectiveService service,
        CancellationToken cancellationToken) {

        var airworthinessDirective = await service.Get(identifier, cancellationToken);
        return airworthinessDirective is null ? Results.NotFound() : Results.Ok( airworthinessDirective );
    }


    private static async Task<IResult> GetAll(
        IAirworthinessDirectiveService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AirworthinessDirectiveResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAirworthinessDirectiveService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToWorkOrders(
        MultipleAssociationRequest request,
        IAirworthinessDirectiveService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkOrders(
        MultipleAssociationRequest request,
        IAirworthinessDirectiveService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AirworthinessDirective mapRequestToAirworthinessDirective( AirworthinessDirectiveRequest request ) {
        var model = new AirworthinessDirective
        {
            Id = request.Id,
            DirectiveNumber = request.DirectiveNumber,
            Title = request.Title,
        };
        return model;
    }

}
