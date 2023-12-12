using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoListApi.Models;

namespace ToDoListApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> Get()
        {
            return Ok(DataStore.ToDoItems);
        }

        [HttpPost]
        public ActionResult<ToDoItem> Post([FromBody] ToDoItem item)
        {
            item.Id = DataStore.ToDoItems.Count + 1;
            DataStore.ToDoItems.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ToDoItem item)
        {
            var index = DataStore.ToDoItems.FindIndex(existingItem => existingItem.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            DataStore.ToDoItems[index] = item;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = DataStore.ToDoItems.Find(existingItem => existingItem.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            DataStore.ToDoItems.Remove(item);
            return NoContent();
        }
    }
}
