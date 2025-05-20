using System;
using System.Collections.Generic;

namespace HalyomorphaHalys.DensityMap.Models;

public partial class BugDensityNotification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public decimal? NotificationLatitude { get; set; }

    public decimal? NotificationLongitude { get; set; }

    public int? NotificationCount { get; set; }

    public DateTime? NotificationDateTime { get; set; }
}
