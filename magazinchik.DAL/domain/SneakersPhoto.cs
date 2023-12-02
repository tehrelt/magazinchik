using System;
using System.Collections.Generic;

namespace magazinchik.DAL.domain;

public partial class SneakersPhoto
{
    public ulong Id { get; set; }
    public ulong SneakerId { get; set; }

    public string PhotoUrl { get; set; }
    public virtual Sneaker Sneaker { get; set; } = null!;
}
