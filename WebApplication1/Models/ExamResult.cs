using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ExamResult
    {
        public string Details { get; set; }
        public bool Complete { get; set; } = false;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ReadyTime { get; set; } = new DateTime(1970, 1, 1);
    }
    public class ExamResultAddViewModel
    {
        public int ExamId { get; set; }
        public string Details { get; set; }
        public bool Complete { get; set; } = false;
    }
}