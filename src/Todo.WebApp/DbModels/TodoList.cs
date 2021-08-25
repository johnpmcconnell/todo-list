using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.WebApp.DbModels
{
    [Table("todo_list")]
    public class TodoList
    {
        [Column("todo_list_id")]
        [Key]
        public int TodoListId { get; private set; }

        [Column("title")]
        [Required]
        public string Title { get; private set; }

        // Required for Entity Framework
        private TodoList() { }

        public TodoList(int todoListId, string title)
        {
            this.TodoListId = todoListId;
            this.Title = title;
        }
    }
}
