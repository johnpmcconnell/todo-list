using System;

namespace Todo.WebApp.SharedModels
{
    public class TodoListSummary
    {
        public int Id { get; }
        public string Title { get; }

        public TodoListSummary(int id, string title)
        {
            if (id < 1)
            {
                throw new ArgumentException("Must be a positive integer", nameof(id));
            }

            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Must not be null or empty", nameof(title));
            }

            this.Id = id;
            this.Title = title;
        }
    }
}
