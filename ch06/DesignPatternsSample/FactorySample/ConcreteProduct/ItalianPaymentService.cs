using DesignPatternsSample.FactorySample.Enums;
using DesignPatternsSample.FactorySample.ProductInterface;
using System;

namespace DesignPatternsSample.FactorySample.ConcreteProduct
{
    class ItalianPaymentService : IPaymentService
    {
        public string EmailToCharge { get ; set ; }
        public decimal MoneyToCharge { get ; set ; }
        public EnumChargingOptions OptionToCharge { get ; set ; }

        public bool ProcessCharging()
        {
            Console.WriteLine("This payment will be processed by Italian Service.");
            Console.WriteLine($"Person: {EmailToCharge}");
            Console.WriteLine($"Price: $ {MoneyToCharge:0.00}");
            Console.WriteLine($"Option: {OptionToCharge}");
            Console.WriteLine("");
            return true;
        }
    }
}
