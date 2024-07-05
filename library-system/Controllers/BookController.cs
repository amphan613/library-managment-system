﻿using library_system.Services;
using library_system.Entities;
using Microsoft.AspNetCore.Mvc;

namespace library_system.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController(IBookService bookService) : ControllerBase
	{
		private readonly IBookService _bookService = bookService;

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Book book)
		{
			bool result = await _bookService.AddAsync(book);
			if (result)
			{
				return Ok(book);
			}
			else
			{
				return BadRequest("Failed to add the book.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] Book book)
		{
			bool result = await _bookService.UpdateAsync(book);
			if (result)
			{
				return Ok(book);
			}
			else
			{
				return NotFound("Book not found.");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			bool result = await _bookService.DeleteAsync(id);
			if (result)
			{
				return Ok($"Book with ID {id} deleted successfully.");
			}
			else
			{
				return NotFound("Book not found.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var book = await _bookService.GetByIdAsync(id);
			if (book != null)
			{
				return Ok(book);
			}
			else
			{
				return NotFound("Book not found.");
			}
		}
	}
}