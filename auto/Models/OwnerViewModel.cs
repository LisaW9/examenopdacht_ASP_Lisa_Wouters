using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Models
{
    public class OwnerViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        public List<CarViewModel> Cars { get; set; }
    }

    public class OwnerIndexViewModel
    {
        public IEnumerable<OwnerViewModel> Owners { get; set; }
    }
}
