using System;
using System.Collections.Generic;
using System.Text;

namespace Models_Employee
{
   public class Employee_Data
    {
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Contactno { get; set; }
        public byte? IsBlocked { get; set; }
        public int Role { get; set; }
        public DateTime Dob { get; set; }
        public DateTime Doj { get; set; }
        public byte IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int Id { get; set; }
        public string Gender { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
    }
}
