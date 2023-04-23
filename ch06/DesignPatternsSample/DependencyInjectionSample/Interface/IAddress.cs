using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.DependencyInjectionSample.Interface
{
    public interface IAddress
    {
        string City { get; set; }
        string ZipCode { get; set; }
        string Country { get; set; }
    }
}
