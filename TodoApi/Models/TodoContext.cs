//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;        //ADDED BY dr FOR EF
using TodoApi.Models;                       //ADDED BY dr FOR EF

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)        //MODIFIED BY dr FOR EF
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }                              //ADDED BY dr FOR EF

        public DbSet<TodoApi.Models.TodoItemDTO> TodoItemDTO { get; set; }        //ADDED BY dr FOR DTO
    }
}
