using magazinchik.DAL.domain;
using magazinchik.DTOs;

namespace magazinchik.Repositories.Interfaces;

public interface IManufacturerRepository
{
    Task<ulong> Create(Manufacturer dto);
    Task<IEnumerable<Manufacturer>> Get();
    Task<Manufacturer?> Get(ulong id);
    Task<bool> Put(ulong id, string name);
    Task<bool> Delete(ulong id);
}