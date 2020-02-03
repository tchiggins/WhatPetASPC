using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.Contracts;
using WhatPetASPC.Models;
namespace WhatPetASPC.DAL
{
    public class PetDB : DbContext
    {
        public DbSet<CostCategories> AllCostCategories { get; set; }
        public DbSet<PetClass> AllPetClasses { get; set; }
        public DbSet<Species> AllSpecies { get; set; }
        public DbSet<PetType> AllPetTypes { get; set; }
        public DbSet<Questions> AllQuestions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Contract.Requires(modelBuilder != null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}