﻿using System;
using System.Collections.Generic;

namespace maganzinchik.DAL.sneakers_shop;

public partial class Cloth
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Sneaker> Sneakers { get; } = new List<Sneaker>();
}
