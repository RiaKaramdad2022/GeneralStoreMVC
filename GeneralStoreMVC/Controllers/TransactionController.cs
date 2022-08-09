using GeneralStoreMVC.Models.Transaction;
using GeneralStoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class TransactionController : Controller
    {
        private ITransactionService _service;
        private ICustomerService _customerService;
        private IProductService _productService;

        public TransactionController(ITransactionService service, ICustomerService customerService, IProductService productService)
        {
            _service = service;
            _customerService = customerService;
            _productService = productService;
        }
        
        public async Task<IActionResult> Index()
        {
            var transactions = await _service.GetAllTransactions();
            return View(transactions);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCreate model)
        {
            if (!ModelState.IsValid)
                {
                    TempData["ErrorMsg"] = "Model State Is Valid";
                    return View(model);
                }
            if (await _service.CreateTransaction(model))
                return RedirectToAction(nameof(Index));

            TempData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);

        }

        public async Task<IActionResult> Details (int id)
        {
            var transaction = await _service.GetTransactionById(id);
            if(transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
                 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _service.GetTransactionById(id);
            if (transaction == null) return NotFound();

            var transactionEdit = new TransactionEdit
            {
                Id = transaction.Id,
                ProductId = transaction.ProductId,
                CustomerId = transaction.CustomerId,
                Quantity = transaction.Quantity,
                DateOfTransaction = transaction.DateOfTransaction
            };
            return View(transactionEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TransactionEdit model)
        {
            if (id != model.Id || !ModelState.IsValid)
                return View(ModelState);

            bool wasUpdated = await _service.UpdateTransaction(model);
            if (wasUpdated) return RedirectToAction(nameof(Index));

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _service.GetTransactionById(id);
            if (transaction == null) return NotFound();
            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTransactin(TransactionDetail model)
        {
            if (await _service.DeleteTransaction(model.Id))
                return RedirectToAction(nameof(Index));
            else
                return BadRequest();
        }
    }


}

