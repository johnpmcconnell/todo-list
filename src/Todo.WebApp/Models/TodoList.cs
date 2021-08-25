using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Todo.WebApp.Models
{
    public class TodoList
    {
        public int Id { get; }
        public string Title { get; }
        public ReadOnlyCollection<string> Items { get; }

        public TodoList(int id, string title, IEnumerable<string> items)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentException($"{nameof(title)} must not be null or empty");
            }

            if (items == null)
            {
                items = new List<string>();
            }

            this.Id = id;
            this.Title = title;
            this.Items = new ReadOnlyCollection<string>(items.ToList());
        }
    }
}
