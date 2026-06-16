using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankTransaction.Models;

public class TransactionsController : Controller
{
    private readonly TransactionDbContext _context;

    public TransactionsController(TransactionDbContext context)
    {
        _context = context;
    }

    // GET: TRANSACTIONS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Transactions.ToListAsync());
    }

    // GET: TRANSACTIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(m => m.TransactionId == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: TRANSACTIONS/AddOrEdit
    public IActionResult AddOrEdit(int id=0)
    {
        if (id == 0)
        {
            return View(new Transaction());
        }
        else
        {
            return View(_context.Transactions.Find(id));
        }
    }

    // POST: TRANSACTIONS/AddOrEdit
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SwiftCode,Amount,Date")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            if (transaction.TransactionId == 0) 
            {
                transaction.Date = DateTime.Now;
                _context.Add(transaction);

            }
            else
            transaction.Date = DateTime.Now;
            _context.Update(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transaction);
    }

    // GET: TRANSACTIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }
        return View(transaction);
    }

    // POST: TRANSACTIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int? transactionid, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SwiftCode,Amount,Date")] Transaction transaction)
    //{
    //    if (transactionid != transaction.TransactionId)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(transaction);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!TransactionExists(transaction.TransactionId))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(transaction);
    //}

    // GET: TRANSACTIONS/Delete/5
    //public async Task<IActionResult> Delete(int? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var transaction = await _context.Transactions
    //        .FirstOrDefaultAsync(m => m.TransactionId == id);
    //    if (transaction == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(transaction);
    //}

    // POST: TRANSACTIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    //private bool TransactionExists(int? id)
    //{
    //    return _context.Transactions.Any(e => e.TransactionId == id);
    //}
}
