using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Todo.WebApp.DbModels;

#nullable disable

namespace Todo.WebApp
{
    public class TodoConnectInterceptor : DbConnectionInterceptor
    {
        public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        {
            base.ConnectionOpened(connection, eventData);
        }
    }

    public partial class TodoListDbContext : DbContext
    {
        public DbSet<DbTodoList> TodoLists { get; set; }
        public DbSet<DbTodoListItem> TodoListItems { get; set; }

        public TodoListDbContext()
        {
        }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connStrEnvVar = "TODOLISTDB";
            if (!optionsBuilder.IsConfigured)
            {
                string connStr = Environment.GetEnvironmentVariable(connStrEnvVar);

                // Because we're appending the connection string, we need to validate
                // it's not empty ourselves
                if (String.IsNullOrEmpty(connStr))
                {
                    throw new InvalidOperationException($"Connection string is empty. Set {connStrEnvVar} environment variable.");
                }

                // SQLite does not enforce FK constraints by default.
                // Do not depend on configuring user to enable them.
                // Equivalent to executing PRAGMA foreign_keys = ON
                connStr += ";" + "Foreign Keys=true";
                optionsBuilder.UseSqlite(connStr)
                #if DEBUG
                    .LogTo(q => System.Diagnostics.Debug.WriteLine($"EF {q}"))
                #endif
                ;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<DbTodoListItem>(eb =>
            {
                eb.HasOne<DbTodoList>()
                    .WithMany()
                    .HasForeignKey(c => c.TodoListId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
