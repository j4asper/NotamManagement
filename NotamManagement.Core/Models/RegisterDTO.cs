using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotamManagement.Core.Models
{
    public class RegisterDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int OrganizationId { get; set; }

    }
}
