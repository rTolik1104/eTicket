using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Enums
{
    public enum MovieCategory
    {
        [Display(Name = "Sealect a category")]
        Action =1,
        Comedy,
        Drama,
        Documentary,
        Cartoon,
        Horror
    }
}
