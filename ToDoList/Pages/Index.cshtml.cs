using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Pages.ToDoItems
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<ToDoModel> ToDoItems { get; private set; }

        public async Task OnGetAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/ToDoItems");
            var client = _clientFactory.CreateClient("ToDoListClient");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                ToDoItems = await JsonSerializer.DeserializeAsync<IEnumerable<ToDoModel>>(contentStream);
            }
            else
            {
                ToDoItems = Array.Empty<ToDoModel>();
            }
        }
    }
}
