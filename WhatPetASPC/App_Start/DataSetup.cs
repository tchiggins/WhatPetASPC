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

        // Load the DataTable
        static DataTable LoadDataTable(string FileName)
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

        public class PetClass
        {
            // Clear the PetClass Table
            public static void ClassTable()
            {
                var db = new DAL.PetDB();
                db.AllPetClasses.RemoveRange(db.AllPetClasses);
                db.SaveChanges();
            }

            // Load the PetClass CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // PetClassID,ClassName
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.PetClass.CSV_FileName);

                int rows;
                DataTable dt = LoadDataTable(CSVPath);
                rows = dt.Rows.Count;

                // Load all the data
                var db = new DAL.PetDB();

                for (int r = 0; r < rows; r++)
                {
                    var pc = new Models.PetClass
                    {
                        PetClassID = Int32.Parse(dt.Rows[r].ItemArray[Constants.PetClass.PetClassIDPos].ToString()),
                        ClassName = dt.Rows[r].ItemArray[Constants.PetClass.ClassNamePos].ToString()
                    };
                    db.AllPetClasses.Add(pc);
                }
                db.SaveChanges();
            }
        }

        public class Species
        {

            // Get PetClassID from the chosen ClassName
            static int getPetClassID(string ClassName)
            {
                var db = new DAL.PetDB();
                var MyPetCLass = from PetClass in db.AllPetClasses
                                 where PetClass.ClassName == ClassName
                                 select PetClass.PetClassID;
                return MyPetCLass.FirstOrDefault();
            }
            
            // Clear the Species Table
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                db.AllSpecies.RemoveRange(db.AllSpecies);
                db.SaveChanges();
            }

            // Load the Species CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // SpeciesID,ClassName,SpeciesName
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.Species.CSV_FileName);

                int rows;
                DataTable dt = LoadDataTable(CSVPath);
                rows = dt.Rows.Count;

                // Load all the data
                var db = new DAL.PetDB();

                for (int r = 0; r < rows; r++)
                {
                    var sp = new Models.Species()
                    {
                        SpeciesID = Int32.Parse(dt.Rows[r].ItemArray[Constants.Species.SpeciesIDPos].ToString()),
                        PetClassID = getPetClassID(dt.Rows[r].ItemArray[Constants.Species.ClassNamePos].ToString()),
                        SpeciesName = dt.Rows[r].ItemArray[Constants.Species.SpeciesNamePos].ToString()
                    };
                    if (sp.PetClassID != 0)
                        db.AllSpecies.Add(sp);
                }
                db.SaveChanges();
            }
        }

        public class PetTypes
        {
            // Get SpeciesID from the chosen SpeciesName
            static int getSpeciesID(string SpeciesName)
            {
                var db = new DAL.PetDB();
                var MySpecies = from Species in db.AllSpecies
                                where Species.SpeciesName == SpeciesName
                                select Species.SpeciesID;

                return MySpecies.FirstOrDefault();
            }
            
            // Clear the PetType Table
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                db.AllPetTypes.RemoveRange(db.AllPetTypes);
                db.SaveChanges();
            }

            // Load the PetTypes CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // PetTypeID,SpeciesName,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.PetTypes.CSV_FileName);

                int rows;
                DataTable dt = LoadDataTable(CSVPath);
                rows = dt.Rows.Count;

                // Load all the data
                var db = new DAL.PetDB();

                for (int r = 0; r < rows; r++)
                {
                    var pt = new Models.PetType()
                    {
                        // PetTypeID,SpeciesName,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage
                        PetTypeID = Int32.Parse(dt.Rows[r].ItemArray[0].ToString()),
                        SpeciesID = getSpeciesID(dt.Rows[r].ItemArray[Constants.PetTypes.SpeciesNamePos].ToString()),
                        TypeName = dt.Rows[r].ItemArray[Constants.PetTypes.TypeNamePos].ToString(),
                        PetSize = dt.Rows[r].ItemArray[Constants.PetTypes.PetSizePos].ToString(),
                        PetSolitary = dt.Rows[r].ItemArray[Constants.PetTypes.PetSolitaryPos].ToString(),
                        PetIndoors = dt.Rows[r].ItemArray[Constants.PetTypes.PetIndoorsPos].ToString(),
                        PetOutdoors = dt.Rows[r].ItemArray[Constants.PetTypes.PetOutdoorsPos].ToString(),
                        PetWalk = dt.Rows[r].ItemArray[Constants.PetTypes.PetWalkPos].ToString(),
                        PetDiet = dt.Rows[r].ItemArray[Constants.PetTypes.PetDietPos].ToString(),
                        PetImage = dt.Rows[r].ItemArray[Constants.PetTypes.PetImagePos].ToString()
                    };
                    if (pt.SpeciesID != 0)
                        db.AllPetTypes.Add(pt);
                }
                db.SaveChanges();
            }
        }
    }
}