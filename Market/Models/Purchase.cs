using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public double Cost { get; set; }
        public int UserId { get; set; }
        public int? Status { get; set; }

        public virtual Product ProductNavigation { get; set; }
        public virtual StatusPurchase StatusNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
