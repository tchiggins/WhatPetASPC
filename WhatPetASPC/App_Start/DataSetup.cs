using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using CsvHelper;
using Serilog;
namespace WhatPetASPC.App_Start
{
    public static class DataSetup
    {
        // Load the DataTable
        private static DataTable LoadDataTable(string FileName, ref DataTable dt)
        {
            string InfoLog;
            using (var reader = new StreamReader(FileName as String))
            using (var csv = new CsvReader(reader))
            {
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                InfoLog = "Attempting to load DataTable from ";
                InfoLog += FileName;
                InfoLog += "...";
                Log.Information(InfoLog);
                try
                {
                    using (var dr = new CsvDataReader(csv))
                    {
                        dt.Load(dr);
                    }
                    InfoLog = "DataTable loaded successfully from ";
                    InfoLog += FileName;
                    Log.Information(InfoLog);
                }
                catch (Exception e)
                {
                    InfoLog = "DataTable load failed from ";
                    InfoLog += FileName;
                    Log.Error(InfoLog);
                    Log.Error(e.Message);
                }
            }
            return dt;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Error prevention")]
        public static class PetClass
        {
            // Clear the PetClass Table
            public static void ClassTable()
            {
                var db = new DAL.PetDB();
                db.AllPetClasses.RemoveRange(db.AllPetClasses);
                db.SaveChanges();
                db.Dispose();
            }
            // Load the PetClass CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // PetClassID,ClassName
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.PetClass.CSV_FileName);
                var dt = new DataTable();
                LoadDataTable(CSVPath, ref dt);
                int rows = dt.Rows.Count;
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
                db.Dispose();
                dt.Dispose();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Error prevention")]
        public static class Species
        {
            // Get PetClassID from the chosen ClassName
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Error prevention")]
            public static int getPetClassID(string ClassName)
            {
                var db = new DAL.PetDB();
                var MyPetClass = from PetClass in db.AllPetClasses
                                 where PetClass.ClassName == ClassName
                                 select PetClass.PetClassID;
                db.SaveChanges();
                return MyPetClass.FirstOrDefault();
            }
            // Clear the Species Table
            [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "Warning reason unclear")]
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                db.AllSpecies.RemoveRange(db.AllSpecies);
                db.SaveChanges();
                db.Dispose();
            }
            // Load the Species CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // SpeciesID,ClassName,SpeciesName
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.Species.CSV_FileName);
                var dt = new DataTable();
                LoadDataTable(CSVPath, ref dt);
                int rows = dt.Rows.Count;
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
                    {
                        db.AllSpecies.Add(sp);
                    }
                }
                db.SaveChanges();
                db.Dispose();
                dt.Dispose();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Error prevention")]
        public static class CostCategories
        {
            // Clear the CostCategories table
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                db.AllCostCategories.RemoveRange(db.AllCostCategories);
                db.SaveChanges();
                db.Dispose();
            }
            // Load the CostCategories CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // QuestionID,QuestionText
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.CostCategories.CC_FileName);
                var dt = new DataTable();
                LoadDataTable(CSVPath, ref dt);
                int rows = dt.Rows.Count;
                // Load all the data
                var db = new DAL.PetDB();
                for (int r = 0; r < rows; r++)
                {
                    var cc = new Models.CostCategories()
                    {

                    };
                }
                db.SaveChanges();
                db.Dispose();
                dt.Dispose();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Error prevention")]
        public static class PetTypes
        {
            // Get SpeciesID from the chosen SpeciesName
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Error prevention")]
            public static int getSpeciesID(string SpeciesName)
            {
                var db = new DAL.PetDB();
                var MySpecies = from Species in db.AllSpecies
                                where Species.SpeciesName == SpeciesName
                                select Species.SpeciesID;
                db.SaveChanges();
                return MySpecies.FirstOrDefault();
            }
            // Clear the PetType Table
            [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "Warning reason unclear")]
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                db.AllPetTypes.RemoveRange(db.AllPetTypes);
                db.SaveChanges();
                db.Dispose();
            }
            // Load the PetTypes CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // PetTypeID,SpeciesName,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.PetTypes.CSV_FileName);
                var dt = new DataTable();
                LoadDataTable(CSVPath, ref dt);
                int rows = dt.Rows.Count;
                // Load all the data
                var db = new DAL.PetDB();
                for (int r = 0; r < rows; r++)
                {
                    var pt = new Models.PetType()
                    {
                        // PetTypeID,SpeciesName,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetDietCost,PetImage
                        PetTypeID = Int32.Parse(dt.Rows[r].ItemArray[0].ToString()),
                        SpeciesID = getSpeciesID(dt.Rows[r].ItemArray[Constants.PetTypes.SpeciesNamePos].ToString()),
                        TypeName = dt.Rows[r].ItemArray[Constants.PetTypes.TypeNamePos].ToString(),
                        PetSize = dt.Rows[r].ItemArray[Constants.PetTypes.PetSizePos].ToString(),
                        PetSolitary = dt.Rows[r].ItemArray[Constants.PetTypes.PetSolitaryPos].ToString(),
                        PetIndoors = dt.Rows[r].ItemArray[Constants.PetTypes.PetIndoorsPos].ToString(),
                        PetOutdoors = dt.Rows[r].ItemArray[Constants.PetTypes.PetOutdoorsPos].ToString(),
                        PetWalk = dt.Rows[r].ItemArray[Constants.PetTypes.PetWalkPos].ToString(),
                        PetDiet = dt.Rows[r].ItemArray[Constants.PetTypes.PetDietPos].ToString(),
                        PetDietCost = dt.Rows[r].ItemArray[Constants.PetTypes.PetDietCostPos].ToString(),
                        PetImage = dt.Rows[r].ItemArray[Constants.PetTypes.PetImagePos].ToString()
                    };
                    if (pt.SpeciesID != 0)
                    {
                        db.AllPetTypes.Add(pt);
                    }
                }
                db.SaveChanges();
                db.Dispose();
                dt.Dispose();
            }
        }
    }
}