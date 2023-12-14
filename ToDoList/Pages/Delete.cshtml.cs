using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ToDoList.Models;

namespace ToDoList.Pages.ToDoItems
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public ToDoModel ToDoModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient("ToDoListClient");
            var response = await client.GetAsync($"api/ToDoItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                ToDoModel = await JsonSerializer.DeserializeAsync<ToDoModel>(contentStream);
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = _clientFactory.CreateClient("ToDoListClient");
            var response = await client.DeleteAsync($"api/ToDoItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
