using auto.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Color { get; set; }

        [Required(AllowEmptyStrings = false)]
        public DateTime PurchaseDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LicensePlate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Owner Owner { get; set; }

        [Required(AllowEmptyStrings = false)]
        public CarType Type { get; set; }
    }
}
