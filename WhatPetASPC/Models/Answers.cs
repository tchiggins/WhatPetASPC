namespace WhatPetASPC.Models
{
    public class Answers
    {
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public int QuestionID { get; set; }
        public virtual Questions Questions { get; set; }
    }
}