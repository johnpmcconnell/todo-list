using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.DataModels;

namespace Todo.WebApp.MvcModels
{
    public class TodoListEditModel
    {
        [FromRoute]
        public int ListId { get; set; }
        public string Title { get; set; }
        public List<string> Items { get; set; }

        public TodoList ToData()
        {
            return new TodoList(this.ListId, this.Title, this.Items);
        }
    }
}
