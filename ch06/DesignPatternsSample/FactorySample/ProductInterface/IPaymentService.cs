using DesignPatternsSample.FactorySample.Enums;

namespace DesignPatternsSample.FactorySample.ProductInterface
{
    public interface IPaymentService 
    {
        /// <summary>
        /// E-mail of the person who will be charged
        /// </summary>
        string EmailToCharge { get; set; }
        /// <summary>
        /// Money that will be charged
        /// </summary>
        decimal MoneyToCharge { get; set; }
        
        /// <summary>
        /// Credit Card or Debit Card
        /// </summary>
        EnumChargingOptions OptionToCharge { get; set; }

        /// <summary>
        /// Method responsible for process the charging asked
        /// </summary>
        /// <returns></returns>
        bool ProcessCharging();

    }
}
