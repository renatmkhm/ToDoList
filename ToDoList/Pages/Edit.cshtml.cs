using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoList.Models;
using System.Text.Json;

namespace ToDoList.Pages.ToDoItems
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("ToDoListClient");
            var response = await client.PutAsJsonAsync($"api/ToDoItems/{ToDoModel.id}", ToDoModel);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
