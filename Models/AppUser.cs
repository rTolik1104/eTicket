using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Models
{
    public class AppUser:IdentityUser
    {
        public string ProfileImgUrl { get; set; }
        public DateTime MemberScince { get; set; }

    }
}
