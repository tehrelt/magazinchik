using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/manufacturers")]
public class ManufacturerController : ControllerBase
{
    private SneakersShopContext _context;
    public ManufacturerController(SneakersShopContext context)
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
            .Select(m => m.ToDto())
            .OrderBy(m => m.Id)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManufacturerDto>> GetById(ulong id)
    {
        Manufacturer manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);

        if (manufacturer == null)
        {
            return NotFound();
        }

        return manufacturer.ToDto();
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