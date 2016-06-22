using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ExamResult
    {
        [Display(Name = "Szczegóły")]
        public string Details { get; set; }
        [Display(Name="Gotowe")]
        public bool Complete { get; set; } = false;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ReadyTime { get; set; } = new DateTime(1970, 1, 1);
    }
    public class ExamResultAddViewModel
    {
        public int ExamId { get; set; }
        [Display(Name = "Szczegóły")]
        public string Details { get; set; }
        [Display(Name="Gotowe")]
        public bool Complete { get; set; } = false;
    }
}