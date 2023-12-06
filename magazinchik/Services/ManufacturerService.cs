using magazinchik.Converters;
using magazinchik.DAL.domain;
using magazinchik.DTOs;
using magazinchik.Repositories.Interfaces;
using magazinchik.Services.Interfaces;

namespace magazinchik.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly IManufacturerRepository _repo;

    public ManufacturerService(IManufacturerRepository repo)
    {
        _repo = repo;
    }

    public async Task<ulong> Create(ManufacturerInputDto dto)
    {
        Manufacturer manufacturer = new Manufacturer
        {
            Name = dto.Name
        };
        return await _repo.Create(manufacturer);
    }

    public async Task<IEnumerable<ManufacturerDto>> Get() => (await _repo.Get()).Select(m => m.ToDto());
    public async Task<ManufacturerDto?> Get(ulong id) => (await _repo.Get(id))?.ToDto();
    public async Task<bool> Put(ulong id, ManufacturerInputDto dto) => await _repo.Put(id, dto.Name);
    public async Task<bool> Delete(ulong id) => await _repo.Delete(id);
}