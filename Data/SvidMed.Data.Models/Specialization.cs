using System.Collections.Generic;
using SvidMed.Data.Common.Models;

namespace SvidMed.Data.Models
{
    public class Specialization : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
