
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Api;

public static class AircraftVariantEndpoints
{
    public static IEndpointRouteBuilder MapAircraftVariantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aircraftVariant").WithTags("AircraftVariants");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignModel_", AssignModel_);
        group.MapPut("/unassignModel_", UnassignModel_);
        group.MapPut("/assignEngineType", AssignEngineType);
        group.MapPut("/unassignEngineType", UnassignEngineType);
        group.MapPut("/assignAvionicsSuite", AssignAvionicsSuite);
        group.MapPut("/unassignAvionicsSuite", UnassignAvionicsSuite);
        group.MapPut("/assignApu", AssignApu);
        group.MapPut("/unassignApu", UnassignApu);
        group.MapPut("/assignLandingGear", AssignLandingGear);
        group.MapPut("/unassignLandingGear", UnassignLandingGear);

    group.MapPut("/addToCabinLayouts", AddToCabinLayouts);
    group.MapPut("/removeFromCabinLayouts", RemoveFromCabinLayouts);

    group.MapPut("/addToOptions", AddToOptions);
    group.MapPut("/removeFromOptions", RemoveFromOptions);

    group.MapPut("/addToPackages", AddToPackages);
    group.MapPut("/removeFromPackages", RemoveFromPackages);


        return app;
    }

    private static async Task<IResult> Create(
        AircraftVariantRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftVariant( request );

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
        AircraftVariantRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAircraftVariant( request );

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
        IAircraftVariantService service,
        CancellationToken cancellationToken) {

        var aircraftVariant = await service.Get(identifier, cancellationToken);
        return aircraftVariant is null ? Results.NotFound() : Results.Ok( aircraftVariant );
    }


    private static async Task<IResult> GetAll(
        IAircraftVariantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AircraftVariantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignModel_(
        AssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignModel_(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignModel_(
    AssociationRequest request,
    IAircraftVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignModel_(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEngineType(
        AssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEngineType(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEngineType(
    AssociationRequest request,
    IAircraftVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEngineType(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAvionicsSuite(
        AssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAvionicsSuite(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAvionicsSuite(
    AssociationRequest request,
    IAircraftVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAvionicsSuite(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApu(
        AssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignApu(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignApu(
    AssociationRequest request,
    IAircraftVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignApu(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLandingGear(
        AssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLandingGear(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLandingGear(
    AssociationRequest request,
    IAircraftVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLandingGear(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCabinLayouts(
        MultipleAssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCabinLayouts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCabinLayouts(
        MultipleAssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCabinLayouts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOptions(
        MultipleAssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOptions(
        MultipleAssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPackages(
        MultipleAssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPackages(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPackages(
        MultipleAssociationRequest request,
        IAircraftVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPackages(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AircraftVariant mapRequestToAircraftVariant( AircraftVariantRequest request ) {
        var model = new AircraftVariant
        {
            Id = request.Id,
            VariantCode = request.VariantCode,
            RangeNm = request.RangeNm,
            MaxTakeoffWeightKg = request.MaxTakeoffWeightKg,
        };
        return model;
    }

}
