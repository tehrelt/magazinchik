using maganzinchik.DAL.domain;
using magazinchik.DTOs;

namespace magazinchik.Converters;

public static class Converter
{
    public static ManufacturerDto ToDto(this Manufacturer manufacturer)
    {
        return new ManufacturerDto
        {
            Id = manufacturer.Id,
            Name = manufacturer.Name
        };
    }

    public static BrandDto ToDto(this Brand brand)
    {
        return new BrandDto
        {
            Id = brand.Id,
            Name = brand.Name,
            ManufacturerName = brand.Manufacturer.Name
        };
    }
}