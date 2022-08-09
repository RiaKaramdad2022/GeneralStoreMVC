using GeneralStoreMVC.Data;

namespace GeneralStoreMVC.Models.Transaction
{
    public class TransactionDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfTransaction { get; set; }

        public virtual GeneralStoreMVC.Data.Customer Customer { get; set; } = null!;
        public virtual GeneralStoreMVC.Data.Product Product { get; set; } = null!;
    }
}

