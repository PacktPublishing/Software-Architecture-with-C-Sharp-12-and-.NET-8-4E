using DesignPatternsSample.FactorySample.Enums;
using DesignPatternsSample.FactorySample.ProductInterface;
using System;

namespace DesignPatternsSample.FactorySample.ConcreteProduct
{
    class BrazilianPaymentService : IPaymentService
    {
        public string EmailToCharge { get ; set ; }
        public decimal MoneyToCharge { get ; set ; }
        public EnumChargingOptions OptionToCharge { get ; set ; }

        public bool ProcessCharging()
        {
            Console.WriteLine("This payment will be processed by Brazilian Service.");
            Console.WriteLine($"Person: {EmailToCharge}");
            Console.WriteLine($"Price: R$ {MoneyToCharge:0.00}");
            Console.WriteLine($"Option: {OptionToCharge}");
            Console.WriteLine("");
            return true;
        }
    }
}
