using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatPetASPC.Models
{
    public class PetType
    {
        public int PetTypeID { get; set; }
        public virtual ICollection<Species> SpeciesID { get; set; }
        public string TypeName { get; set; }
        public string PetSize { get; set; }
        public string PetSolitary { get; set; }
        public string PetIndoors { get; set; }
        public string PetOutdoors { get; set; }
        public string PetWalk { get; set; }
        public string PetDiet { get; set; }
        public string PetImage { get; set; }
    }
}