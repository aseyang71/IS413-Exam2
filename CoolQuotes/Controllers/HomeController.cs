using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoolQuotes.Models;

namespace CoolQuotes.Controllers
{
    public class HomeController : Controller
    {
        //Added the QuoteContext and name it context to be used for the CROD
        private QuotesContext context { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, QuotesContext ctx)
        {
            _logger = logger;

            // Also set the variable context to equal to the QuotesContext
            context = ctx;
        }

        public IActionResult Index()
        {
            return View("Index", context.QuoteLists);
        }


        [HttpGet]
        public IActionResult EnterQuote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterQuote(QuoteList newQuote)
        {
            context.QuoteLists.Add(newQuote);
            context.SaveChanges();

            return RedirectToAction("Index", newQuote);
        }

        [HttpPost]
        public IActionResult DeleteQuote(int quoteID)
        {
            QuoteList quote = context.QuoteLists.First(q => q.QuoteID == quoteID);

            context.Remove(quote);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditQuote(int quoteID)
        {
            QuoteList quote = context.QuoteLists.First(q => q.QuoteID == quoteID);

            return View(quote);
        }

        [HttpPost]
        public IActionResult EditQuote(QuoteList curQuote, int quoteID)
        {
            var updatedQuote = context.QuoteLists.FirstOrDefault(q => q.QuoteID == quoteID);

            if (updatedQuote != null && ModelState.IsValid)
            {
                updatedQuote.Quote = curQuote.Quote;
                updatedQuote.Speaker = curQuote.Speaker;
                updatedQuote.Date = curQuote.Date;
                updatedQuote.Subject = curQuote.Subject;
                updatedQuote.Citation = curQuote.Citation;

                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
                return View("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
