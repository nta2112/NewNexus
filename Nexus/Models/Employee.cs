﻿using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public int ShopId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual RetailShop Shop { get; set; } = null!;
}
