using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

public class SneakersController : ControllerBase
{
    private readonly SneakersShopContext _context;

    public SneakersController(SneakersShopContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<IdDto>> Post(SneakerInputDto dto)
    {
        var sneaker = new Sneaker
        {
            Name = dto.Name,
            Weight = dto.Weight,
            BrandId = dto.BrandId,
            ClothId = dto.ClothId,
            SneakerSizeId = dto.SneakerSizeId,
            ZipTypeId = dto.ZipTypeId,
            ReleaseDate = dto.ReleaseDate,
            Price = dto.Price
        };

        _context.Sneakers.Add(sneaker);

        await _context.SaveChangesAsync();

        return new IdDto { Id = sneaker.Id };
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SneakerDto>>> Get()
    {
        return await _context.Sneakers
            .Include(s => s.Brand)
            .Include(s => s.Cloth)
            .Include(s => s.SneakerSize)
            .Include(s => s.ZipType)
            .OrderBy(s => s.Id)
            .Select(s => s.ToDto())
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SneakerDto>> Get(ulong id)
    {
        Sneaker? sneaker = await _context.Sneakers
            .Include(s => s.Brand)
            .Include(s => s.Cloth)
            .Include(s => s.SneakerSize)
            .Include(s => s.ZipType)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        return sneaker != null ? sneaker.ToDto() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(ulong id, [FromBody] SneakerInputDto dto)
    {
        Sneaker? sneaker = _context.Sneakers.Find(id);
        if (sneaker == null)
        {
            return NotFound("sneaker wasn't found");
        }

        sneaker.Name = dto.Name;
        sneaker.Weight = dto.Weight;
        sneaker.BrandId = dto.BrandId;
        sneaker.ClothId = dto.ClothId;
        sneaker.SneakerSizeId = dto.SneakerSizeId;
        sneaker.ZipTypeId = dto.ZipTypeId;

        sneaker.ReleaseDate = dto.ReleaseDate;
        sneaker.Price = dto.Price;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        Sneaker? sneaker = _context.Sneakers.Find(id);
        if (sneaker == null)
        {
            return NotFound("sneaker wasn't found");
        }

        _context.Sneakers.Remove(sneaker);

        await _context.SaveChangesAsync();

        return Ok();
    }
}