using System;
using System.Collections.Generic;

namespace Todo.WebApp.MvcModels
{
    /// <summary>
    /// A representation of the to-do list that contains only raw data,
    /// with fields like surrogate IDs omitted.
    /// </summary>
    public class TodoListDataModel
    {
        public string Title { get; set; }
        public List<string> Items { get; set; }
    }
}
