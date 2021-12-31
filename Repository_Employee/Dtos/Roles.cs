using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository_Employee.Dtos
{
    public partial class Roles
    {
        public Roles()
        {
            User = new HashSet<Employees>();
        }

        public string Rolename { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Employees> User { get; set; }
    }
}
