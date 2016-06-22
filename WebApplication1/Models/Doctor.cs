using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<Patient> Patients { get; set; }
    }
}