using System.Collections.Generic;
using ToDoListApi.Models;

namespace ToDoListApi
{
    public static class DataStore
    {
        public static List<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
    }
}
