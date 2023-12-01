using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/photos")]
public class PhotosController : ControllerBase
{
    private readonly SneakersShopContext _context;
    public PhotosController(SneakersShopContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<IdDto>> Create(PhotoInputDto dto)
    {
        Photo photo = new Photo
        {
            Url = dto.Url
        };
        
        _context.Photos.Add(photo);

        await _context.SaveChangesAsync();

        return new IdDto { Id = photo.Id };
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhotoDto>>> Get()
    {
        return await _context.Photos
            .OrderBy(c => c.Id)
            .Select(c => c.ToDto())
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoDto>> Get(ulong id)
    {
        Photo? photo = await _context.Photos.FindAsync(id);
        return photo != null ? photo.ToDto() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(ulong id, PhotoInputDto dto)
    {
        Photo? photo = await _context.Photos.FindAsync(id);

        if (photo == null)
        {
            return NotFound();
        }

        photo.Url = dto.Url;

        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        Photo? photo = await _context.Photos.FindAsync(id);
        if (photo == null)
        {
            return NotFound();
        }

        _context.Photos.Remove(photo);

        await _context.SaveChangesAsync();
        
        return Ok();
    }
}