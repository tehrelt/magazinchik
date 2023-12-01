using System;
using System.Collections.Generic;

namespace maganzinchik.DAL.domain;

public partial class ZipType
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Sneaker> Sneakers { get; } = new List<Sneaker>();
}
