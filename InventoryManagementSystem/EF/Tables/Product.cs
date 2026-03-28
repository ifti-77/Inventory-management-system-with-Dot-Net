using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.EF.Tables;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
