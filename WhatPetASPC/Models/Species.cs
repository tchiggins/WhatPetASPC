namespace WhatPetASPC.Models
{
    public class Species
    {
        public int SpeciesID { get; set; }
        public string SpeciesName { get; set; }
        public int PetClassID { get; set; }
        public virtual PetClass PetClass { get; set; }
    }
}