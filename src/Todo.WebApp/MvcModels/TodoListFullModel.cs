using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.SharedModels;

namespace Todo.WebApp.MvcModels
{
    public class TodoListFullModel : TodoListDataModel
    {
        [FromRoute]
        public int ListId { get; set; }

        public TodoList ToShared()
        {
            return new TodoList(this.ListId, this.Title, this.ItemDescriptions);
        }
    }
}
