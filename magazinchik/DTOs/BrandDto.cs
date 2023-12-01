namespace magazinchik.DTOs;

public class BrandDto
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string ManufacturerName { get; set; }
}

public class BrandInputDto
{
    public string Name { get; set; }
    public ulong ManufacturerId { get; set; }
}