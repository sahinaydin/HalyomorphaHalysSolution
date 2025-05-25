using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalyomorphaHalys.UWP.Models
{
    public class BugDensityNotification
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public decimal? NotificationLatitude { get; set; }

        public decimal? NotificationLongitude { get; set; }

        public int? NotificationCount { get; set; }

        public DateTime? NotificationDateTime { get; set; }
    }
}
