using maganzinchik.DAL.domain;
using magazinchik.DTOs;

namespace magazinchik.Converters;

public static class Converter
{
    public static ManufacturerDto ToDto(this Manufacturer m)
    {
        return new ManufacturerDto
        {
            Id = m.Id,
            Name = m.Name
        };
    }
    public static BrandDto ToDto(this Brand b)
    {
        return new BrandDto
        {
            Id = b.Id,
            Name = b.Name,
            ManufacturerName = b.Manufacturer.Name
        };
    }
    public static ClothDto ToDto(this Cloth c)
    {
        return new ClothDto()
        {
            Id = c.Id,
            Name = c.Name
        };
    }
    public static ZipTypeDto ToDto(this ZipType z)
    {
        return new ZipTypeDto
        {
            Id = z.Id,
            Name = z.Name
        };
    }
    public static SneakerSizeDto ToDto(this SneakerSize s)
    {
        return new SneakerSizeDto
        {
            Id = s.Id,
            EuSize = s.EuSize,
            UsSize = s.UsSize,
            CmSize = s.CmSize
        };
    }
}