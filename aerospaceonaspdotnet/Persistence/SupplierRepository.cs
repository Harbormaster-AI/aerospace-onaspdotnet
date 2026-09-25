
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class SupplierRepository : ISupplierRepository
{
    private readonly ApplicationDbContext _db;

    public SupplierRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Add(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Update(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Remove(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToManufacturersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AerospaceManufacturers
            .Where(aerospaceManufacturer =>
                request.ChildIds.Contains(aerospaceManufacturer.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aerospaceManufacturer =>
                        EF.Property<Guid?>(
                            aerospaceManufacturer,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromManufacturersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AerospaceManufacturers
            .Where(aerospaceManufacturer =>
                request.ChildIds.Contains(aerospaceManufacturer.Id) &&
                EF.Property<Guid?>(
                    aerospaceManufacturer,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aerospaceManufacturer =>
                        EF.Property<Guid?>(
                            aerospaceManufacturer,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToComponentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Component_s
            .Where(component_ =>
                request.ChildIds.Contains(component_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    component_ =>
                        EF.Property<Guid?>(
                            component_,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromComponentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Component_s
            .Where(component_ =>
                request.ChildIds.Contains(component_.Id) &&
                EF.Property<Guid?>(
                    component_,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    component_ =>
                        EF.Property<Guid?>(
                            component_,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToEngineTypesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EngineTypes
            .Where(engineType =>
                request.ChildIds.Contains(engineType.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    engineType =>
                        EF.Property<Guid?>(
                            engineType,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEngineTypesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EngineTypes
            .Where(engineType =>
                request.ChildIds.Contains(engineType.Id) &&
                EF.Property<Guid?>(
                    engineType,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    engineType =>
                        EF.Property<Guid?>(
                            engineType,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToAvionicsSuitesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AvionicsSuites
            .Where(avionicsSuite =>
                request.ChildIds.Contains(avionicsSuite.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    avionicsSuite =>
                        EF.Property<Guid?>(
                            avionicsSuite,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAvionicsSuitesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AvionicsSuites
            .Where(avionicsSuite =>
                request.ChildIds.Contains(avionicsSuite.Id) &&
                EF.Property<Guid?>(
                    avionicsSuite,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    avionicsSuite =>
                        EF.Property<Guid?>(
                            avionicsSuite,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToApusAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.APUs
            .Where(aPU =>
                request.ChildIds.Contains(aPU.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aPU =>
                        EF.Property<Guid?>(
                            aPU,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromApusAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.APUs
            .Where(aPU =>
                request.ChildIds.Contains(aPU.Id) &&
                EF.Property<Guid?>(
                    aPU,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aPU =>
                        EF.Property<Guid?>(
                            aPU,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToLandingGearsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LandingGears
            .Where(landingGear =>
                request.ChildIds.Contains(landingGear.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    landingGear =>
                        EF.Property<Guid?>(
                            landingGear,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLandingGearsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LandingGears
            .Where(landingGear =>
                request.ChildIds.Contains(landingGear.Id) &&
                EF.Property<Guid?>(
                    landingGear,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    landingGear =>
                        EF.Property<Guid?>(
                            landingGear,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
