using System;
using System.Collections.Generic;

namespace maganzinchik.DAL.sneakers_shop;

public partial class Manufacturer
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Brand> Brands { get; } = new List<Brand>();
}
