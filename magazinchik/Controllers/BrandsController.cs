using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/brands")]
public class BrandsController : ControllerBase
{
    private readonly SneakersShopContext _context;

    public BrandsController(SneakersShopContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<IdDto>> Create(BrandInputDto dto)
    {
        Brand brand = new Brand
        {
            Name = dto.Name,
            ManufacturerId = dto.ManufacturerId
        };

        _context.Brands.Add(brand);

        await _context.SaveChangesAsync();

        return new IdDto { Id = brand.Id };
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandDto>>> Get()
    {
        return await _context.Brands
                .Include(b => b.Manufacturer)
                .OrderBy(b => b.Id)
                .Select(b => b.ToDto())
                .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDto>> Get(ulong id)
    {
        Brand? brand = await _context.Brands
            .Include(b => b.Manufacturer)
            .FirstOrDefaultAsync(b => id == b.Id);

        return brand != null ? brand.ToDto() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(ulong id, BrandInputDto dto)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand == null)
        {
            return NotFound();
        }

        brand.Name = dto.Name;
        brand.ManufacturerId = dto.ManufacturerId;

        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand == null)
        {
            return NotFound();
        }

        _context.Brands.Remove(brand);

        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
}