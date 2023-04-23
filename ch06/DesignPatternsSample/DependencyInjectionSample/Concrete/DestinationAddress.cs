using DesignPatternsSample.DependencyInjectionSample.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.DependencyInjectionSample.Concrete
{
    class DestinationAddress : IAddress
    {
        public string ZipCode { get;  set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
