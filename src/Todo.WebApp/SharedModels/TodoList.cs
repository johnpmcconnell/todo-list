using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Todo.WebApp.SharedModels
{
    public class TodoList : TodoListSummary
    {
        public IReadOnlyList<string> Items { get; }

        public TodoList(int id, string title, IEnumerable<string> items)
            : base(id, title)
        {
            if (null == items)
            {
                items = Enumerable.Empty<string>();
            }

            if (items.Any(String.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Cannot contain null, empty, or whitespace only strings", nameof(items));
            }

            this.Items = new ReadOnlyCollection<string>(items.ToList());
        }
    }
}
