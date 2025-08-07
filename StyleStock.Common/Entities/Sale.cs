using StyleStock.domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleStock.domain.Entities;

public partial class Sale:BaseEntity
{

    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();

    public virtual User User { get; set; } = null!;
}
