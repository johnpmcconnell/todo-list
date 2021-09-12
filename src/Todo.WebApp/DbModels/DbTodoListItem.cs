using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.WebApp.DbModels
{
    [Table("todo_list_item")]
    public class DbTodoListItem
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
        public string ItemDescription { get; set; }

        // Required for Entity Framework
        private DbTodoListItem() { }

        public static DbTodoListItem ForInsert(
            int todoListId,
            string itemDescription
        ) => new DbTodoListItem()
        {
            TodoListId = todoListId,
            ItemDescription = itemDescription
        };
    }
}
