using maganzinchik.DAL.domain;

namespace magazinchik.DTOs;

public class ManufacturerDto
{
    public ulong Id { get; set; }
    public string Name { get; set; }

    public ManufacturerDto()
    {
        Id = 0;
        Name = string.Empty;
    }
    
    public ManufacturerDto(ulong id, string name)
    {
        Id = id;
        Name = name;
    }

    public ManufacturerDto(Manufacturer manufacturer)
    {
        Id = manufacturer.Id;
        Name = manufacturer.Name;
    }
}