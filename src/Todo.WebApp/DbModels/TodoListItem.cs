using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.WebApp.DbModels
{
    [Table("todo_list_item")]
    public class TodoListItem
    {
        [Column("todo_list_item_id")]
        [Key]
        public int TodoListItemId { get; private set; }

        [Column("todo_list_id")]
        // ForeignKey in TodoListDbContext.OnModelCreating
        [Required]
        public int TodoListId { get; private set; }

        [Column("item_description")]
        [Required]
        public string ItemDescription { get; private set; }

        // Required for Entity Framework
        private TodoListItem() { }

        public TodoListItem(int todoListItemId, int todoListId, string itemDescription)
        {
            this.TodoListItemId = todoListItemId;
            this.TodoListId = todoListId;
            this.ItemDescription = itemDescription;
        }
    }
}
