using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FakeSource
{
    public class PurchaseMessage
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string? Location { get; set; }
        public decimal Cost { get; set; }
        
    }
}
