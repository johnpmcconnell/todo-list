﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Todo.WebApp;

namespace Todo.WebApp.Migrations
{
    [DbContext(typeof(TodoListDbContext))]
    [Migration("20210825203910_InitialEmptyDb")]
    partial class InitialEmptyDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");
#pragma warning restore 612, 618
        }
    }
}
