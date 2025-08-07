using StyleStock.domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleStock.domain.Entities;

public partial class Product :BaseEntity
{


    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string Size { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int CategoryId { get; set; }

    public int StockQuantity { get; set; }

    public DateTime EntryDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}
