using auto.Database;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public int? OwnerId { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public string OwnerLabel { get; set; }

        public int? TypeId { get; set; }
        public List<SelectListItem> Types { get; set; }
        public string TypeLabel { get; set; }
    }

    public class CarIndexViewModel
    {
        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
