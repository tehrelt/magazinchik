using System;
using System.Collections.Generic;

namespace maganzinchik.DAL.domain;

public partial class Sneaker
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public ulong BrandId { get; set; }
    
    public ulong ClothId { get; set; }

    public ulong SneakerSizeId { get; set; }
    
    public ulong ZipTypeId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public decimal Price { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Cloth Cloth { get; set; } = null!;

    public virtual SneakerSize SneakerSize { get; set; } = null!;

    public virtual ICollection<SneakersPhoto> SneakersPhotos { get; } = new List<SneakersPhoto>();

    public virtual ZipType ZipType { get; set; } = null!;
}
