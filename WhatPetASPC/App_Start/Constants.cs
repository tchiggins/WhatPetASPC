namespace WhatPetASPC.App_Start
{
    static class Constants
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
            // SpeciesID,ClassName,;
            public const string CSV_FileName = "~/Files/Species.csv";

            public const int SpeciesIDPos = 0;
            public const int ClassNamePos = 1;
            public const int SpeciesNamePos = 2;
        }

        public static class PetTypes
        {
            // PetTypeID,SpeciesName,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage
            public const string CSV_FileName = "~/Files/PetType.csv";

            public const int PetTypeIDPos   = 0;
            public const int SpeciesNamePos = 1;
            public const int TypeNamePos    = 2;
            public const int PetSizePos     = 3;
            public const int PetSolitaryPos = 4;
            public const int PetIndoorsPos  = 5;
            public const int PetOutdoorsPos = 6;
            public const int PetWalkPos     = 7;
            public const int PetDietPos     = 8;
            public const int PetImagePos    = 9;
        }
    }
}