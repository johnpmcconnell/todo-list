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

        public static DataModels.TodoList CreateList(
            this TodoListDbContext db,
            string title,
            IEnumerable<string> items
        )
        {
            var dbList = DbTodoList.ForInsert(title);

            db.TodoLists.Add(dbList);
            db.SaveChanges();

            var dbItems = items
                .Select(item => DbTodoListItem.ForInsert(dbList.TodoListId, item))
                // Must materialize NOW
                // Otherwise iteration will create new instances, which will not
                // have the updated ID values after INSERT.
                .ToList();

            db.TodoListItems.AddRange(dbItems);
            db.SaveChanges();

            return new TodoList(
                dbList.TodoListId,
                dbList.Title,
                dbItems.Select(i => i.ItemDescription)
            );
        }
    }
}
