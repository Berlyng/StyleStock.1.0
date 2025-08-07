using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StyleStock.domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleStock.domain.Entities;

public partial class Purchase:BaseEntity
{
  

    public DateTime PurchaseDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int SupplierID { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
