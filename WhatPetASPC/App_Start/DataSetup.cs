using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper;
using System.IO;
using System.Data;

namespace WhatPetASPC.App_Start
{
    public class DataSetup
    {
        // Clear the PetClass Table
        public static void ClearPetClassTable()
        {
            var db = new DAL.PetDB();
            db.AllPetClasses.RemoveRange(db.AllPetClasses);
            db.SaveChanges();
        }

        // Clear the Species Table
        public static void ClearSpeciesTable()
        {
            var db = new DAL.PetDB();
            db.AllSpecies.RemoveRange(db.AllSpecies);
            db.SaveChanges();
        }
        // Load the DataTable
        public static DataTable LoadDataTable(string FileName)
        {
            var dt = new DataTable();
            using (var reader = new StreamReader(FileName))
            using (var csv = new CsvReader(reader))
            {
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {
                    dt.Load(dr);
                }
            }
            return dt;
        }
        // Load the PetClass CSV file
        public static void PC_CSVImport()
        {
            // Upload and save the file
            string CSVPath = HttpContext.Current.Server.MapPath("~/Files/PetClass.csv");

            int rows;
            DataTable dt = LoadDataTable(CSVPath);
            rows = dt.Rows.Count;

            // Load all the data
            var db = new DAL.PetDB();

            for (int r = 0; r < rows; r++)
            {
                var pc = new Models.PetClass
                {
                    PetClassID = Int32.Parse(dt.Rows[r].ItemArray[0].ToString()),
                    ClassName = dt.Rows[r].ItemArray[1].ToString()
                };
                db.AllPetClasses.Add(pc);
            }
            db.SaveChanges();
        }
        // Load the Species CSV file
        public static void SP_CSVImport()
        {
            // Upload and save the file
            string CSVPath = HttpContext.Current.Server.MapPath("~/Files/Species.csv");

            int rows;
            DataTable dt = LoadDataTable(CSVPath);
            rows = dt.Rows.Count;

            // Load all the data
            var db = new DAL.PetDB();

            for (int r = 0; r < rows; r++)
            {
                var sp = new Models.Species()
                {
                    SpeciesID = Int32.Parse(dt.Rows[r].ItemArray[0].ToString()),
                    PetClassID = getPetClassID(dt.Rows[r].ItemArray[1].ToString()),
                    SpeciesName = dt.Rows[r].ItemArray[2].ToString()
                };
                if (sp.PetClassID != 0)
                    db.AllSpecies.Add(sp);
            }
            db.SaveChanges();
        }
        // Get SpeciesID from the chosen SpeciesName
        static int getSpeciesID(string SpeciesName)
        {
            var db = new DAL.PetDB();
            var MySpecies = from Species in db.AllSpecies
                            where Species.SpeciesName == SpeciesName
                            select Species.SpeciesID;
            return 0;
        }
        // Get PetClassID from the chosen ClassName
        static int getPetClassID(string ClassName)
        {
            var db = new DAL.PetDB();
            var MyPetCLass = from PetClass in db.AllPetClasses
                            where PetClass.ClassName == ClassName
                             select PetClass.PetClassID;
            return MyPetCLass.FirstOrDefault();
        }
    }
}