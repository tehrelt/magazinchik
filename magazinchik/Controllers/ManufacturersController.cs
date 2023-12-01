using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/manufacturers")]
public class ManufacturersController : ControllerBase
{
    private readonly SneakersShopContext _context;
    public ManufacturersController(SneakersShopContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<IdDto>> Create(ManufacturerInputDto dto)
    {
        Manufacturer manufacturer = new Manufacturer
        {
            Name = dto.Name
        };
        
        _context.Manufacturers.Add(manufacturer);

        await _context.SaveChangesAsync();

        return new IdDto { Id = manufacturer.Id };
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManufacturerDto>>> Get()
    {
        return await _context.Manufacturers
            .OrderBy(m => m.Id)
            .Select(m => m.ToDto())
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManufacturerDto>> Get(ulong id)
    {
        Manufacturer manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);
        return manufacturer != null ? manufacturer.ToDto() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(ulong id, ManufacturerInputDto dto)
    {
        Manufacturer? manufacturer = await _context.Manufacturers.FindAsync(id);

        if (manufacturer == null)
        {
            return NotFound();
        }
        
        manufacturer.Name = dto.Name;

        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        Manufacturer? manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }

        _context.Manufacturers.Remove(manufacturer);

        await _context.SaveChangesAsync();
        
        return Ok();
    }
}