namespace WhatPetASPC.App_Start
{
    internal static class Constants
    {
        public static class PetClass
        {
            // PetClassID,ClassName
            public const string CSV_FileName = "~/Files/PetClass.csv";
            public const int PetClassIDPos = 0;
            public const int ClassNamePos = 1;
        }
        public static class Species
        {
            // SpeciesID,ClassName,SpeciesName
            public const string CSV_FileName = "~/Files/Species.csv";
            public const int SpeciesIDPos = 0;
            public const int ClassNamePos = 1;
            public const int SpeciesNamePos = 2;
        }
        public static class PetTypes
        {
            // PetTypeID SpeciesName TypeName Size    Outdoors PurchaseCost    MonthlyCost HoursNeeded Image
            public const string CSV_FileName = "~/Files/PetType.csv";
            public const int PetTypeIDPos       = 0;
            public const int SpeciesNamePos     = 1;
            public const int TypeNamePos        = 2;
            public const int SizePos            = 3;
            public const int OutdoorsPos        = 4;
            public const int PurchaseCostPos    = 5;
            public const int MonthlyCostPos     = 6;
            public const int HoursNeededPos     = 7;
            public const int ImagePos           = 8;
        }
        public static class SizeCategories
        {
            // SizeCategID,AverageSizeLowerBound,AverageSizeUpperBound,AverageMassLowerBound,AverageMassUpperBound
            public const string SC_FileName = "~/Files/SizeCategories.csv";
            public const int SizeCategIDPos             = 0;
            public const int AverageSizeLowerBoundPos   = 1;
            public const int AverageSizeUpperBoundPos   = 2;
            public const int AverageMassLowerBoundPos   = 3;
            public const int AverageMassUpperBoundPos   = 4;
        }
        public static class CostCategories
        {
            // CostID,CostBracket
            public const string CC_FileName = "~/Files/CostCategories.csv";
            public const int CostIDPos      = 0;
            public const int CostBracketPos = 1;
        }
    }
}