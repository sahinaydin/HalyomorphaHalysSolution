using System;
using System.Collections.Generic;

namespace HalyomorphaHalys.Authentication.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<BugDensityNotification> BugDensityNotifications { get; set; } = new List<BugDensityNotification>();

    public virtual ICollection<TestImage> TestImages { get; set; } = new List<TestImage>();
}
