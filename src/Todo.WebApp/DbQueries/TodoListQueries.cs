using System;
using System.Collections.Generic;
using System.Linq;
using Todo.WebApp.DataModels;
using Todo.WebApp.DbModels;

namespace Todo.WebApp.DbQueries
{
    public static class TodoListQueries
    {
        public static TodoList FetchList(this TodoListDbContext db, int listId)
        {
            var list = db.TodoLists.Find(listId);
            var items = db.TodoListItems.Where(i => i.TodoListId == listId);

            return new TodoList(
                listId,
                list.Title,
                items.Select(i => i.ItemDescription)
            );
        }
    }
}
