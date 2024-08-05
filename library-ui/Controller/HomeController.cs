using library_ui.Services;
using Microsoft.AspNetCore.Mvc;

namespace library_ui.Controllers
{
	public class HomeController(IBookService bookService) : Controller
	{
		private readonly IBookService _bookService = bookService;

		public async Task<IActionResult> Index()
		{
			ViewBag.Data = await _bookService.GetAllBooksAsync();
			return PartialView("_Default");
		}

		public IActionResult AddBook()
		{
			// Your logic here
			return PartialView("~/Views/Books/_AddBook.cshtml");
		}

		public IActionResult DeleteBook()
		{
			// Your logic here
			return View();
		}

		public IActionResult FindBook()
		{
			// Your logic here
			return View();
		}

		public IActionResult UpdateBook()
		{
			// Your logic here
			return View();
		}

		public async Task<IActionResult> Default()
		{
			ViewBag.Data = await _bookService.GetAllBooksAsync();
			return PartialView("_Default");
		}
	}
}
