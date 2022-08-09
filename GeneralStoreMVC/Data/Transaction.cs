using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Data
{
    public partial class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfTransaction { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
