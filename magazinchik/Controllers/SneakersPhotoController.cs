using maganzinchik.DAL;
using maganzinchik.DAL.domain;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/sneakers/{id}/photos")]
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
        if (await SneakerExists(dto.SneakerId) == false)
        {
            NotFound("Sneakers wasn't found for adding an image");
        }
        
        var photo = new SneakersPhoto
        {
            SneakerId = dto.SneakerId,
            PhotoId = dto.PhotoId
        };

        _context.SneakersPhotos.Add(photo);

        await _context.SaveChangesAsync();

        return new IdDto { Id = photo.Id };
    }
}