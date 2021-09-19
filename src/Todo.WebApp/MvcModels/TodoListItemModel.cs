using System;
using System.ComponentModel.DataAnnotations;
using static Todo.WebApp.MvcModels.ValidationConstants;

namespace Todo.WebApp.MvcModels
{
    /// <summary>
    /// Model class for an item in a to-do list. Exists to allow validation
    /// of incoming values.
    /// </summary>
    public class TodoListItemModel
    {
        [Required(ErrorMessage = "Cannot be empty or whitespace only")]
        [MinLength(MinStrLen, ErrorMessage = MinStrError)]
        [MaxLength(MaxStrLen, ErrorMessage = MaxStrError)]
        public string Description { get; set; }
    }
}
