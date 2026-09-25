
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AerospaceManufacturerRepository : IAerospaceManufacturerRepository
{
    private readonly ApplicationDbContext _db;

    public AerospaceManufacturerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AerospaceManufacturer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AerospaceManufacturers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AerospaceManufacturer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AerospaceManufacturers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken)
    {
        _db.AerospaceManufacturers.Add(aerospaceManufacturer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken)
    {
        _db.AerospaceManufacturers.Update(aerospaceManufacturer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken)
    {
        _db.AerospaceManufacturers.Remove(aerospaceManufacturer);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToProgramsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftPrograms
            .Where(aircraftProgram =>
                request.ChildIds.Contains(aircraftProgram.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftProgram =>
                        EF.Property<Guid?>(
                            aircraftProgram,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProgramsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftPrograms
            .Where(aircraftProgram =>
                request.ChildIds.Contains(aircraftProgram.Id) &&
                EF.Property<Guid?>(
                    aircraftProgram,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftProgram =>
                        EF.Property<Guid?>(
                            aircraftProgram,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToPlantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Plants
            .Where(plant =>
                request.ChildIds.Contains(plant.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    plant =>
                        EF.Property<Guid?>(
                            plant,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPlantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Plants
            .Where(plant =>
                request.ChildIds.Contains(plant.Id) &&
                EF.Property<Guid?>(
                    plant,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    plant =>
                        EF.Property<Guid?>(
                            plant,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToSuppliersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Suppliers
            .Where(supplier =>
                request.ChildIds.Contains(supplier.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    supplier =>
                        EF.Property<Guid?>(
                            supplier,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSuppliersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Suppliers
            .Where(supplier =>
                request.ChildIds.Contains(supplier.Id) &&
                EF.Property<Guid?>(
                    supplier,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    supplier =>
                        EF.Property<Guid?>(
                            supplier,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToProductionCertificatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionCertificates
            .Where(productionCertificate =>
                request.ChildIds.Contains(productionCertificate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionCertificate =>
                        EF.Property<Guid?>(
                            productionCertificate,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductionCertificatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionCertificates
            .Where(productionCertificate =>
                request.ChildIds.Contains(productionCertificate.Id) &&
                EF.Property<Guid?>(
                    productionCertificate,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionCertificate =>
                        EF.Property<Guid?>(
                            productionCertificate,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
