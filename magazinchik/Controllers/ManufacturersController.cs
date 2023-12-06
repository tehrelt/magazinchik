using magazinchik.DAL;
using magazinchik.DAL.domain;
using magazinchik.Converters;
using magazinchik.DTOs;
using magazinchik.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace magazinchik.Controllers;

[ApiController]
[Route("api/v1/manufacturers")]
public class ManufacturersController : ControllerBase
{
    private readonly IManufacturerService _service;
    public ManufacturersController(IManufacturerService service)
    {
        _service = service;
    }

    [HttpPost] public async Task<ActionResult<IdDto>> Create(ManufacturerInputDto dto) =>Ok(new IdDto { Id = await _service.Create(dto) });
    [HttpGet] public async Task<ActionResult<IEnumerable<ManufacturerDto>>> Get() => Ok(await _service.Get());

    [HttpGet("{id}")]
    public async Task<ActionResult<ManufacturerDto>> Get(ulong id)
    {
        var dto = await _service.Get(id);
        return  dto != null ? dto :  NotFound();
    }
    
    [HttpPut("{id}")] public async Task<ActionResult> Put(ulong id, ManufacturerInputDto dto) => await _service.Put(id, dto) ? Ok() : NotFound();
    
    [HttpDelete("{id}")] public async Task<ActionResult> Delete(ulong id) => await _service.Delete(id) ? Ok() : NotFound();
}