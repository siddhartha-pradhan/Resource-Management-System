using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Core.Models
{
    public class Staff : IdentityUser
    {
        public string Name { get; set; }
    }
}
