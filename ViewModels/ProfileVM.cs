using eTicket_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.ViewModels
{
    public class ProfileVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfileImgUrl { get; set; }
        public DateTime MemberScine { get; set; }
        public List<Order> Orders { get; set; }
        public bool isAdmin { get; set; }
    }
}
