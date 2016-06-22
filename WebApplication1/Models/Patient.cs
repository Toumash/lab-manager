using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;

namespace WebApplication1.Models
{
    public class Patient
    {
        public int ID { get; set; }

        [Display(Name = "Imię")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [PESEL]
        [Required]
        [Index(IsUnique = true)]
        [StringLength(11)]
        [Display(Name = "Pesel")]
        public string PESEL { get; set; }

        [Display(Name = "Badania")]
        public virtual List<Exam> Exams { get; set; }
    }

    [NotMapped]
    public class PatientCreateViewModel
    {
        [Display(Name = "Imię")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [PESEL]
        [Required]
        [StringLength(11)]
        [Display(Name = "Pesel")]
        public string PESEL { get; set; }

        public bool AlreadyExists { get; set; } = false;
        public Patient ActualPatient { get; set; }
    }

    [NotMapped]
    public class PatientEditViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Imię")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }
    }

    public class PESELAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is string))
            {
                throw new InvalidDataException("PESEL MUST be a string, check your code");
            }
            var data = (string) value;
            data = data.Trim();

            if (data.Length != 11)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            int checksum = 0;
            // the algorithm to check if the pesel is valid
            checksum += data[0]*1;
            checksum += data[1]*3;
            checksum += data[2]*7;
            checksum += data[3]*9;
            checksum += data[4]*1;
            checksum += data[5]*3;
            checksum += data[6]*7;
            checksum += data[7]*9;
            checksum += data[8]*1;
            checksum += data[9]*3;
            checksum += data[10]*1;

            if (checksum%10 == 0)
            {
                return null;
            }
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}