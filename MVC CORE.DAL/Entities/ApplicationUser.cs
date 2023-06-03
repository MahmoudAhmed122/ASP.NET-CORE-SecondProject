using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.DDL.Entities
{
    public class ApplicationUser:IdentityUser 
    {
        public bool IsAgree { get; set; }
    }
}
