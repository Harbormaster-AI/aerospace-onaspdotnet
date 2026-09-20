using Microsoft.EntityFrameworkCore;

using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<AerospaceManufacturer> AerospaceManufacturers => Set<AerospaceManufacturer>();
public DbSet<AircraftProgram> AircraftPrograms => Set<AircraftProgram>();
public DbSet<AircraftFamily> AircraftFamilys => Set<AircraftFamily>();
public DbSet<AircraftModel> AircraftModels => Set<AircraftModel>();
public DbSet<EngineType> EngineTypes => Set<EngineType>();
public DbSet<AircraftVariant> AircraftVariants => Set<AircraftVariant>();
public DbSet<AvionicsSuite> AvionicsSuites => Set<AvionicsSuite>();
public DbSet<APU> APUs => Set<APU>();
public DbSet<LandingGear> LandingGears => Set<LandingGear>();
public DbSet<AircraftOption> AircraftOptions => Set<AircraftOption>();
public DbSet<AircraftPackage> AircraftPackages => Set<AircraftPackage>();
public DbSet<Supplier> Suppliers => Set<Supplier>();
public DbSet<Component_> Component_s => Set<Component_>();
public DbSet<Plant> Plants => Set<Plant>();
public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();
public DbSet<WorkCenter> WorkCenters => Set<WorkCenter>();
public DbSet<ProductionOrder> ProductionOrders => Set<ProductionOrder>();
public DbSet<BuildSchedule> BuildSchedules => Set<BuildSchedule>();
public DbSet<Warehouse> Warehouses => Set<Warehouse>();
public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
public DbSet<Operator> Operators => Set<Operator>();
public DbSet<AircraftOrder> AircraftOrders => Set<AircraftOrder>();
public DbSet<Quote> Quotes => Set<Quote>();
public DbSet<PurchaseAgreement> PurchaseAgreements => Set<PurchaseAgreement>();
public DbSet<Aircraft> Aircrafts => Set<Aircraft>();
public DbSet<Registration> Registrations => Set<Registration>();
public DbSet<Warranty> Warrantys => Set<Warranty>();
public DbSet<CabinLayout> CabinLayouts => Set<CabinLayout>();
public DbSet<MROFacility> MROFacilitys => Set<MROFacility>();
public DbSet<MaintenanceAppointment> MaintenanceAppointments => Set<MaintenanceAppointment>();
public DbSet<MaintenanceWorkOrder> MaintenanceWorkOrders => Set<MaintenanceWorkOrder>();
public DbSet<AirworthinessDirective> AirworthinessDirectives => Set<AirworthinessDirective>();
public DbSet<ServiceBulletin> ServiceBulletins => Set<ServiceBulletin>();
public DbSet<ConnectedAircraft> ConnectedAircrafts => Set<ConnectedAircraft>();
public DbSet<FlightHealthEvent> FlightHealthEvents => Set<FlightHealthEvent>();
public DbSet<SoftwareLoad> SoftwareLoads => Set<SoftwareLoad>();
public DbSet<TypeCertificate> TypeCertificates => Set<TypeCertificate>();
public DbSet<ProductionCertificate> ProductionCertificates => Set<ProductionCertificate>();
public DbSet<SalesRegion> SalesRegions => Set<SalesRegion>();
public DbSet<SalesCampaign> SalesCampaigns => Set<SalesCampaign>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // AerospaceManufacturer has one or more Programs of type AircraftProgram
        modelBuilder.Entity<AircraftProgram>()
            .HasOne<AerospaceManufacturer>()
            .WithMany(parent => parent.Programs)
            .HasForeignKey("ProgramsId");

        // AerospaceManufacturer has one or more Plants of type Plant
        modelBuilder.Entity<Plant>()
            .HasOne<AerospaceManufacturer>()
            .WithMany(parent => parent.Plants)
            .HasForeignKey("PlantsId");

        // AerospaceManufacturer has one or more Suppliers of type Supplier
        modelBuilder.Entity<Supplier>()
            .HasOne<AerospaceManufacturer>()
            .WithMany(parent => parent.Suppliers)
            .HasForeignKey("SuppliersId");

        // AerospaceManufacturer has one or more ProductionCertificates of type ProductionCertificate
        modelBuilder.Entity<ProductionCertificate>()
            .HasOne<AerospaceManufacturer>()
            .WithMany(parent => parent.ProductionCertificates)
            .HasForeignKey("ProductionCertificatesId");

        // AircraftProgram has one Manufacturer of type AerospaceManufacturer
        modelBuilder.Entity<AircraftProgram>()
            .HasOne(x => x.Manufacturer)
            .WithMany()
            .HasForeignKey("ManufacturerId");

        // AircraftProgram has one TypeCertificate of type TypeCertificate
        modelBuilder.Entity<AircraftProgram>()
            .HasOne(x => x.TypeCertificate)
            .WithMany()
            .HasForeignKey("TypeCertificateId");


        // AircraftProgram has one or more AircraftFamilies of type AircraftFamily
        modelBuilder.Entity<AircraftFamily>()
            .HasOne<AircraftProgram>()
            .WithMany(parent => parent.AircraftFamilies)
            .HasForeignKey("AircraftFamiliesId");

        // AircraftProgram has one or more KeySuppliers of type Supplier
        modelBuilder.Entity<Supplier>()
            .HasOne<AircraftProgram>()
            .WithMany(parent => parent.KeySuppliers)
            .HasForeignKey("KeySuppliersId");

        // AircraftFamily has one Program of type AircraftProgram
        modelBuilder.Entity<AircraftFamily>()
            .HasOne(x => x.Program)
            .WithMany()
            .HasForeignKey("ProgramId");


        // AircraftFamily has one or more AircraftModels of type AircraftModel
        modelBuilder.Entity<AircraftModel>()
            .HasOne<AircraftFamily>()
            .WithMany(parent => parent.AircraftModels)
            .HasForeignKey("AircraftModelsId");

        // AircraftModel has one Family of type AircraftFamily
        modelBuilder.Entity<AircraftModel>()
            .HasOne(x => x.Family)
            .WithMany()
            .HasForeignKey("FamilyId");


        // AircraftModel has one or more Variants of type AircraftVariant
        modelBuilder.Entity<AircraftVariant>()
            .HasOne<AircraftModel>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");

        // AircraftModel has one or more EngineTypes of type EngineType
        modelBuilder.Entity<EngineType>()
            .HasOne<AircraftModel>()
            .WithMany(parent => parent.EngineTypes)
            .HasForeignKey("EngineTypesId");

        // EngineType has one Supplier of type Supplier
        modelBuilder.Entity<EngineType>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("SupplierId");


        // EngineType has one or more CompatibleModels of type AircraftModel
        modelBuilder.Entity<AircraftModel>()
            .HasOne<EngineType>()
            .WithMany(parent => parent.CompatibleModels)
            .HasForeignKey("CompatibleModelsId");

        // AircraftVariant has one Model of type AircraftModel
        modelBuilder.Entity<AircraftVariant>()
            .HasOne(x => x.Model)
            .WithMany()
            .HasForeignKey("ModelId");

        // AircraftVariant has one EngineType of type EngineType
        modelBuilder.Entity<AircraftVariant>()
            .HasOne(x => x.EngineType)
            .WithMany()
            .HasForeignKey("EngineTypeId");

        // AircraftVariant has one AvionicsSuite of type AvionicsSuite
        modelBuilder.Entity<AircraftVariant>()
            .HasOne(x => x.AvionicsSuite)
            .WithMany()
            .HasForeignKey("AvionicsSuiteId");

        // AircraftVariant has one Apu of type APU
        modelBuilder.Entity<AircraftVariant>()
            .HasOne(x => x.Apu)
            .WithMany()
            .HasForeignKey("ApuId");

        // AircraftVariant has one LandingGear of type LandingGear
        modelBuilder.Entity<AircraftVariant>()
            .HasOne(x => x.LandingGear)
            .WithMany()
            .HasForeignKey("LandingGearId");


        // AircraftVariant has one or more CabinLayouts of type CabinLayout
        modelBuilder.Entity<CabinLayout>()
            .HasOne<AircraftVariant>()
            .WithMany(parent => parent.CabinLayouts)
            .HasForeignKey("CabinLayoutsId");

        // AircraftVariant has one or more Options of type AircraftOption
        modelBuilder.Entity<AircraftOption>()
            .HasOne<AircraftVariant>()
            .WithMany(parent => parent.Options)
            .HasForeignKey("OptionsId");

        // AircraftVariant has one or more Packages of type AircraftPackage
        modelBuilder.Entity<AircraftPackage>()
            .HasOne<AircraftVariant>()
            .WithMany(parent => parent.Packages)
            .HasForeignKey("PackagesId");

        // AvionicsSuite has one Supplier of type Supplier
        modelBuilder.Entity<AvionicsSuite>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("SupplierId");


        // AvionicsSuite has one or more Variants of type AircraftVariant
        modelBuilder.Entity<AircraftVariant>()
            .HasOne<AvionicsSuite>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");

        // AvionicsSuite has one or more SoftwareLoads of type SoftwareLoad
        modelBuilder.Entity<SoftwareLoad>()
            .HasOne<AvionicsSuite>()
            .WithMany(parent => parent.SoftwareLoads)
            .HasForeignKey("SoftwareLoadsId");

        // APU has one Supplier of type Supplier
        modelBuilder.Entity<APU>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("SupplierId");


        // APU has one or more Variants of type AircraftVariant
        modelBuilder.Entity<AircraftVariant>()
            .HasOne<APU>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");

        // LandingGear has one Supplier of type Supplier
        modelBuilder.Entity<LandingGear>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("SupplierId");


        // LandingGear has one or more Variants of type AircraftVariant
        modelBuilder.Entity<AircraftVariant>()
            .HasOne<LandingGear>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");


        // AircraftOption has one or more Variants of type AircraftVariant
        modelBuilder.Entity<AircraftVariant>()
            .HasOne<AircraftOption>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");

        // AircraftOption has one or more Packages of type AircraftPackage
        modelBuilder.Entity<AircraftPackage>()
            .HasOne<AircraftOption>()
            .WithMany(parent => parent.Packages)
            .HasForeignKey("PackagesId");


        // AircraftPackage has one or more Options of type AircraftOption
        modelBuilder.Entity<AircraftOption>()
            .HasOne<AircraftPackage>()
            .WithMany(parent => parent.Options)
            .HasForeignKey("OptionsId");

        // AircraftPackage has one or more Variants of type AircraftVariant
        modelBuilder.Entity<AircraftVariant>()
            .HasOne<AircraftPackage>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");


        // Supplier has one or more Manufacturers of type AerospaceManufacturer
        modelBuilder.Entity<AerospaceManufacturer>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.Manufacturers)
            .HasForeignKey("ManufacturersId");

        // Supplier has one or more Components of type Component_
        modelBuilder.Entity<Component_>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.Components)
            .HasForeignKey("ComponentsId");

        // Supplier has one or more EngineTypes of type EngineType
        modelBuilder.Entity<EngineType>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.EngineTypes)
            .HasForeignKey("EngineTypesId");

        // Supplier has one or more AvionicsSuites of type AvionicsSuite
        modelBuilder.Entity<AvionicsSuite>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.AvionicsSuites)
            .HasForeignKey("AvionicsSuitesId");

        // Supplier has one or more Apus of type APU
        modelBuilder.Entity<APU>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.Apus)
            .HasForeignKey("ApusId");

        // Supplier has one or more LandingGears of type LandingGear
        modelBuilder.Entity<LandingGear>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.LandingGears)
            .HasForeignKey("LandingGearsId");

        // Component_ has one Supplier of type Supplier
        modelBuilder.Entity<Component_>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("SupplierId");


        // Plant has one Manufacturer of type AerospaceManufacturer
        modelBuilder.Entity<Plant>()
            .HasOne(x => x.Manufacturer)
            .WithMany()
            .HasForeignKey("ManufacturerId");


        // Plant has one or more ProductionLines of type ProductionLine
        modelBuilder.Entity<ProductionLine>()
            .HasOne<Plant>()
            .WithMany(parent => parent.ProductionLines)
            .HasForeignKey("ProductionLinesId");

        // Plant has one or more Warehouses of type Warehouse
        modelBuilder.Entity<Warehouse>()
            .HasOne<Plant>()
            .WithMany(parent => parent.Warehouses)
            .HasForeignKey("WarehousesId");

        // ProductionLine has one Plant of type Plant
        modelBuilder.Entity<ProductionLine>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");


        // ProductionLine has one or more WorkCenters of type WorkCenter
        modelBuilder.Entity<WorkCenter>()
            .HasOne<ProductionLine>()
            .WithMany(parent => parent.WorkCenters)
            .HasForeignKey("WorkCentersId");

        // WorkCenter has one ProductionLine of type ProductionLine
        modelBuilder.Entity<WorkCenter>()
            .HasOne(x => x.ProductionLine)
            .WithMany()
            .HasForeignKey("ProductionLineId");


        // ProductionOrder has one Variant of type AircraftVariant
        modelBuilder.Entity<ProductionOrder>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");

        // ProductionOrder has one Plant of type Plant
        modelBuilder.Entity<ProductionOrder>()
            .HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey("PlantId");

        // ProductionOrder has one AircraftOrder of type AircraftOrder
        modelBuilder.Entity<ProductionOrder>()
            .HasOne(x => x.AircraftOrder)
            .WithMany()
            .HasForeignKey("AircraftOrderId");



        // BuildSchedule has one or more ProductionOrders of type ProductionOrder
        modelBuilder.Entity<ProductionOrder>()
            .HasOne<BuildSchedule>()
            .WithMany(parent => parent.ProductionOrders)
            .HasForeignKey("ProductionOrdersId");


        // Warehouse has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<Warehouse>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // InventoryItem has one Component of type Component_
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Component)
            .WithMany()
            .HasForeignKey("ComponentId");

        // InventoryItem has one Warehouse of type Warehouse
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey("WarehouseId");


        // Operator has one SalesRegion of type SalesRegion
        modelBuilder.Entity<Operator>()
            .HasOne(x => x.SalesRegion)
            .WithMany()
            .HasForeignKey("SalesRegionId");


        // Operator has one or more AircraftOrders of type AircraftOrder
        modelBuilder.Entity<AircraftOrder>()
            .HasOne<Operator>()
            .WithMany(parent => parent.AircraftOrders)
            .HasForeignKey("AircraftOrdersId");

        // Operator has one or more OperatedAircraft of type Aircraft
        modelBuilder.Entity<Aircraft>()
            .HasOne<Operator>()
            .WithMany(parent => parent.OperatedAircraft)
            .HasForeignKey("OperatedAircraftId");

        // AircraftOrder has one Operator of type Operator
        modelBuilder.Entity<AircraftOrder>()
            .HasOne(x => x.Operator)
            .WithMany()
            .HasForeignKey("OperatorId");

        // AircraftOrder has one Variant of type AircraftVariant
        modelBuilder.Entity<AircraftOrder>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");

        // AircraftOrder has one Quote of type Quote
        modelBuilder.Entity<AircraftOrder>()
            .HasOne(x => x.Quote)
            .WithMany()
            .HasForeignKey("QuoteId");

        // AircraftOrder has one PurchaseAgreement of type PurchaseAgreement
        modelBuilder.Entity<AircraftOrder>()
            .HasOne(x => x.PurchaseAgreement)
            .WithMany()
            .HasForeignKey("PurchaseAgreementId");


        // Quote has one AircraftOrder of type AircraftOrder
        modelBuilder.Entity<Quote>()
            .HasOne(x => x.AircraftOrder)
            .WithMany()
            .HasForeignKey("AircraftOrderId");


        // PurchaseAgreement has one AircraftOrder of type AircraftOrder
        modelBuilder.Entity<PurchaseAgreement>()
            .HasOne(x => x.AircraftOrder)
            .WithMany()
            .HasForeignKey("AircraftOrderId");


        // Aircraft has one Variant of type AircraftVariant
        modelBuilder.Entity<Aircraft>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");

        // Aircraft has one Operator of type Operator
        modelBuilder.Entity<Aircraft>()
            .HasOne(x => x.Operator)
            .WithMany()
            .HasForeignKey("OperatorId");

        // Aircraft has one Registration of type Registration
        modelBuilder.Entity<Aircraft>()
            .HasOne(x => x.Registration)
            .WithMany()
            .HasForeignKey("RegistrationId");

        // Aircraft has one Warranty of type Warranty
        modelBuilder.Entity<Aircraft>()
            .HasOne(x => x.Warranty)
            .WithMany()
            .HasForeignKey("WarrantyId");

        // Aircraft has one ConnectedAircraft of type ConnectedAircraft
        modelBuilder.Entity<Aircraft>()
            .HasOne(x => x.ConnectedAircraft)
            .WithMany()
            .HasForeignKey("ConnectedAircraftId");

        // Aircraft has one CabinLayout of type CabinLayout
        modelBuilder.Entity<Aircraft>()
            .HasOne(x => x.CabinLayout)
            .WithMany()
            .HasForeignKey("CabinLayoutId");


        // Aircraft has one or more MaintenanceRecords of type MaintenanceWorkOrder
        modelBuilder.Entity<MaintenanceWorkOrder>()
            .HasOne<Aircraft>()
            .WithMany(parent => parent.MaintenanceRecords)
            .HasForeignKey("MaintenanceRecordsId");

        // Registration has one Aircraft of type Aircraft
        modelBuilder.Entity<Registration>()
            .HasOne(x => x.Aircraft)
            .WithMany()
            .HasForeignKey("AircraftId");


        // Warranty has one Aircraft of type Aircraft
        modelBuilder.Entity<Warranty>()
            .HasOne(x => x.Aircraft)
            .WithMany()
            .HasForeignKey("AircraftId");


        // CabinLayout has one Variant of type AircraftVariant
        modelBuilder.Entity<CabinLayout>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");


        // CabinLayout has one or more Aircraft of type Aircraft
        modelBuilder.Entity<Aircraft>()
            .HasOne<CabinLayout>()
            .WithMany(parent => parent.Aircraft)
            .HasForeignKey("AircraftId");

        // CabinLayout has one or more Options of type AircraftOption
        modelBuilder.Entity<AircraftOption>()
            .HasOne<CabinLayout>()
            .WithMany(parent => parent.Options)
            .HasForeignKey("OptionsId");


        // MROFacility has one or more Appointments of type MaintenanceAppointment
        modelBuilder.Entity<MaintenanceAppointment>()
            .HasOne<MROFacility>()
            .WithMany(parent => parent.Appointments)
            .HasForeignKey("AppointmentsId");

        // MROFacility has one or more WorkOrders of type MaintenanceWorkOrder
        modelBuilder.Entity<MaintenanceWorkOrder>()
            .HasOne<MROFacility>()
            .WithMany(parent => parent.WorkOrders)
            .HasForeignKey("WorkOrdersId");

        // MaintenanceAppointment has one Aircraft of type Aircraft
        modelBuilder.Entity<MaintenanceAppointment>()
            .HasOne(x => x.Aircraft)
            .WithMany()
            .HasForeignKey("AircraftId");

        // MaintenanceAppointment has one MroFacility of type MROFacility
        modelBuilder.Entity<MaintenanceAppointment>()
            .HasOne(x => x.MroFacility)
            .WithMany()
            .HasForeignKey("MroFacilityId");

        // MaintenanceAppointment has one WorkOrder of type MaintenanceWorkOrder
        modelBuilder.Entity<MaintenanceAppointment>()
            .HasOne(x => x.WorkOrder)
            .WithMany()
            .HasForeignKey("WorkOrderId");


        // MaintenanceWorkOrder has one Aircraft of type Aircraft
        modelBuilder.Entity<MaintenanceWorkOrder>()
            .HasOne(x => x.Aircraft)
            .WithMany()
            .HasForeignKey("AircraftId");

        // MaintenanceWorkOrder has one AirworthinessDirective of type AirworthinessDirective
        modelBuilder.Entity<MaintenanceWorkOrder>()
            .HasOne(x => x.AirworthinessDirective)
            .WithMany()
            .HasForeignKey("AirworthinessDirectiveId");

        // MaintenanceWorkOrder has one ServiceBulletin of type ServiceBulletin
        modelBuilder.Entity<MaintenanceWorkOrder>()
            .HasOne(x => x.ServiceBulletin)
            .WithMany()
            .HasForeignKey("ServiceBulletinId");



        // AirworthinessDirective has one or more WorkOrders of type MaintenanceWorkOrder
        modelBuilder.Entity<MaintenanceWorkOrder>()
            .HasOne<AirworthinessDirective>()
            .WithMany(parent => parent.WorkOrders)
            .HasForeignKey("WorkOrdersId");


        // ServiceBulletin has one or more WorkOrders of type MaintenanceWorkOrder
        modelBuilder.Entity<MaintenanceWorkOrder>()
            .HasOne<ServiceBulletin>()
            .WithMany(parent => parent.WorkOrders)
            .HasForeignKey("WorkOrdersId");

        // ServiceBulletin has one or more Variants of type AircraftVariant
        modelBuilder.Entity<AircraftVariant>()
            .HasOne<ServiceBulletin>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");

        // ConnectedAircraft has one Aircraft of type Aircraft
        modelBuilder.Entity<ConnectedAircraft>()
            .HasOne(x => x.Aircraft)
            .WithMany()
            .HasForeignKey("AircraftId");


        // ConnectedAircraft has one or more FlightHealthEvents of type FlightHealthEvent
        modelBuilder.Entity<FlightHealthEvent>()
            .HasOne<ConnectedAircraft>()
            .WithMany(parent => parent.FlightHealthEvents)
            .HasForeignKey("FlightHealthEventsId");

        // ConnectedAircraft has one or more SoftwareLoads of type SoftwareLoad
        modelBuilder.Entity<SoftwareLoad>()
            .HasOne<ConnectedAircraft>()
            .WithMany(parent => parent.SoftwareLoads)
            .HasForeignKey("SoftwareLoadsId");

        // FlightHealthEvent has one ConnectedAircraft of type ConnectedAircraft
        modelBuilder.Entity<FlightHealthEvent>()
            .HasOne(x => x.ConnectedAircraft)
            .WithMany()
            .HasForeignKey("ConnectedAircraftId");


        // SoftwareLoad has one ConnectedAircraft of type ConnectedAircraft
        modelBuilder.Entity<SoftwareLoad>()
            .HasOne(x => x.ConnectedAircraft)
            .WithMany()
            .HasForeignKey("ConnectedAircraftId");

        // SoftwareLoad has one AvionicsSuite of type AvionicsSuite
        modelBuilder.Entity<SoftwareLoad>()
            .HasOne(x => x.AvionicsSuite)
            .WithMany()
            .HasForeignKey("AvionicsSuiteId");


        // TypeCertificate has one Program of type AircraftProgram
        modelBuilder.Entity<TypeCertificate>()
            .HasOne(x => x.Program)
            .WithMany()
            .HasForeignKey("ProgramId");


        // ProductionCertificate has one Manufacturer of type AerospaceManufacturer
        modelBuilder.Entity<ProductionCertificate>()
            .HasOne(x => x.Manufacturer)
            .WithMany()
            .HasForeignKey("ManufacturerId");



        // SalesRegion has one or more Operators of type Operator
        modelBuilder.Entity<Operator>()
            .HasOne<SalesRegion>()
            .WithMany(parent => parent.Operators)
            .HasForeignKey("OperatorsId");

        // SalesRegion has one or more SalesCampaigns of type SalesCampaign
        modelBuilder.Entity<SalesCampaign>()
            .HasOne<SalesRegion>()
            .WithMany(parent => parent.SalesCampaigns)
            .HasForeignKey("SalesCampaignsId");

        // SalesCampaign has one Region of type SalesRegion
        modelBuilder.Entity<SalesCampaign>()
            .HasOne(x => x.Region)
            .WithMany()
            .HasForeignKey("RegionId");

        // SalesCampaign has one Operator of type Operator
        modelBuilder.Entity<SalesCampaign>()
            .HasOne(x => x.Operator)
            .WithMany()
            .HasForeignKey("OperatorId");


        // SalesCampaign has one or more Quotes of type Quote
        modelBuilder.Entity<Quote>()
            .HasOne<SalesCampaign>()
            .WithMany(parent => parent.Quotes)
            .HasForeignKey("QuotesId");

    }
}
