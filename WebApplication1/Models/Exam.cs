using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Exam
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public Patient Patient { get; set; }

        public int PatientId { get; set; }

        [Display(Name = "Zlecono")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Issued { get; set; }

        [Display(Name = "Szczegóły")]
        public string Details { get; set; }

        [Display(Name = "Wyniki")]
        public virtual ExamResult Result { get; set; }
    }

    public class ExamCreateViewModel
    {
        [Required]
        [Display(Name = "Nazwa badania")]
        public string Name { get; set; }

        [PESEL]
        [Required]
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Display(Name = "Szczegóły")]
        public string Details { get; set; }

        public bool IsExistingPesel { get; set; } = true;
    }
}