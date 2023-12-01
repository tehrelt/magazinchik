using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ManufacturerController : ControllerBase
{
    private SneakersShopContext _context;
    public ManufacturerController(SneakersShopContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManufacturerDto>>> Get()
    {
        List<ManufacturerDto> manufacturers = new List<ManufacturerDto>();
        
        await _context.Manufacturers.ForEachAsync(m => manufacturers.Add(m.ToDto()));
        
        return manufacturers;
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
}