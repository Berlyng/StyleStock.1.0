using System;
using System.Collections.Generic;

namespace StyleStock.domain.Entities;

public partial class PurchaseDetail
{
    public int DetailId { get; set; }

    public int PurchaseId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Purchase Purchase { get; set; } = null!;
}
