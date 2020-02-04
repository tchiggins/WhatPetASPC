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
        public static class Questions
        {
            // Clear the Questions Table
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                Log.Information("Attempting to clear Questions table...");
                try
                {
                    db.AllQuestions.RemoveRange(db.AllQuestions);
                    Log.Information("Questions table cleared successfully");
                    Log.Information("Attempting to save changes to Questions table...");
                    try
                    {
                        db.SaveChanges();
                        Log.Information("Successfully saved changes to Questions table");
                    }
                    catch (Exception e)
                    {
                        Log.Error("Failed to save changes to Questions table");
                        Log.Error(e.Message);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Failed to clear Questions table");
                    Log.Error(e.Message);
                }
                db.Dispose();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Error prevention")]
        public static class PetClass
        {
            // Clear the PetClass Table
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                Log.Information("Attempting to clear PetClass table...");
                try
                {
                    db.AllPetClasses.RemoveRange(db.AllPetClasses);
                    Log.Information("PetClass table cleared successfully");
                    Log.Information("Attempting to save changes to PetClass table...");
                    try
                    {
                        db.SaveChanges();
                        Log.Information("Successfully saved changes to PetClass table");
                    }
                    catch (Exception e)
                    {
                        Log.Error("Failed to save changes to PetClass table");
                        Log.Error(e.Message);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Failed to clear PetClass table");
                    Log.Error(e.Message);
                }
                db.Dispose();
            }
            // Load the PetClass CSV file
            public static void CSVImport()
            {
                bool LoadFailed = false;
                // Upload and save the file
                // PetClassID,ClassName
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.PetClass.CSV_FileName);
                var dt = new DataTable();
                LoadDataTable(CSVPath, ref dt);
                int rows = dt.Rows.Count;
                // Load all the data
                var db = new DAL.PetDB();
                string InfoLog;
                string FClasses = null;
                for (int r = 0; r < rows; r++)
                {
                    var pc = new Models.PetClass
                    {
                        PetClassID = Int32.Parse(dt.Rows[r].ItemArray[Constants.PetClass.PetClassIDPos].ToString()),
                        ClassName = dt.Rows[r].ItemArray[Constants.PetClass.ClassNamePos].ToString()
                    };
                    InfoLog = "Attempting to load ";
                    InfoLog += pc.ClassName;
                    InfoLog += " class into PetClass table...";
                    Log.Information(InfoLog);
                    try
                    {
                        db.AllPetClasses.Add(pc);
                        LoadFailed = false;
                    }
                    catch (Exception e)
                    {
                        LoadFailed = true;
                        if (r >= 1)
                        {
                            FClasses += ", ";
                        }
                        FClasses += pc.ClassName;
                        InfoLog = "Failed to load ";
                        InfoLog += pc.ClassName;
                        InfoLog += " class into PetClass table";
                        Log.Error(InfoLog);
                        Log.Error(e.Message);
                    }
                }
                Log.Information("Attempting to save changes to PetClass table...");
                try
                {
                    if (LoadFailed == true)
                    {
                        Log.Error("Failed to save changes to PetClass table");
                        InfoLog = "Could not load classes ";
                        InfoLog += FClasses;
                        Log.Error(InfoLog);
                    }
                    else
                    {
                        db.SaveChanges();
                        Log.Information("Successfully saved changes to PetClass table");
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Failed to save changes to PetClass table");
                    Log.Error(e.Message);
                }
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
                string InfoLog = null;
                IQueryable<int> MyPetClass = null;
                var db = new DAL.PetDB();
                Log.Information("Attempting to get PetClassID foreign key...");
                try
                {
                    MyPetClass = from PetClass in db.AllPetClasses
                                 where PetClass.ClassName == ClassName
                                 select PetClass.PetClassID;
                    Log.Information("Successfully got PetClassID foreign key");
                }
                catch (Exception e)
                {
                    InfoLog = "Failed to get PetClassID foreign table key for class ";
                    InfoLog += ClassName;
                    Log.Error(InfoLog);
                    Log.Error(e.Message);
                }
                Log.Information("Attempting to return PetClassID foreign key...");
                try
                {
                    return MyPetClass.FirstOrDefault();
                }
                catch (Exception e)
                {
                    Log.Error("Failed to return PetClassID foreign key");
                    Log.Error(e.Message);
                    return 0;
                }
            }
            // Clear the Species Table
            [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "Warning reason unclear")]
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                try
                {
                    Log.Information("Attempting to clear Species table...");
                    db.AllSpecies.RemoveRange(db.AllSpecies);
                    Log.Information("Species table cleared successfully");
                    Log.Information("Attempting to save changes to Species table...");
                    try
                    {
                        db.SaveChanges();
                        Log.Information("Successfully saved changes to Species table");
                    }
                    catch (Exception e)
                    {
                        Log.Error("Failed to save changes to Species table");
                        Log.Error(e.Message);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Failed to clear Species table");
                    Log.Error(e.Message);
                }
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
                try
                {
                    Log.Information("Attempting to clear Cost Categories table...");
                    db.AllCostCategories.RemoveRange(db.AllCostCategories);
                    Log.Information("Attempting to save changes to Cost Categories table...");
                    try
                    {
                        db.SaveChanges();
                        Log.Information("Successfully saved changes to Cost Categories table");
                    }
                    catch (Exception e)
                    {
                        Log.Error("Failed to save changes to Cost Categories table");
                        Log.Error(e.Message);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Failed to clear Cost Categories table");
                    Log.Error(e.Message);
                }
                db.Dispose();
            }
            // Load the CostCategories CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // CostID, CostCategory
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
                        CostID = int.Parse(dt.Rows[r].ItemArray[0].ToString()),
                        CostBracket = dt.Rows[r].ItemArray[Constants.CostCategories.CostBracketPos].ToString()
                    };
                    if (cc.CostID != 0)
                    {
                        db.AllCostCategories.Add(cc);
                    }

                }
                Log.Information("Attempting to save changes to CostCategories table...");
                try
                {
                    db.SaveChanges();
                    Log.Information("Successfully saved changes to CostCategories table");
                }
                catch (Exception e)
                {
                    Log.Error("Failed to save changes to CostCategories table");
                    Log.Error(e.Message);
                }
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
                IQueryable<int> MySpecies = null;
                var db = new DAL.PetDB();
                Log.Information("Attempting to get SpeciesID foreign key...");
                try
                {
                    MySpecies = from Species in db.AllSpecies
                                where Species.SpeciesName == SpeciesName
                                select Species.SpeciesID;
                    Log.Information("Successfully got SpeciesID foreign key");
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
                Log.Information("Attempting to return SpeciesID foreign key...");
                try
                {
                    return MySpecies.FirstOrDefault();
                }
                catch (Exception e)
                {
                    Log.Error("Failed to return SpeciesID foreign key");
                    Log.Error(e.Message);
                    return 0;
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Error prevention")]
            public static int getCostID(string CostBracket)
            {
                IQueryable<int> MyCost = null;
                var db = new DAL.PetDB();
                string InfoLog;
                Log.Information("Attempting to get CostID foreign key...");
                try
                {
                    MyCost = from CostCategories in db.AllCostCategories
                             where CostCategories.CostBracket == CostBracket
                             select CostCategories.CostID;
                    Log.Information("Successfully got CostID foreign key");
                }
                catch (Exception e)
                {
                    InfoLog = "Failed to get CostID foreign key for CostBracket ";
                    InfoLog += CostBracket;
                    Log.Error(InfoLog);
                    Log.Error(e.Message);
                }
                Log.Information("Attempting to return CostID foreign key...");
                try
                {
                    return MyCost.FirstOrDefault();
                }
                catch (Exception e)
                {
                    Log.Error("Failed to return CostID foreign key");
                    Log.Error(e.Message);
                    return 0;
                }
            }
            // Clear the PetType Table
            [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "Warning reason unclear")]
            public static void ClearTable()
            {
                var db = new DAL.PetDB();
                Log.Information("Attempting to clear PetType table...");
                try
                {
                    db.AllPetTypes.RemoveRange(db.AllPetTypes);
                    Log.Information("Successfully cleared PetType table");
                    Log.Information("Attempting to save changes to PetType table...");
                    try
                    {
                        db.SaveChanges();
                        Log.Information("Successfully saved changes to PetType table");
                    }
                    catch (Exception e)
                    {
                        Log.Error("Failed to save changes to PetType table");
                        Log.Error(e.Message);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Failed to clear PetType table");
                    Log.Error(e.Message);
                }
                db.Dispose();
            }
            // Load the PetTypes CSV file
            public static void CSVImport()
            {
                // Upload and save the file
                // PetTypeID,SpeciesName,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,CostID,PetImage
                string CSVPath = HttpContext.Current.Server.MapPath(Constants.PetTypes.CSV_FileName);
                var dt = new DataTable();
                LoadDataTable(CSVPath, ref dt);
                int rows = dt.Rows.Count;
                // Load all the data
                var db = new DAL.PetDB();
                for (int r = 0; r < rows; r++)
                {
                    // Read the Pet Type ID
                    var ReadPetTypeIDString = dt.Rows[r].ItemArray[0].ToString();
                    var ReadPetTypeID = 0;
                    if (ReadPetTypeIDString != null)
                    {
                        ReadPetTypeID = int.Parse(ReadPetTypeIDString);
                    }

                    // Read the Species ID
                    var ReadSpeciesIDString = dt.Rows[r].ItemArray[Constants.PetTypes.SpeciesNamePos].ToString();
                    var ReadSpeciesID = 0;
                    if (ReadSpeciesIDString != null)
                    {
                        ReadSpeciesID = getSpeciesID(ReadSpeciesIDString);
                    }

                    // Read the Cost ID
                    var ReadCostIDString = dt.Rows[r].ItemArray[Constants.PetTypes.CostIDPos].ToString();
                    var ReadCostID = 0;
                    if (ReadCostIDString != null)
                    {
                        ReadCostID = getCostID(ReadCostIDString);
                    }

                    var pt = new Models.PetType()
                    {
                        // PetTypeID,SpeciesName,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetDietCost,PetImage
                        PetTypeID = ReadPetTypeID,
                        SpeciesID = ReadSpeciesID,
                        TypeName = dt.Rows[r].ItemArray[Constants.PetTypes.TypeNamePos].ToString(),
                        PetSize = dt.Rows[r].ItemArray[Constants.PetTypes.PetSizePos].ToString(),
                        PetSolitary = dt.Rows[r].ItemArray[Constants.PetTypes.PetSolitaryPos].ToString(),
                        PetIndoors = dt.Rows[r].ItemArray[Constants.PetTypes.PetIndoorsPos].ToString(),
                        PetOutdoors = dt.Rows[r].ItemArray[Constants.PetTypes.PetOutdoorsPos].ToString(),
                        PetWalk = dt.Rows[r].ItemArray[Constants.PetTypes.PetWalkPos].ToString(),
                        PetDiet = dt.Rows[r].ItemArray[Constants.PetTypes.PetDietPos].ToString(),
                        CostID = ReadCostID,
                        PetImage = dt.Rows[r].ItemArray[Constants.PetTypes.PetImagePos].ToString(),
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