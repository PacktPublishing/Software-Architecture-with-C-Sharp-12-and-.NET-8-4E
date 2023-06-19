using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GrpcMicroServiceStore.Models
{
    public class QueueItem
    { 
        public long Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public DateTimeOffset PurchaseTime { get; set; }
        public DateTimeOffset ExtractionTime { get; set; }
        [MaxLength(128), Required]
        public string? Location { get; set; }
        public decimal Cost { get; set; }
        public Guid MessageId { get; set; }
    }
}
