using System;
using System.Collections.Generic;

namespace maganzinchik.DAL.domain;

public partial class Photo
{
    public ulong Id { get; set; }

    public string Url { get; set; } = null!;

    public virtual ICollection<SneakersPhoto> SneakersPhotos { get; } = new List<SneakersPhoto>();
}
