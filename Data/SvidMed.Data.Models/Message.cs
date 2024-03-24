using SvidMed.Data.Common.Models;

namespace SvidMed.Data.Models
{
    public class Message : BaseDeletableModel<int>
    {
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public string Content { get; set; }
    }
}
