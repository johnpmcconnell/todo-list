using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Todo.WebApp.SharedModels
{
    public class TodoList
    {
        public int Id { get; }
        public string Title { get; }
        public IReadOnlyList<string> Items { get; }

        public TodoList(int id, string title, IEnumerable<string> items)
        {
            if (id < 1)
            {
                throw new ArgumentException("Must be a positive integer", nameof(id));
            }

            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Must not be null or empty", nameof(title));
            }

            if (null == items)
            {
                items = Enumerable.Empty<string>();
            }

            if (items.Any(String.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Cannot contain null, empty, or whitespace only strings", nameof(items));
            }

            this.Id = id;
            this.Title = title;
            this.Items = new ReadOnlyCollection<string>(items.ToList());
        }
    }
}
