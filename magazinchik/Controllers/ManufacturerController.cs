using maganzinchik.DAL;
using maganzinchik.DAL.domain;
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
    public async Task<ActionResult<IEnumerable<Manufacturer>>> Get()
    {
        return await _context.Manufacturers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Manufacturer>> GetById(ulong id)
    {
        Manufacturer manufacturer = _context.Manufacturers.FirstOrDefault(m => m.Id == id);

        if (manufacturer == null)
        {
            return NotFound();
        }

        return manufacturer;
    }
}