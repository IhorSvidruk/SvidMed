using SvidMed.Data.Common.Models;

namespace SvidMed.Data.Models
{
    public class Town : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int? ZipCode { get; set; }
    }
}
