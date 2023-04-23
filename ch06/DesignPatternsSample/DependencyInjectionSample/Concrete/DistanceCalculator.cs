using DesignPatternsSample.DependencyInjectionSample.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.DependencyInjectionSample.Concrete
{
    public class DistanceCalculator
    {
        private IAddress _userAddress;
        private IAddress _destinationAddress;

        public DistanceCalculator(IAddress userAddress, IAddress destinationAddress)
        {
            _userAddress = userAddress;
            _destinationAddress = destinationAddress;
        }

        public void Calculate()
        {
            Console.WriteLine($"The distance between {_userAddress.City} and {_destinationAddress.City} is 000");
        }
    }
}
