using aerospaceonaspdotnet.Api;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Service;
using aerospaceonaspdotnet.Telemetry;
using OpenTelemetry.Metrics;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("aerospaceonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

    builder.Services.AddScoped<IAerospaceManufacturerRepository, AerospaceManufacturerRepository>();
    builder.Services.AddScoped<IAircraftProgramRepository, AircraftProgramRepository>();
    builder.Services.AddScoped<IAircraftFamilyRepository, AircraftFamilyRepository>();
    builder.Services.AddScoped<IAircraftModelRepository, AircraftModelRepository>();
    builder.Services.AddScoped<IEngineTypeRepository, EngineTypeRepository>();
    builder.Services.AddScoped<IAircraftVariantRepository, AircraftVariantRepository>();
    builder.Services.AddScoped<IAvionicsSuiteRepository, AvionicsSuiteRepository>();
    builder.Services.AddScoped<IAPURepository, APURepository>();
    builder.Services.AddScoped<ILandingGearRepository, LandingGearRepository>();
    builder.Services.AddScoped<IAircraftOptionRepository, AircraftOptionRepository>();
    builder.Services.AddScoped<IAircraftPackageRepository, AircraftPackageRepository>();
    builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
    builder.Services.AddScoped<IComponent_Repository, Component_Repository>();
    builder.Services.AddScoped<IPlantRepository, PlantRepository>();
    builder.Services.AddScoped<IProductionLineRepository, ProductionLineRepository>();
    builder.Services.AddScoped<IWorkCenterRepository, WorkCenterRepository>();
    builder.Services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
    builder.Services.AddScoped<IBuildScheduleRepository, BuildScheduleRepository>();
    builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
    builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
    builder.Services.AddScoped<IOperator_Repository, Operator_Repository>();
    builder.Services.AddScoped<IAircraftOrderRepository, AircraftOrderRepository>();
    builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
    builder.Services.AddScoped<IPurchaseAgreementRepository, PurchaseAgreementRepository>();
    builder.Services.AddScoped<IAircraftRepository, AircraftRepository>();
    builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
    builder.Services.AddScoped<IWarrantyRepository, WarrantyRepository>();
    builder.Services.AddScoped<ICabinLayoutRepository, CabinLayoutRepository>();
    builder.Services.AddScoped<IMROFacilityRepository, MROFacilityRepository>();
    builder.Services.AddScoped<IMaintenanceAppointmentRepository, MaintenanceAppointmentRepository>();
    builder.Services.AddScoped<IMaintenanceWorkOrderRepository, MaintenanceWorkOrderRepository>();
    builder.Services.AddScoped<IAirworthinessDirectiveRepository, AirworthinessDirectiveRepository>();
    builder.Services.AddScoped<IServiceBulletinRepository, ServiceBulletinRepository>();
    builder.Services.AddScoped<IConnectedAircraftRepository, ConnectedAircraftRepository>();
    builder.Services.AddScoped<IFlightHealthEventRepository, FlightHealthEventRepository>();
    builder.Services.AddScoped<ISoftwareLoadRepository, SoftwareLoadRepository>();
    builder.Services.AddScoped<ITypeCertificateRepository, TypeCertificateRepository>();
    builder.Services.AddScoped<IProductionCertificateRepository, ProductionCertificateRepository>();
    builder.Services.AddScoped<ISalesRegionRepository, SalesRegionRepository>();
    builder.Services.AddScoped<ISalesCampaignRepository, SalesCampaignRepository>();

    builder.Services.AddScoped<IAerospaceManufacturerService, AerospaceManufacturerService>();
    builder.Services.AddScoped<IAircraftProgramService, AircraftProgramService>();
    builder.Services.AddScoped<IAircraftFamilyService, AircraftFamilyService>();
    builder.Services.AddScoped<IAircraftModelService, AircraftModelService>();
    builder.Services.AddScoped<IEngineTypeService, EngineTypeService>();
    builder.Services.AddScoped<IAircraftVariantService, AircraftVariantService>();
    builder.Services.AddScoped<IAvionicsSuiteService, AvionicsSuiteService>();
    builder.Services.AddScoped<IAPUService, APUService>();
    builder.Services.AddScoped<ILandingGearService, LandingGearService>();
    builder.Services.AddScoped<IAircraftOptionService, AircraftOptionService>();
    builder.Services.AddScoped<IAircraftPackageService, AircraftPackageService>();
    builder.Services.AddScoped<ISupplierService, SupplierService>();
    builder.Services.AddScoped<IComponent_Service, Component_Service>();
    builder.Services.AddScoped<IPlantService, PlantService>();
    builder.Services.AddScoped<IProductionLineService, ProductionLineService>();
    builder.Services.AddScoped<IWorkCenterService, WorkCenterService>();
    builder.Services.AddScoped<IProductionOrderService, ProductionOrderService>();
    builder.Services.AddScoped<IBuildScheduleService, BuildScheduleService>();
    builder.Services.AddScoped<IWarehouseService, WarehouseService>();
    builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();
    builder.Services.AddScoped<IOperator_Service, Operator_Service>();
    builder.Services.AddScoped<IAircraftOrderService, AircraftOrderService>();
    builder.Services.AddScoped<IQuoteService, QuoteService>();
    builder.Services.AddScoped<IPurchaseAgreementService, PurchaseAgreementService>();
    builder.Services.AddScoped<IAircraftService, AircraftService>();
    builder.Services.AddScoped<IRegistrationService, RegistrationService>();
    builder.Services.AddScoped<IWarrantyService, WarrantyService>();
    builder.Services.AddScoped<ICabinLayoutService, CabinLayoutService>();
    builder.Services.AddScoped<IMROFacilityService, MROFacilityService>();
    builder.Services.AddScoped<IMaintenanceAppointmentService, MaintenanceAppointmentService>();
    builder.Services.AddScoped<IMaintenanceWorkOrderService, MaintenanceWorkOrderService>();
    builder.Services.AddScoped<IAirworthinessDirectiveService, AirworthinessDirectiveService>();
    builder.Services.AddScoped<IServiceBulletinService, ServiceBulletinService>();
    builder.Services.AddScoped<IConnectedAircraftService, ConnectedAircraftService>();
    builder.Services.AddScoped<IFlightHealthEventService, FlightHealthEventService>();
    builder.Services.AddScoped<ISoftwareLoadService, SoftwareLoadService>();
    builder.Services.AddScoped<ITypeCertificateService, TypeCertificateService>();
    builder.Services.AddScoped<IProductionCertificateService, ProductionCertificateService>();
    builder.Services.AddScoped<ISalesRegionService, SalesRegionService>();
    builder.Services.AddScoped<ISalesCampaignService, SalesCampaignService>();

// apply the service resolver
builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

builder.Services.AddSingleton<ApplicationTelemetry>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Harbormaster.Application");

        metrics.AddPrometheusExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapAerospaceManufacturerEndpoints();
    app.MapAircraftProgramEndpoints();
    app.MapAircraftFamilyEndpoints();
    app.MapAircraftModelEndpoints();
    app.MapEngineTypeEndpoints();
    app.MapAircraftVariantEndpoints();
    app.MapAvionicsSuiteEndpoints();
    app.MapAPUEndpoints();
    app.MapLandingGearEndpoints();
    app.MapAircraftOptionEndpoints();
    app.MapAircraftPackageEndpoints();
    app.MapSupplierEndpoints();
    app.MapComponent_Endpoints();
    app.MapPlantEndpoints();
    app.MapProductionLineEndpoints();
    app.MapWorkCenterEndpoints();
    app.MapProductionOrderEndpoints();
    app.MapBuildScheduleEndpoints();
    app.MapWarehouseEndpoints();
    app.MapInventoryItemEndpoints();
    app.MapOperator_Endpoints();
    app.MapAircraftOrderEndpoints();
    app.MapQuoteEndpoints();
    app.MapPurchaseAgreementEndpoints();
    app.MapAircraftEndpoints();
    app.MapRegistrationEndpoints();
    app.MapWarrantyEndpoints();
    app.MapCabinLayoutEndpoints();
    app.MapMROFacilityEndpoints();
    app.MapMaintenanceAppointmentEndpoints();
    app.MapMaintenanceWorkOrderEndpoints();
    app.MapAirworthinessDirectiveEndpoints();
    app.MapServiceBulletinEndpoints();
    app.MapConnectedAircraftEndpoints();
    app.MapFlightHealthEventEndpoints();
    app.MapSoftwareLoadEndpoints();
    app.MapTypeCertificateEndpoints();
    app.MapProductionCertificateEndpoints();
    app.MapSalesRegionEndpoints();
    app.MapSalesCampaignEndpoints();

app.Run();

public partial class Program { }

