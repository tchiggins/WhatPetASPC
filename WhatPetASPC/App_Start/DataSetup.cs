using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WhatPetASPC.App_Start
{
    public class DataSetup
    {
        // Populate Species data table
        bool PopulateSpeciesData(Models.Species Data)
        {
            var db = new DAL.PetDB();
            db.AllSpecies.Add(Data);
            db.SaveChanges();
            return true;
        }
        // Get SpeciesID from the chosen SpeciesName
        int getSpeciesID(string SpeciesName)
        {
            var db = new DAL.PetDB();
            var MySpecies = from Species in db.AllSpecies
                            where Species.SpeciesName == SpeciesName
                            select Species.SpeciesID;
            return 0;
        }
        // Get PetClassID from the chosen ClassName
        int getPetClassID(string ClassName)
        {
            var db = new DAL.PetDB();
            var MyPetCLass = from PetClass in db.AllPetClasses
                            where PetClass.ClassName == ClassName
                             select PetClass.PetClassID;
            return 0;
        }
    }
}