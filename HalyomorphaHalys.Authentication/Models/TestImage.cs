using System;
using System.Collections.Generic;

namespace HalyomorphaHalys.Authentication.Models;

public partial class TestImage
{
    public int ImageId { get; set; }

    public int? UserId { get; set; }

    public string? ImageTitle { get; set; }

    public string? ImageName { get; set; }

    public byte[]? ImageFile { get; set; }

    public virtual User? User { get; set; }
}
