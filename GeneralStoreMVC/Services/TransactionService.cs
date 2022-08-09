using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Transaction;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly GeneralStoreDbContext _context;
        public TransactionService(GeneralStoreDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTransaction(TransactionCreate model)
        {
            if (model == null) return false;

            _context.Transactions.Add(new Transaction
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                DateOfTransaction = DateTime.Now
            });
            if (await _context.SaveChangesAsync() == 1)
                return true;
            return false;
        }
        public async Task<IEnumerable<TransactionListItem>> GetAllTransactions()
        {
            var transcations = await _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Product)
                .Select(Transaction => new TransactionListItem
                {
                    Id = Transaction.Id,
                    CustomerId = Transaction.Customer.Id,
                    ProductId = Transaction.Product.Id,
                    Quantity = Transaction.Quantity,
                    DateOfTransaction = Transaction.DateOfTransaction
                }).ToListAsync();
            return transcations;
        }

        public async Task<TransactionDetail> GetTransactionById(int customerId)
        {
            var transaction = await _context.Transactions
                 .Include(t => t.Product)
                 .Include(t => t.Customer)
                 .FirstOrDefaultAsync(t => t.Id == customerId);

            if (transaction == null) return null;
            var transacationDetail = new TransactionDetail
            {
                Id = transaction.Id,
                ProductId = transaction.ProductId,
                Product = transaction.Product,
                Customer = transaction.Customer,
                CustomerId = transaction.CustomerId,
                Quantity = transaction.Quantity,
                DateOfTransaction = transaction.DateOfTransaction

            };
            return transacationDetail;
        }

        public async Task<bool> UpdateTransaction(TransactionEdit model)
        {
            var transacation = await _context.Transactions.FindAsync(model.Id);
            if (transacation is null) return false;

            transacation.Id = model.Id;
            transacation.ProductId = model.ProductId;
            transacation.CustomerId = model.CustomerId;
            transacation.Quantity = model.Quantity;
            transacation.DateOfTransaction = model.DateOfTransaction;

            if (await _context.SaveChangesAsync() == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteTransaction(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction is null) return false;

            _context.Transactions.Remove(transaction);
            if (await _context.SaveChangesAsync() == 1)
                return true;
            return false;
        }







    }

}


