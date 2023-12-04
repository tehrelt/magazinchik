using magazinchik.Converters;
using magazinchik.DAL;
using magazinchik.DAL.domain;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/sneakers/{sneakerId}/photos")]
public class SneakersPhotoController : ControllerBase
{
    private readonly SneakersShopContext _context;
    private readonly Configs.MinioConfig _minioConfig;
    public SneakersPhotoController(SneakersShopContext context, IOptions<Configs.MinioConfig> minioConfig)
    {
        _context = context;
        _minioConfig = minioConfig.Value;
    }

    private async Task<bool> SneakerExists(ulong id) => await _context.Sneakers.FindAsync(id) != null;

    [HttpPost]
    public async Task<ActionResult> Post([FromRoute] ulong sneakerId,
                                         [FromForm]  IFormFile file) 
    {
        if (await SneakerExists(sneakerId) == false)
        {
            return BadRequest($"Sneaker#{sneakerId} wasn't found");
        }
        
        var objectName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        using (var minio = new MinioClient()
                   .WithEndpoint(_minioConfig.Endpoint)
                   .WithCredentials(_minioConfig.AccessToken, _minioConfig.SecretToken)
                   .WithSSL(false)
                   .Build())
        {
            try
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(objectName.ToString())
                    .WithStreamData(file.OpenReadStream())
                    .WithObjectSize(file.Length)
                    .WithContentType(file.ContentType);
                
                await minio.PutObjectAsync(putObjectArgs);
            }
            catch (AuthorizationException e)
            {
                return BadRequest("minio not authorized");
            }
            catch (BucketNotFoundException e)
            {
                return BadRequest($"bucket '{_minioConfig.BucketName}' wasn't found");
            }
            catch (Exception e)
            {
                return BadRequest($"Unexpected error at SneakersPhotoController {e.Message}");
            }
        }
        
        var photo = new SneakersPhoto
        {
            SneakerId = sneakerId,
            PhotoUrl = objectName
        };
        
        _context.SneakersPhotos.Add(photo);
        
        await _context.SaveChangesAsync();

        return Created($"api/v1/sneakers/{sneakerId}/photos/{photo.Id}", photo.Id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SneakersPhotoDto>>> Get([FromRoute] ulong sneakerId)
    {
        if (await SneakerExists(sneakerId) == false)
        {
            return NotFound($"Sneaker#{sneakerId} wasn't found");
        }

        return Ok(new SneakersPhotosDto
        {
            Photos = _context.SneakersPhotos.Where(p => p.SneakerId == sneakerId).Select(p => $"http://{_minioConfig.Endpoint}/{_minioConfig.BucketName}/{p.PhotoUrl}").ToArray(),
            Count = _context.SneakersPhotos.Count(p => p.SneakerId == sneakerId)
        });
    } 
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SneakersPhotoDto>> Get(ulong sneakerId, ulong id)
    {
        if (await SneakerExists(sneakerId) == false) {
            return NotFound($"Sneaker#{sneakerId} wasn't found");
        }
        SneakersPhoto? photo = await _context.SneakersPhotos
            .Include(p => p.Sneaker)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (photo == null)
        {
            return NotFound($"Photo#{id} wasn't found");
        }

        var dto = photo.ToDto($"http://{_minioConfig.Endpoint}/{_minioConfig.BucketName}/{photo.PhotoUrl}");

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong sneakerId, ulong id)
    {
        if (await SneakerExists(sneakerId) == false)
        {
            return NotFound($"Sneaker#{sneakerId} wasn't found");
        }

        SneakersPhoto? photo = await _context.SneakersPhotos.FindAsync(id);
        if (photo == null)
        {
            return NotFound($"Photo#{id} wasn't found");
        }
        
        using (var minio = new MinioClient()
                   .WithEndpoint(_minioConfig.Endpoint)
                   .WithCredentials(_minioConfig.AccessToken, _minioConfig.SecretToken)
                   .WithSSL(false)
                   .Build())
        {
            try
            {
                var roArgs = new RemoveObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(photo.PhotoUrl);
                
                await minio.RemoveObjectAsync(roArgs);
            }
            catch (AuthorizationException e)
            {
                return BadRequest("minio not authorized");
            }
            catch (BucketNotFoundException e)
            {
                return BadRequest($"bucket '{_minioConfig.BucketName}' wasn't found");
            }
            catch (Exception e)
            {
                return BadRequest($"Unexpected error at SneakersPhotoController {e.Message}");
            }
        }
        
        _context.SneakersPhotos.Remove(photo);
        await _context.SaveChangesAsync();
            
        return Ok();
    }
}