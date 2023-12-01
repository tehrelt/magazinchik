using System;
using System.Collections.Generic;

namespace maganzinchik.DAL.domain;

public partial class SneakerSize
{
    public ulong Id { get; set; }

    public double UsSize { get; set; }

    public double EuSize { get; set; }

    public double CmSize { get; set; }

    public virtual ICollection<Sneaker> Sneakers { get; } = new List<Sneaker>();
}
