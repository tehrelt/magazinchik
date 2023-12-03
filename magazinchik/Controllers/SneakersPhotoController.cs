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
                                         [FromBody]  SneakersPhotoInputDto dto)
    {
        if (await SneakerExists(sneakerId) == false)
        {
            return BadRequest($"Sneaker#{sneakerId} wasn't found");
        }
        
        var objectName = System.Guid.NewGuid();
        using (var minio = new MinioClient()
                   .WithEndpoint(_minioConfig.Endpoint)
                   .WithCredentials(_minioConfig.AccessToken, _minioConfig.SecretToken)
                   .WithSSL(false)
                   .Build())
        {
            try
            {
                var bytes = Convert.FromBase64String(dto.PhotoString);
                
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(objectName.ToString())
                    .WithStreamData(new MemoryStream(bytes))
                    .WithObjectSize(bytes.Length)
                    .WithContentType("application/octet-stream");
                
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
            PhotoUrl = objectName.ToString()
        };
        
        _context.SneakersPhotos.Add(photo);
        
        await _context.SaveChangesAsync();

        return Ok(photo.Id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SneakersPhotoDto>>> Get([FromRoute] ulong sneakerId)
    {
        if (await SneakerExists(sneakerId) == false)
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
        if (await SneakerExists(sneakerId) == false) {
            return NotFound("Sneaker wasn't found");
        }
        SneakersPhoto? photo = await _context.SneakersPhotos.FindAsync(id);
        return photo != null ? Ok(photo.PhotoUrl) : NotFound();
    }
}