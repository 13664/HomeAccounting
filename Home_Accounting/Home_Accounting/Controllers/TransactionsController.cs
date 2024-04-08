using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Home_Accounting.Data;
using Home_Accounting.Models;
using FluentNHibernate.Utils;

namespace Home_Accounting.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionsDbContext _context;

        public TransactionsController(TransactionsDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(string searchby, string searchValue)
        {


            var transactionsDbContext = _context.Transactions.Include(t => t.Category).Include(t => t.Type);

            if(transactionsDbContext.ToList().Count == 0)
            {
                TempData["InfoMessage"] = "Currently types transactions are not availbale";
            }
            else
            {
                if (string.IsNullOrEmpty(searchValue)){
                    TempData["InfoMessage"] = "Please provide search value";
                }
                else
                {
                    if (searchby.ToLower() == "type")
                    {
                        var searchByType = transactionsDbContext.Where(p=>p.Type.TypeName.ToLower().Contains(searchValue.ToLower()));
                        return View(searchByType);
                    }
                     else if (searchby.ToLower() == "category")
                    {
                        var searchByCat = transactionsDbContext.Where(p => p.Category.CategoryName.ToLower().Contains(searchValue.ToLower()));
                        return View(searchByCat);
                    }
                    else if (searchby.ToLower()=="dateadded")
                    {
                        DateTime searchDate;
                        if (DateTime.TryParse(searchValue, out searchDate))
                        {
                            searchDate = searchDate.Date;

                           
                            var searchByDateAdded = transactionsDbContext.Where(p => p.DateAdded.Date == searchDate);
                            return View(searchByDateAdded);
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");

                    }
                }
            }
            return View(await transactionsDbContext.ToListAsync());
        }


        // GET: Transactions/Create
        public IActionResult Create()
        {
            var types = _context.Types.ToList();
            var categories = new List<Category>();

            types.Add(new CategoryType()
            {
                Id = 0,
                TypeName = "--Select type--",
            });

            categories.Add(new Category()
            {
                Id = 0,
                CategoryName = "--Select category---"
            });

             
            ViewBag.Types = new SelectList(types, "Id", "TypeName");
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            var model = new Transaction();
            model.DateAdded = DateTime.Now;
            return View(model);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,DateAdded,Comment,TypeId,CategoryId")] Transaction transaction)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
*/
            try
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(transaction);

        }

        [HttpGet]
      
        public JsonResult GetCategoryByTypeId(int typeId)
        {
            return Json(_context.Categories.Where(u => u.TypeId == typeId).ToList());
        }


    }
}
