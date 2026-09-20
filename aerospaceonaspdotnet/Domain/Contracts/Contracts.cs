using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class AerospaceManufacturerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
}

public class AerospaceManufacturerResponse : AerospaceManufacturerRequest {
    public static AerospaceManufacturerResponse FromModel(AerospaceManufacturer model) {
        return new AerospaceManufacturerResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            HeadquartersCountry = model.HeadquartersCountry,
            Website = model.Website,
        };
    }
}

public class AircraftProgramRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ProgramCode { get; set; } 
 public virtual int? EntryIntoServiceYear { get; set; } 
 public virtual ProgramStatus? Status { get; set; } 
}

public class AircraftProgramResponse : AircraftProgramRequest {
    public static AircraftProgramResponse FromModel(AircraftProgram model) {
        return new AircraftProgramResponse {
            Id = model.Id,
            Name = model.Name,
            ProgramCode = model.ProgramCode,
            EntryIntoServiceYear = model.EntryIntoServiceYear,
            Status = model.Status,
        };
    }
}

public class AircraftFamilyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? FamilyCode { get; set; } 
}

public class AircraftFamilyResponse : AircraftFamilyRequest {
    public static AircraftFamilyResponse FromModel(AircraftFamily model) {
        return new AircraftFamilyResponse {
            Id = model.Id,
            Name = model.Name,
            FamilyCode = model.FamilyCode,
        };
    }
}

public class AircraftModelRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ModelDesignation { get; set; } 
 public virtual AircraftType? AircraftType { get; set; } 
}

public class AircraftModelResponse : AircraftModelRequest {
    public static AircraftModelResponse FromModel(AircraftModel model) {
        return new AircraftModelResponse {
            Id = model.Id,
            Name = model.Name,
            ModelDesignation = model.ModelDesignation,
            AircraftType = model.AircraftType,
        };
    }
}

public class EngineTypeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EngineModelCode { get; set; } 
 public virtual decimal? MaxThrustKn { get; set; } 
 public virtual EngineCategory? Category { get; set; } 
}

public class EngineTypeResponse : EngineTypeRequest {
    public static EngineTypeResponse FromModel(EngineType model) {
        return new EngineTypeResponse {
            Id = model.Id,
            EngineModelCode = model.EngineModelCode,
            MaxThrustKn = model.MaxThrustKn,
            Category = model.Category,
        };
    }
}

public class AircraftVariantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? VariantCode { get; set; } 
 public virtual int? RangeNm { get; set; } 
 public virtual decimal? MaxTakeoffWeightKg { get; set; } 
}

public class AircraftVariantResponse : AircraftVariantRequest {
    public static AircraftVariantResponse FromModel(AircraftVariant model) {
        return new AircraftVariantResponse {
            Id = model.Id,
            VariantCode = model.VariantCode,
            RangeNm = model.RangeNm,
            MaxTakeoffWeightKg = model.MaxTakeoffWeightKg,
        };
    }
}

public class AvionicsSuiteRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SuiteName { get; set; } 
 public virtual string? SoftwareBaseline { get; set; } 
}

public class AvionicsSuiteResponse : AvionicsSuiteRequest {
    public static AvionicsSuiteResponse FromModel(AvionicsSuite model) {
        return new AvionicsSuiteResponse {
            Id = model.Id,
            SuiteName = model.SuiteName,
            SoftwareBaseline = model.SoftwareBaseline,
        };
    }
}

public class APURequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Model_ { get; set; } 
}

public class APUResponse : APURequest {
    public static APUResponse FromModel(APU model) {
        return new APUResponse {
            Id = model.Id,
            Model_ = model.Model_,
        };
    }
}

public class LandingGearRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SupplierPartNumber { get; set; } 
 public virtual LandingGearType? GearType { get; set; } 
}

public class LandingGearResponse : LandingGearRequest {
    public static LandingGearResponse FromModel(LandingGear model) {
        return new LandingGearResponse {
            Id = model.Id,
            SupplierPartNumber = model.SupplierPartNumber,
            GearType = model.GearType,
        };
    }
}

public class AircraftOptionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual OptionCategory? OptionCategory { get; set; } 
}

public class AircraftOptionResponse : AircraftOptionRequest {
    public static AircraftOptionResponse FromModel(AircraftOption model) {
        return new AircraftOptionResponse {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
            OptionCategory = model.OptionCategory,
        };
    }
}

public class AircraftPackageRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual PackageType? PackageType { get; set; } 
}

public class AircraftPackageResponse : AircraftPackageRequest {
    public static AircraftPackageResponse FromModel(AircraftPackage model) {
        return new AircraftPackageResponse {
            Id = model.Id,
            Name = model.Name,
            PackageType = model.PackageType,
        };
    }
}

public class SupplierRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual SupplierType? SupplierType { get; set; } 
 public virtual SupplierApprovalStatus? ApprovalStatus { get; set; } 
}

public class SupplierResponse : SupplierRequest {
    public static SupplierResponse FromModel(Supplier model) {
        return new SupplierResponse {
            Id = model.Id,
            Name = model.Name,
            SupplierType = model.SupplierType,
            ApprovalStatus = model.ApprovalStatus,
        };
    }
}

public class Component_Request {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PartNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual ComponentCategory? ComponentCategory { get; set; } 
 public virtual SerializationMethod? SerializationMethod { get; set; } 
}

public class Component_Response : Component_Request {
    public static Component_Response FromModel(Component_ model) {
        return new Component_Response {
            Id = model.Id,
            PartNumber = model.PartNumber,
            Name = model.Name,
            ComponentCategory = model.ComponentCategory,
            SerializationMethod = model.SerializationMethod,
        };
    }
}

public class PlantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? PlantCode { get; set; } 
 public virtual Address? Address { get; set; } 
}

public class PlantResponse : PlantRequest {
    public static PlantResponse FromModel(Plant model) {
        return new PlantResponse {
            Id = model.Id,
            Name = model.Name,
            PlantCode = model.PlantCode,
            Address = model.Address,
        };
    }
}

public class ProductionLineRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual ProductionLineType? LineType { get; set; } 
}

public class ProductionLineResponse : ProductionLineRequest {
    public static ProductionLineResponse FromModel(ProductionLine model) {
        return new ProductionLineResponse {
            Id = model.Id,
            Name = model.Name,
            LineType = model.LineType,
        };
    }
}

public class WorkCenterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Capability { get; set; } 
}

public class WorkCenterResponse : WorkCenterRequest {
    public static WorkCenterResponse FromModel(WorkCenter model) {
        return new WorkCenterResponse {
            Id = model.Id,
            Name = model.Name,
            Capability = model.Capability,
        };
    }
}

public class ProductionOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderNumber { get; set; } 
 public virtual ProductionOrderStatus? Status { get; set; } 
}

public class ProductionOrderResponse : ProductionOrderRequest {
    public static ProductionOrderResponse FromModel(ProductionOrder model) {
        return new ProductionOrderResponse {
            Id = model.Id,
            OrderNumber = model.OrderNumber,
            Status = model.Status,
        };
    }
}

public class BuildScheduleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ScheduleNumber { get; set; } 
 public virtual ScheduleStatus? Status { get; set; } 
}

public class BuildScheduleResponse : BuildScheduleRequest {
    public static BuildScheduleResponse FromModel(BuildSchedule model) {
        return new BuildScheduleResponse {
            Id = model.Id,
            ScheduleNumber = model.ScheduleNumber,
            Status = model.Status,
        };
    }
}

public class WarehouseRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
}

public class WarehouseResponse : WarehouseRequest {
    public static WarehouseResponse FromModel(Warehouse model) {
        return new WarehouseResponse {
            Id = model.Id,
            Name = model.Name,
        };
    }
}

public class InventoryItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? QuantityOnHand { get; set; } 
 public virtual int? QuantityReserved { get; set; } 
 public virtual string? LotNumber { get; set; } 
}

public class InventoryItemResponse : InventoryItemRequest {
    public static InventoryItemResponse FromModel(InventoryItem model) {
        return new InventoryItemResponse {
            Id = model.Id,
            QuantityOnHand = model.QuantityOnHand,
            QuantityReserved = model.QuantityReserved,
            LotNumber = model.LotNumber,
        };
    }
}

public class Operator_Request {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? IcaoDesignator { get; set; } 
 public virtual OperatorType? OperatorType { get; set; } 
}

public class Operator_Response : Operator_Request {
    public static Operator_Response FromModel(Operator_ model) {
        return new Operator_Response {
            Id = model.Id,
            Name = model.Name,
            IcaoDesignator = model.IcaoDesignator,
            OperatorType = model.OperatorType,
        };
    }
}

public class AircraftOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
 public virtual AircraftOrderStatus? Status { get; set; } 
}

public class AircraftOrderResponse : AircraftOrderRequest {
    public static AircraftOrderResponse FromModel(AircraftOrder model) {
        return new AircraftOrderResponse {
            Id = model.Id,
            OrderNumber = model.OrderNumber,
            TotalAmount = model.TotalAmount,
            Status = model.Status,
        };
    }
}

public class QuoteRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? QuoteNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
}

public class QuoteResponse : QuoteRequest {
    public static QuoteResponse FromModel(Quote model) {
        return new QuoteResponse {
            Id = model.Id,
            QuoteNumber = model.QuoteNumber,
            TotalAmount = model.TotalAmount,
        };
    }
}

public class PurchaseAgreementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AgreementNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
}

public class PurchaseAgreementResponse : PurchaseAgreementRequest {
    public static PurchaseAgreementResponse FromModel(PurchaseAgreement model) {
        return new PurchaseAgreementResponse {
            Id = model.Id,
            AgreementNumber = model.AgreementNumber,
            EffectiveDate = model.EffectiveDate,
        };
    }
}

public class AircraftRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual MSN? Msn { get; set; } 
 public virtual DateOnly? DeliveryDate { get; set; } 
}

public class AircraftResponse : AircraftRequest {
    public static AircraftResponse FromModel(Aircraft model) {
        return new AircraftResponse {
            Id = model.Id,
            Msn = model.Msn,
            DeliveryDate = model.DeliveryDate,
        };
    }
}

public class RegistrationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual TailNumber? TailNumber { get; set; } 
 public virtual string? RegistryCountry { get; set; } 
}

public class RegistrationResponse : RegistrationRequest {
    public static RegistrationResponse FromModel(Registration model) {
        return new RegistrationResponse {
            Id = model.Id,
            TailNumber = model.TailNumber,
            RegistryCountry = model.RegistryCountry,
        };
    }
}

public class WarrantyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? CoverageMonths { get; set; } 
 public virtual WarrantyType? WarrantyType { get; set; } 
}

public class WarrantyResponse : WarrantyRequest {
    public static WarrantyResponse FromModel(Warranty model) {
        return new WarrantyResponse {
            Id = model.Id,
            CoverageMonths = model.CoverageMonths,
            WarrantyType = model.WarrantyType,
        };
    }
}

public class CabinLayoutRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? LayoutCode { get; set; } 
 public virtual int? TotalSeats { get; set; } 
 public virtual string? ClassLayout { get; set; } 
}

public class CabinLayoutResponse : CabinLayoutRequest {
    public static CabinLayoutResponse FromModel(CabinLayout model) {
        return new CabinLayoutResponse {
            Id = model.Id,
            LayoutCode = model.LayoutCode,
            TotalSeats = model.TotalSeats,
            ClassLayout = model.ClassLayout,
        };
    }
}

public class MROFacilityRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ApprovalScope { get; set; } 
 public virtual Address? Address { get; set; } 
}

public class MROFacilityResponse : MROFacilityRequest {
    public static MROFacilityResponse FromModel(MROFacility model) {
        return new MROFacilityResponse {
            Id = model.Id,
            Name = model.Name,
            ApprovalScope = model.ApprovalScope,
            Address = model.Address,
        };
    }
}

public class MaintenanceAppointmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? AppointmentDate { get; set; } 
 public virtual AppointmentStatus? Status { get; set; } 
}

public class MaintenanceAppointmentResponse : MaintenanceAppointmentRequest {
    public static MaintenanceAppointmentResponse FromModel(MaintenanceAppointment model) {
        return new MaintenanceAppointmentResponse {
            Id = model.Id,
            AppointmentDate = model.AppointmentDate,
            Status = model.Status,
        };
    }
}

public class MaintenanceWorkOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? WorkOrderNumber { get; set; } 
 public virtual WorkOrderStatus? Status { get; set; } 
}

public class MaintenanceWorkOrderResponse : MaintenanceWorkOrderRequest {
    public static MaintenanceWorkOrderResponse FromModel(MaintenanceWorkOrder model) {
        return new MaintenanceWorkOrderResponse {
            Id = model.Id,
            WorkOrderNumber = model.WorkOrderNumber,
            Status = model.Status,
        };
    }
}

public class AirworthinessDirectiveRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? DirectiveNumber { get; set; } 
 public virtual string? Title { get; set; } 
}

public class AirworthinessDirectiveResponse : AirworthinessDirectiveRequest {
    public static AirworthinessDirectiveResponse FromModel(AirworthinessDirective model) {
        return new AirworthinessDirectiveResponse {
            Id = model.Id,
            DirectiveNumber = model.DirectiveNumber,
            Title = model.Title,
        };
    }
}

public class ServiceBulletinRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? BulletinNumber { get; set; } 
 public virtual ServiceBulletinCategory? Category { get; set; } 
}

public class ServiceBulletinResponse : ServiceBulletinRequest {
    public static ServiceBulletinResponse FromModel(ServiceBulletin model) {
        return new ServiceBulletinResponse {
            Id = model.Id,
            BulletinNumber = model.BulletinNumber,
            Category = model.Category,
        };
    }
}

public class ConnectedAircraftRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CommunicationsProvider { get; set; } 
 public virtual ConnectivityStatus? ConnectivityStatus { get; set; } 
}

public class ConnectedAircraftResponse : ConnectedAircraftRequest {
    public static ConnectedAircraftResponse FromModel(ConnectedAircraft model) {
        return new ConnectedAircraftResponse {
            Id = model.Id,
            CommunicationsProvider = model.CommunicationsProvider,
            ConnectivityStatus = model.ConnectivityStatus,
        };
    }
}

public class FlightHealthEventRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EventCode { get; set; } 
 public virtual EventSeverity? Severity { get; set; } 
}

public class FlightHealthEventResponse : FlightHealthEventRequest {
    public static FlightHealthEventResponse FromModel(FlightHealthEvent model) {
        return new FlightHealthEventResponse {
            Id = model.Id,
            EventCode = model.EventCode,
            Severity = model.Severity,
        };
    }
}

public class SoftwareLoadRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Version { get; set; } 
 public virtual SoftwareLoadType? LoadType { get; set; } 
}

public class SoftwareLoadResponse : SoftwareLoadRequest {
    public static SoftwareLoadResponse FromModel(SoftwareLoad model) {
        return new SoftwareLoadResponse {
            Id = model.Id,
            Version = model.Version,
            LoadType = model.LoadType,
        };
    }
}

public class TypeCertificateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CertificateNumber { get; set; } 
 public virtual string? Authority { get; set; } 
}

public class TypeCertificateResponse : TypeCertificateRequest {
    public static TypeCertificateResponse FromModel(TypeCertificate model) {
        return new TypeCertificateResponse {
            Id = model.Id,
            CertificateNumber = model.CertificateNumber,
            Authority = model.Authority,
        };
    }
}

public class ProductionCertificateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CertificateNumber { get; set; } 
 public virtual string? Authority { get; set; } 
}

public class ProductionCertificateResponse : ProductionCertificateRequest {
    public static ProductionCertificateResponse FromModel(ProductionCertificate model) {
        return new ProductionCertificateResponse {
            Id = model.Id,
            CertificateNumber = model.CertificateNumber,
            Authority = model.Authority,
        };
    }
}

public class SalesRegionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? RegionCode { get; set; } 
}

public class SalesRegionResponse : SalesRegionRequest {
    public static SalesRegionResponse FromModel(SalesRegion model) {
        return new SalesRegionResponse {
            Id = model.Id,
            Name = model.Name,
            RegionCode = model.RegionCode,
        };
    }
}

public class SalesCampaignRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CampaignCode { get; set; } 
 public virtual SalesCampaignStatus? Status { get; set; } 
}

public class SalesCampaignResponse : SalesCampaignRequest {
    public static SalesCampaignResponse FromModel(SalesCampaign model) {
        return new SalesCampaignResponse {
            Id = model.Id,
            CampaignCode = model.CampaignCode,
            Status = model.Status,
        };
    }
}

