using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Todo.WebApp.MvcModels.ValidationConstants;

namespace Todo.WebApp.MvcModels
{
    /// <summary>
    /// A representation of the to-do list that contains only raw data,
    /// with fields like surrogate IDs omitted.
    /// </summary>
    public class TodoListDataModel
    {
        [Required(ErrorMessage = "Cannot be empty or whitespace only")]
        [MinLength(MinStrLen, ErrorMessage = MinStrError)]
        [MaxLength(MaxStrLen, ErrorMessage = MaxStrError)]
        public string Title { get; set; }

        [Required(ErrorMessage = MinListError)]
        [MinLength(MinListLen, ErrorMessage = MinListError)]
        [MaxLength(MaxListLen, ErrorMessage = MaxListError)]
        public List<TodoListItemModel> Items { get; set; }

        [BindNever]
        public IEnumerable<string> ItemDescriptions
        {
            get => this.Items?.Select(i => i.Description) ?? Enumerable.Empty<string>();
        }
    }
}
