using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using library_ui.Models;

namespace library_ui.Services
{
	public interface IBookService
	{
		Task<Book> AddBookAsync(Book book);
		Task<Book> GetBookAsync(int id);
		Task<IEnumerable<Book>> GetAllBooksAsync();
		Task<Book> UpdateBookAsync(int id, Book book);
		Task DeleteBookAsync(int id);
	}
	public class BookService : IBookService
	{
		private readonly HttpClient _httpClient;
		private const string ContextUrl = "Book";

		public BookService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<Book> AddBookAsync(Book book)
		{
			var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(ContextUrl, bookJson);
			response.EnsureSuccessStatusCode();

			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<Book>(responseBody);
		}

		public async Task<Book> GetBookAsync(int id)
		{
			var response = await _httpClient.GetAsync($"{ContextUrl}/{id}");
			response.EnsureSuccessStatusCode();

			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<Book>(responseBody);
		}

		public async Task<IEnumerable<Book>> GetAllBooksAsync()
		{
			var response = await _httpClient.GetAsync(ContextUrl);
			response.EnsureSuccessStatusCode();

			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<IEnumerable<Book>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task<Book> UpdateBookAsync(int id, Book book)
		{
			var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"{ContextUrl}/{id}", bookJson);
			response.EnsureSuccessStatusCode();

			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<Book>(responseBody);
		}

		public async Task DeleteBookAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"{ContextUrl}/{id}");
			response.EnsureSuccessStatusCode();
		}
	}
}