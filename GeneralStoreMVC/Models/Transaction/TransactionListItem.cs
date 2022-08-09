namespace GeneralStoreMVC.Models.Transaction
{
    public class TransactionListItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfTransaction { get; set; }



    }
}
