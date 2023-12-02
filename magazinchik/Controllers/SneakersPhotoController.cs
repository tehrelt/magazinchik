using magazinchik.Converters;
using magazinchik.DAL;
using magazinchik.DAL.domain;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/sneakers/{sneakerId}/photos")]
public class SneakersPhotoController : ControllerBase
{
    private readonly SneakersShopContext _context;
    public SneakersPhotoController(SneakersShopContext context)
    {
        _context = context;
    }

    private async Task<bool> SneakerExists(ulong id) => await _context.Sneakers.FindAsync(id) != null;
    
    [HttpPost]
    public async Task<ActionResult<IdDto>> Post([FromBody] SneakersPhotoInputDto dto)
    {
        var photo = new SneakersPhoto
        {
            SneakerId = dto.SneakerId,
            PhotoUrl = dto.PhotoUrl
        };

        _context.SneakersPhotos.Add(photo);

        await _context.SaveChangesAsync();

        return new IdDto { Id = photo.Id };
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SneakersPhotoDto>>> Get([FromRoute] ulong sneakerId)
    {
        if (await SneakerExists(sneakerId))
        {
            return NotFound("Sneaker wasn't found");
        }

        return await _context.SneakersPhotos
            .OrderBy (sp => sp.Id)
            .Where   (sp => sp.SneakerId == sneakerId)
            .Select  (sp => sp.ToDto())
            .ToListAsync();
    } 
    
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(ulong sneakerId, ulong id)
    {
        if (await SneakerExists(sneakerId)) {
            return NotFound("Sneaker wasn't found");
        }
        SneakersPhoto? photo = await _context.SneakersPhotos.FindAsync(id);
        return photo != null ? new RedirectResult(photo.PhotoUrl) : NotFound();
    }
}