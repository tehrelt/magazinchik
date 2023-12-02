using magazinchik.DAL.domain;

namespace magazinchik.DTOs;

public class ManufacturerDto
{
    public ulong Id { get; set; }
    public string Name { get; set; }
}

public class ManufacturerInputDto
{
    public string Name { get; set; }
}