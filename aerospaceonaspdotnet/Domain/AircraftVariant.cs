
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AircraftVariant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftvariantId { get; set; } 
 public virtual string? VariantCode { get; set; } 
 public virtual int? RangeNm { get; set; } 
 public virtual decimal? MaxTakeoffWeightKg { get; set; } 
public virtual AircraftModel? Model_ { get; set; } 
public virtual EngineType? EngineType { get; set; } 
public virtual AvionicsSuite? AvionicsSuite { get; set; } 
public virtual APU? Apu { get; set; } 
public virtual LandingGear? LandingGear { get; set; } 
public virtual ICollection<CabinLayout> CabinLayouts { get; set; } = new List<CabinLayout>();
public virtual ICollection<AircraftOption> Options { get; set; } = new List<AircraftOption>();
public virtual ICollection<AircraftPackage> Packages { get; set; } = new List<AircraftPackage>();

    public static AircraftVariant FromRequest(AircraftVariantRequest request) {
        return new AircraftVariant {
            Id = request.Id,
            VariantCode = request.VariantCode,
            RangeNm = request.RangeNm,
            MaxTakeoffWeightKg = request.MaxTakeoffWeightKg,
        };
    }
}
