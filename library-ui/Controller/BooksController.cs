using library_ui.Models;
using library_ui.Services;
using Microsoft.AspNetCore.Mvc;

namespace library_ui.Controllers
{

	[Route("[controller]")]
	public class BooksController : Controller
	{
		private readonly IBookService _bookService;

		public BooksController(IBookService bookService)
		{
			_bookService = bookService;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> AddBook(Book book)
		{
			if (ModelState.IsValid)
			{
				await _bookService.AddBookAsync(book);
				return RedirectToAction("Index", "Home");
			}

			return View(book); // Return the view with validation errors
		}
	}
}
