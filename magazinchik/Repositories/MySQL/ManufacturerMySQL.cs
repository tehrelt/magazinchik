using magazinchik.DAL;
using magazinchik.DAL.domain;
using magazinchik.DTOs;
using magazinchik.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Repositories.MySQL;

public class ManufacturerMySql : IManufacturerRepository
{
    private readonly SneakersShopContext _db;

    public ManufacturerMySql(SneakersShopContext db)
    {
        _db = db;
    }

    public async Task<ulong> Create(Manufacturer dto)
    {
        _db.Manufacturers.Add(dto);

        await _db.SaveChangesAsync();

        return dto.Id;
    }

    public async Task<IEnumerable<Manufacturer>> Get()
    {
        return await _db.Manufacturers
            .OrderBy(m => m.Id)
            .ToListAsync();
    }

    public async Task<Manufacturer?> Get(ulong id)
    {
        Manufacturer? manufacturer = await _db.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);
        return manufacturer;
    }

    public async Task<bool> Put(ulong id, string name)
    {
        var manufacturer = await this.Get(id);
        if (manufacturer == null)
        {
            return false;
        }
        
        manufacturer.Name = name;
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(ulong id)
    {
        var manufacturer = await this.Get(id);
        if (manufacturer == null)
        {
            return false;
        }
    
        _db.Manufacturers.Remove(manufacturer); 
        await _db.SaveChangesAsync();

        return true;
    }
}