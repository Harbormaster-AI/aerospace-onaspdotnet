
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class RegistrationEndpoints
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/registration").WithTags("Registrations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAircraft", AssignAircraft);
        group.MapPut("/unassignAircraft", UnassignAircraft);


        return app;
    }

    private static async Task<IResult> Create(
        RegistrationRequest request,
        IRegistrationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRegistration( request );

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
        RegistrationRequest request,
        IRegistrationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRegistration( request );

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
        IRegistrationService service,
        CancellationToken cancellationToken) {

        var registration = await service.Get(identifier, cancellationToken);
        return registration is null ? Results.NotFound() : Results.Ok( registration );
    }


    private static async Task<IResult> GetAll(
        IRegistrationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RegistrationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRegistrationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAircraft(
        AssociationRequest request,
        IRegistrationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAircraft(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAircraft(
    AssociationRequest request,
    IRegistrationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAircraft(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Registration mapRequestToRegistration( RegistrationRequest request ) {
        var model = new Registration
        {
            Id = request.Id,
            TailNumber = request.TailNumber,
            RegistryCountry = request.RegistryCountry,
        };
        return model;
    }

}
