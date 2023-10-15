using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Musify_web.Models;

namespace Musify_web.Models
{
    public class ProjectDBContext: DbContext
    {

            
        public ProjectDBContext(): base("name=CrudContextDemo")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ProjectDBContext>(null);
            modelBuilder.HasDefaultSchema("dbo");
            //base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Registeration> Registerations { get; set; }
        //public DbSet<State> States { get; set; }
        public DbSet<album> album { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Artist> Artist { get; set; }
        



    }
}