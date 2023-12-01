using System;
using System.Collections.Generic;

namespace maganzinchik.DAL.domain;

public partial class SneakersPhoto
{
    public ulong Id { get; set; }

    public ulong SneakerId { get; set; }

    public ulong PhotoId { get; set; }

    public virtual Photo Photo { get; set; } = null!;

    public virtual Sneaker Sneaker { get; set; } = null!;
}
