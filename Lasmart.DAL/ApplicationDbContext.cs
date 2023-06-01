using System;
using Lastmart.Domain.DBModels;
using Lastmart.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lasmart.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }
        
        /// <summary>
        /// Таблица точек.
        /// </summary>
        public DbSet<DotModel> Dots { get; set; }
        
        /// <summary>
        /// Таблица комментариев.
        /// </summary>
        public DbSet<CommentModel> Comments { get; set; }
    }
}