using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Todo.WebApp.MvcModels
{
    /// <summary>
    /// A representation of the to-do list that contains only raw data,
    /// with fields like surrogate IDs omitted.
    /// </summary>
    public class TodoListDataModel
    {
        [Required(ErrorMessage = "Cannot be empty or whitespace only")]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        [MaxLength(1000, ErrorMessage = "Cannot exceed 1000 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Cannot be empty or whitespace only")]
        [MinLength(1, ErrorMessage = "At least 1 item required")]
        [MaxLength(500, ErrorMessage = "Cannot exceed 500 items")]
        public List<TodoListItemModel> Items { get; set; }

        public IEnumerable<string> ItemDescriptions
        {
            get => this.Items.Select(i => i.Description);
        }
    }
}
