﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhatPetASPC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;



namespace WhatPetASPC.DAL
{
    public class PetDB : DbContext
    {

        public PetDB() : base("DefaultConnection")
        {
        }

        public DbSet<PetClass> AllPetClasses { get; set; }
        public DbSet<Species> AllSpecies { get; set; }
        public DbSet<PetType> AllPetTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}