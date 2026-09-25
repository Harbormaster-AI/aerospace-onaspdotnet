
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AircraftProgram
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftprogramId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ProgramCode { get; set; } 
 public virtual int? EntryIntoServiceYear { get; set; } 
public virtual AerospaceManufacturer? Manufacturer { get; set; } 
public virtual ICollection<AircraftFamily> AircraftFamilies { get; set; } = new List<AircraftFamily>();
public virtual TypeCertificate? TypeCertificate { get; set; } 
public virtual ICollection<Supplier> KeySuppliers { get; set; } = new List<Supplier>();
 public virtual ProgramStatus? Status { get; set; } 

    public static AircraftProgram FromRequest(AircraftProgramRequest request) {
        return new AircraftProgram {
            Id = request.Id,
            Name = request.Name,
            ProgramCode = request.ProgramCode,
            EntryIntoServiceYear = request.EntryIntoServiceYear,
            Status = request.Status,
        };
    }
}
