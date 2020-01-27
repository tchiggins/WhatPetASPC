﻿namespace WhatPetASPC.Models
{
    public class PetType
    {
        public int PetTypeID { get; set; }
        public string TypeName { get; set; }
        public string PetSize { get; set; }
        public string PetSolitary { get; set; }
        public string PetIndoors { get; set; }
        public string PetOutdoors { get; set; }
        public string PetWalk { get; set; }
        public string PetDiet { get; set; }
        public int CostID { get; set; }
        public string PetImage { get; set; }
        public int SpeciesID { get; set; }
        public virtual Species Species { get; set; }
        public virtual CostCategories CostCategories { get; set; }
    }
}