using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.WebApp.MvcModels
{
    /// <summary>
    /// Model class for an item in a to-do list. Exists to allow validation
    /// of incoming values.
    /// </summary>
    public class TodoListItemModel
    {
        [Required(ErrorMessage = "Cannot be empty or whitespace only")]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        [MaxLength(1000, ErrorMessage = "Cannot exceed 1000 characters")]
        public string Description { get; set; }
    }
}
