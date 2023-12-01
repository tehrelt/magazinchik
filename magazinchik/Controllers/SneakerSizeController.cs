using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/sneaker_sizes")]
public class SneakerSizeController : ControllerBase
{
    private readonly SneakersShopContext _context;
    public SneakerSizeController(SneakersShopContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<IdDto>> Create(SneakerSizeInputDto dto)
    {
        SneakerSize size = new SneakerSize
        {
            EuSize = dto.EuSize,
            UsSize = dto.UsSize,
            CmSize = dto.CmSize
        };
        
        _context.SneakerSizes.Add(size);

        await _context.SaveChangesAsync();

        return new IdDto { Id = size.Id };
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SneakerSizeDto>>> Get()
    {
        return await _context.SneakerSizes
            .OrderBy(m => m.Id)
            .Select(m => m.ToDto())
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SneakerSizeDto>> Get(ulong id)
    {
        SneakerSize? sneakerSize = await _context.SneakerSizes.FindAsync(id);
        return sneakerSize != null ? sneakerSize.ToDto() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(ulong id, SneakerSizeInputDto dto)
    {
        SneakerSize? sneakerSize = await _context.SneakerSizes.FindAsync(id);

        if (sneakerSize == null)
        {
            return NotFound();
        }

        sneakerSize.EuSize = dto.EuSize;
        sneakerSize.UsSize = dto.UsSize;
        sneakerSize.CmSize = dto.CmSize;

        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        SneakerSize? size = await _context.SneakerSizes.FindAsync(id);
        if (size == null)
        {
            return NotFound();
        }

        _context.SneakerSizes.Remove(size);

        await _context.SaveChangesAsync();
        
        return Ok();
    }
}