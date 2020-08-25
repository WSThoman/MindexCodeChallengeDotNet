using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    public class Compensation
    {
        // Properties
        // NOTE: By convention, properties in C# begin with upper-case letters
        //

        // Add the [Key] attribute to specify the primary key
        //
        [Key]
        public string EmployeeId { get; set; } = string.Empty;

        public Employee Employee { get; set; } = null;

        public double Salary { get; set; } = 0.0;
		
        public DateTime EffectiveDate { get; set; } = DateTime.MinValue;
    }
}
