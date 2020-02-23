namespace WhatPetASPC.Models
{
    public class PetType
    {
        public int PetTypeID { get; set; }
        public string TypeName { get; set; }
        public string Size { get; set; }
        public string Outdoors { get; set; }
        public string PurchaseCost { get; set; }
        public string MonthlyCost { get; set; }
        public string HoursNeeded { get; set; }
        public string PetImage { get; set; }
        public int SpeciesID { get; set; }
        public virtual Species Species { get; set; }
    }
}