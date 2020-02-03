using System.ComponentModel.DataAnnotations;
namespace WhatPetASPC.Models
{
    public class CostCategories
    {
        [Key]
        public int CostID { get; set; }
        public string CostBracket { get; set; }
    }
}