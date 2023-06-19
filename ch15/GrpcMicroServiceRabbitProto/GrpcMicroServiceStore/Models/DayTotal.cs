using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcMicroServiceStore.Models
{
    public class DayTotal
    {
        public DateTimeOffset Id { get; set; }
        public decimal Total { get; set; }
    }
}
