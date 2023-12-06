using magazinchik.DAL.domain;
using magazinchik.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace magazinchik.Services.Interfaces;

public interface IManufacturerService
{
    Task<ulong> Create(ManufacturerInputDto dto);
    Task<IEnumerable<ManufacturerDto>> Get();
    Task<ManufacturerDto?> Get(ulong id);
    Task<bool> Put(ulong id, ManufacturerInputDto dto);
    Task<bool> Delete(ulong id);
}