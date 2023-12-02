using magazinchik.DAL;
using magazinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace magazinchik.Controllers
{
    [ApiController]
    [Route("api/v1/zips")]
    public class ZipsController : ControllerBase
    {
        private readonly SneakersShopContext _context;

        public ZipsController(SneakersShopContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ActionResult<IdDto>> Post([FromBody] ZipTypeInputDto dto)
        {
            ZipType zip = new ZipType
            {
                Name = dto.Name
            };

            _context.ZipTypes.Add(zip);

            await _context.SaveChangesAsync();

            return new IdDto {Id = zip.Id};
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZipTypeDto>>> Get()
        {
            return await _context.ZipTypes
                .OrderBy(z => z.Id)
                .Select(z => z.ToDto())
                .ToListAsync();
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ZipTypeDto>> Get(ulong id)
        {
            ZipType? zip = await _context.ZipTypes.FindAsync(id);
            return zip != null ? zip.ToDto() : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(ulong id, [FromBody] ZipTypeInputDto dto)
        {
            ZipType? zip = await _context.ZipTypes.FindAsync(id);
            if (zip == null)
            {
                return NotFound();
            }

            zip.Name = dto.Name;

            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ZipType? zip = await _context.ZipTypes.FindAsync(id);
            if (zip == null)
            {
                return NotFound();
            }

            _context.ZipTypes.Remove(zip);

            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
