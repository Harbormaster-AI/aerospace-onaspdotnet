
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftProgramEndpoints
{
    public static IEndpointRouteBuilder MapAircraftProgramEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraftProgram").WithTags("AircraftPrograms");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignManufacturer", AssignManufacturer);
        group.MapPut("/unassignManufacturer", UnassignManufacturer);
        group.MapPut("/assignTypeCertificate", AssignTypeCertificate);
        group.MapPut("/unassignTypeCertificate", UnassignTypeCertificate);

    group.MapPut("/addToAircraftFamilies", AddToAircraftFamilies);
    group.MapPut("/removeFromAircraftFamilies", RemoveFromAircraftFamilies);

    group.MapPut("/addToKeySuppliers", AddToKeySuppliers);
    group.MapPut("/removeFromKeySuppliers", RemoveFromKeySuppliers);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftProgramRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftProgram( request );

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
        AircraftProgramRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftProgram( request );

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
        IAircraftProgramService service,
        CancellationToken cancellationToken) {

        var aircraftProgram = await service.Get(identifier, cancellationToken);
        return aircraftProgram is null ? Results.NotFound() : Results.Ok( aircraftProgram );
    }


    private static async Task<IResult> GetAll(
        IAircraftProgramService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftProgramResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignManufacturer(
        AssociationRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignManufacturer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignManufacturer(
    AssociationRequest request,
    IAircraftProgramService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignManufacturer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTypeCertificate(
        AssociationRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTypeCertificate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTypeCertificate(
    AssociationRequest request,
    IAircraftProgramService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTypeCertificate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAircraftFamilies(
        MultipleAssociationRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAircraftFamilies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAircraftFamilies(
        MultipleAssociationRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAircraftFamilies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToKeySuppliers(
        MultipleAssociationRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToKeySuppliers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromKeySuppliers(
        MultipleAssociationRequest request,
        IAircraftProgramService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromKeySuppliers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AircraftProgram mapRequestToAircraftProgram( AircraftProgramRequest request ) {
        var model = new AircraftProgram
        {
            Id = request.Id,
            Name = request.Name,
            ProgramCode = request.ProgramCode,
            EntryIntoServiceYear = request.EntryIntoServiceYear,
            Status = request.Status,
        };
        return model;
    }

}
