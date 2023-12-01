namespace magazinchik.DTOs;

public class SneakerDto
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public string Brand { get; set; }
    public string Cloth { get; set; }
    public SneakerSizeDto SneakerSize { get; set; }
    public string ZipType { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
}

public class SneakerInputDto
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public ulong BrandId { get; set; }
    public ulong ClothId { get; set; }
    public ulong SneakerSizeId { get; set; }
    public ulong ZipTypeId { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
}
