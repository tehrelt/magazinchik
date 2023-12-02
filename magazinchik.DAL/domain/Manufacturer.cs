using System;
using System.Collections.Generic;

namespace magazinchik.DAL.domain;

public partial class Manufacturer
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Brand> Brands { get; } = new List<Brand>();
}
