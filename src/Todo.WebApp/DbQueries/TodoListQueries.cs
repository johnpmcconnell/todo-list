using System;
using System.Collections.Generic;
using System.Linq;
using Todo.WebApp.DataModels;
using Todo.WebApp.DbModels;

namespace Todo.WebApp.DbQueries
{
    public static class TodoListQueries
    {
        public static TodoList FetchTodoList(this TodoListDbContext db, int listId)
        {
            var list = db.TodoLists.Find(listId);

            if (null == list)
            {
                return null;
            }

            var items = db.TodoListItems.Where(i => i.TodoListId == listId)
                .OrderBy(i => i.TodoListItemId)
                .ToList();

            return new TodoList(
                listId,
                list.Title,
                items.Select(i => i.ItemDescription)
            );
        }

        public static DataModels.TodoList CreateTodoList(
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
                // Otherwise a second iteration will create new instances, which will
                // not have the updated ID values after INSERT.
                .ToList();

            db.TodoListItems.AddRange(dbItems);
            db.SaveChanges();

            return new TodoList(
                dbList.TodoListId,
                dbList.Title,
                dbItems.Select(i => i.ItemDescription)
            );
        }

        public static void UpdateTodoList(
            this TodoListDbContext db,
            TodoList newValues
        )
        {
            var list = db.TodoLists.Find(newValues.Id);

            if (null == list)
            {
                throw new ArgumentException($"List with ID {newValues.Id} does not exist", nameof(newValues));
            }

            var items = db.TodoListItems.Where(i => i.TodoListId == newValues.Id)
                .OrderBy(i => i.TodoListItemId)
                .ToList();

            if (list.Title != newValues.Title)
            {
                list.Title = newValues.Title;
            }

            int smallerCount = Math.Min(newValues.Items.Count, items.Count);

            for (int i = 0; i < smallerCount; i++)
            {
                if (items[i].ItemDescription != newValues.Items[i])
                {
                    items[i].ItemDescription = newValues.Items[i];
                    db.Update(items[i]);
                }
            }

            if (newValues.Items.Count > items.Count)
            {
                db.AddRange(
                    newValues.Items.Skip(smallerCount)
                        .Select(item => DbTodoListItem.ForInsert(list.TodoListId, item))
                );
            }

            if (items.Count > newValues.Items.Count)
            {
                db.RemoveRange(items.Skip(smallerCount));
            }

            db.SaveChanges();
        }
    }
}
