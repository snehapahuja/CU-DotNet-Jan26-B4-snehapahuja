using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickLoanTracker.Models;

namespace QuickLoanTracker.Controllers
{
        public class LoanController : Controller
        {
            private static List

            <Loan> loans = new List<Loan>();

            public IActionResult Index()
            {
                return View(loans);
            }

            public IActionResult Add()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Add(Loan loan)
            {
                if (ModelState.IsValid)
                {
                    loan.Id = loans.Count + 1;
                    loans.Add(loan);
                    return RedirectToAction("Index");
                }

                return View(loan);
            }

            public IActionResult Edit(int id)
            {
                var loan = loans.FirstOrDefault(x => x.Id == id);
                return View(loan);
            }

            [HttpPost]
            public IActionResult Edit(Loan loan)
            {
                var existing = loans.FirstOrDefault(x => x.Id == loan.Id);

                if (existing != null)
                {
                    existing.BorrowerName = loan.BorrowerName;
                    existing.LenderName = loan.LenderName;
                    existing.Amount = loan.Amount;
                    existing.IsSettled = loan.IsSettled;
                }

                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                var loan = loans.FirstOrDefault(x => x.Id == id);

                if (loan != null)
                {
                    loans.Remove(loan);
                }

                return RedirectToAction("Index");
            }
        }
}
