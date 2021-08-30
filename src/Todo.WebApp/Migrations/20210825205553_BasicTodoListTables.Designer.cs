﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Todo.WebApp;

namespace Todo.WebApp.Migrations
{
    [DbContext(typeof(TodoListDbContext))]
    [Migration("20210825205553_BasicTodoListTables")]
    partial class BasicTodoListTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Todo.WebApp.DbModels.TodoList", b =>
                {
                    b.Property<int>("TodoListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("todo_list_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.HasKey("TodoListId");

                    b.ToTable("todo_list");
                });

            modelBuilder.Entity("Todo.WebApp.DbModels.TodoListItem", b =>
                {
                    b.Property<int>("TodoListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("todo_list_item_id");

                    b.Property<string>("ItemDescription")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("item_description");

                    b.Property<int>("TodoListId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("todo_list_id");

                    b.HasKey("TodoListItemId");

                    b.HasIndex("TodoListId");

                    b.ToTable("todo_list_item");
                });

            modelBuilder.Entity("Todo.WebApp.DbModels.TodoListItem", b =>
                {
                    b.HasOne("Todo.WebApp.DbModels.TodoList", null)
                        .WithMany()
                        .HasForeignKey("TodoListId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}