
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class QuoteEndpoints
{
    public static IEndpointRouteBuilder MapQuoteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/quote").WithTags("Quotes");

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
        QuoteRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToQuote( request );

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
        QuoteRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToQuote( request );

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
        IQuoteService service,
        CancellationToken cancellationToken) {

        var quote = await service.Get(identifier, cancellationToken);
        return quote is null ? Results.NotFound() : Results.Ok( quote );
    }


    private static async Task<IResult> GetAll(
        IQuoteService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( QuoteResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IQuoteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraftOrder(
        AssociationRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraftOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraftOrder(
    AssociationRequest request,
    IQuoteService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraftOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Quote mapRequestToQuote( QuoteRequest request ) {
        var model = new Quote
        {
            Id = request.Id,
            QuoteNumber = request.QuoteNumber,
            TotalAmount = request.TotalAmount,
        };
        return model;
    }

}
