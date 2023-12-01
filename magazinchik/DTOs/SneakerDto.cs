namespace magazinchik.DTOs;

public class SneakerDto
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public string Brand { get; set; }
    public string Cloth { get; set; }
    public string SneakerSize { get; set; }
    public string ZipType { get; set; }
}

public class SneakerInputDto
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public ulong BrandId { get; set; }
    public ulong ClothId { get; set; }
    public ulong SneakerSizeTypeId { get; set; }
    public ulong ZipTypeId { get; set; }
}
