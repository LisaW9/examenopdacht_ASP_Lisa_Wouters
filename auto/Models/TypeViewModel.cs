using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Models
{
    public class TypeViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Brand { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }

        public List<CarViewModel> Cars { get; set; }
    }

    public class TypeIndexViewModel
    {
        public IEnumerable<TypeViewModel> Types { get; set; }
    }
}
