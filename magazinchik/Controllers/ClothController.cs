using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/cloths")]
public class ClothController : ControllerBase
{
    private readonly SneakersShopContext _context;
    public ClothController(SneakersShopContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<IdDto>> Create(ClothInputDto dto)
    {
        Cloth cloth = new Cloth
        {
            Name = dto.Name
        };
        
        _context.Cloths.Add(cloth);

        await _context.SaveChangesAsync();

        return new IdDto { Id = cloth.Id };
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClothDto>>> Get()
    {
        return await _context.Cloths
            .OrderBy(c => c.Id)
            .Select(c => c.ToDto())
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClothDto>> GetById(ulong id)
    {
        Cloth? cloth = await _context.Cloths.FindAsync(id);
        return cloth == null ? NotFound() : cloth.ToDto();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(ulong id, ClothInputDto dto)
    {
        Cloth? cloth = await _context.Cloths.FindAsync(id);

        if (cloth == null)
        {
            return NotFound();
        }
        
        cloth.Name = dto.Name;

        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        Cloth? cloth = await _context.Cloths.FindAsync(id);
        if (cloth == null)
        {
            return NotFound();
        }

        _context.Cloths.Remove(cloth);

        await _context.SaveChangesAsync();
        
        return Ok();
    }
}