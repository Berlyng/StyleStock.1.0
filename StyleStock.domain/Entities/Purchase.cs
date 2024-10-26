using System;
using System.Collections.Generic;

namespace StyleStock.domain.Entities;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int SupplierId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
