using System.ComponentModel.DataAnnotations;
namespace WhatPetASPC.Models
{
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
    }
}