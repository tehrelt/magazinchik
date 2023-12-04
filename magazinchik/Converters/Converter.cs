using magazinchik.DAL.domain;
using magazinchik.DTOs;

namespace magazinchik.Converters;

public static class Converter
{
    public static ManufacturerDto ToDto(this Manufacturer m) => new ManufacturerDto
    {
        Id = m.Id,
        Name = m.Name
    };
    public static BrandDto ToDto(this Brand b) => new BrandDto
    {
        Id = b.Id,
        Name = b.Name,
        ManufacturerName = b.Manufacturer.Name
    };
    public static ClothDto ToDto(this Cloth c) => new ClothDto()
    {
        Id = c.Id,
        Name = c.Name
    };
    public static ZipTypeDto ToDto(this ZipType z) => new ZipTypeDto
    {
        Id = z.Id,
        Name = z.Name
    };
    public static SneakerSizeDto ToDto(this SneakerSize s) => new SneakerSizeDto
    {
        Id = s.Id,
        EuSize = s.EuSize,
        UsSize = s.UsSize,
        CmSize = s.CmSize
    };
    // public static PhotoDto ToDto(this Photo p) => new PhotoDto
    // {
    //     Id = p.Id,
    //     Url = p.Url 
    // };
    public static SneakerDto ToDto(this Sneaker s) => new SneakerDto
    {
        Id = s.Id,
        Name = s.Name,
        Weight = s.Weight,
        Brand = s.Brand.Name,
        Cloth = s.Cloth.Name,
        SneakerSize = s.SneakerSize.ToDto(),
        ZipType = s.ZipType.Name,
        ReleaseDate = s.ReleaseDate,
        Price = s.Price
    };
    public static SneakersPhotoDto ToDto(this SneakersPhoto sp, string url) => new SneakersPhotoDto
    {
        Id = sp.Id,
        SneakerName = sp.Sneaker.Name,
        PhotoUrl = url
    };

    public static PhotoDto ToPhotoDto(this SneakersPhoto sp, string url) => new PhotoDto
    {
        Id = sp.Id,
        Url = url
    };

}