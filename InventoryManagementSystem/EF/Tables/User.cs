using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.EF.Tables;

public partial class User
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}
