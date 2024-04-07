using Home_Accounting.Data;
using Home_Accounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Home_Accounting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TransactionsDbContext _transactionsDbContext;

        public HomeController(ILogger<HomeController> logger, TransactionsDbContext transactionsDbContext)
        {
            _logger = logger;
            _transactionsDbContext = transactionsDbContext;
        }

        public IActionResult Index()
        {
            var types = _transactionsDbContext.Types.ToList();
            var categories = new List<Category>();

            types.Add(new CategoryType()
            { Id = 0,
              TypeName = "--Select type--",
            });

            categories.Add(new Category() { Id = 0,
            CategoryName = "--Select category---"});
            ViewBag.Types = new SelectList(types, "Id", "TypeName");
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public JsonResult GetCategoryByTypeId(int typeId)
        {
            return Json(_transactionsDbContext.Categories.Where(u => u.TypeId == typeId).ToList());
        }
    }
}