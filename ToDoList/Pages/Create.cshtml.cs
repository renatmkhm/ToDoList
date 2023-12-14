using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Pages.ToDoItems
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public ToDoModel ToDoModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("ToDoListClient");
            var response = await client.PostAsJsonAsync("api/ToDoItems", ToDoModel);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
