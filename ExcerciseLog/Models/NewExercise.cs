using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ExcerciseLog.Models
{
    public class NewExercise
    {
        public int Exerciseid { get; set; }
        public int Logid { get; set; }
        public string ExerciseName { get; set; }
        public string date { get; set; }
        public int set { get; set; }
        public int rep { get; set; }
        public byte[] Exercise_img { get; set; }
        public string img_type { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }

    }
}