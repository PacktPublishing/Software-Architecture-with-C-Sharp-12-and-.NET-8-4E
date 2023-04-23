using DesignPatternsSample.BuilderSample;
using DesignPatternsSample.CommandSample.ConcreteCommand;
using DesignPatternsSample.CommandSample.Invoker;
using DesignPatternsSample.CommandSample.Receiver;
using DesignPatternsSample.DependencyInjectionSample.Concrete;
using DesignPatternsSample.FactorySample.ConcreteCreator;
using DesignPatternsSample.FactorySample.Enums;
using DesignPatternsSample.ProxySample.Proxy;
using DesignPatternsSample.ProxySample.Subject;
using DesignPatternsSample.SingletonSample;
using System;
using System.Threading;

namespace DesignPatternsSample
{
    class Program
    {
        static void Main()
        {
            #region Builder Sample
            Console.WriteLine("Builder Sample");

            var simpleRoom = new SimpleRoomBuilder().Build();
            simpleRoom.Describe();
            
            var familyRoom = new FamilyRoomBuilder().Build();
            familyRoom.Describe();
            #endregion

            #region Factory Sample

            ProcessCharging(PaymentServiceFactory.ServicesAvailable.Brazilian,
                "gabriel@sample.com", 178.90m, EnumChargingOptions.CreditCard);

            ProcessCharging(PaymentServiceFactory.ServicesAvailable.Italian,
                "francesco@sample.com", 188.70m, EnumChargingOptions.DebitCard);

            #endregion

            #region Singleton Sample
            Console.WriteLine("Singleton Sample");
            SingletonDemo.Current.Message = "This text will be printed by " +
                "the singleton.  Be careful with concurrency.";
            SingletonDemo.Current.Print();
            #endregion
            
            #region Singleton Configuration Sample
            Console.WriteLine("Singleton Configuration Sample");
            for (int i = 0; i< 20; i++)
            {
                Console.WriteLine($"Random Number Parameter: {Configuration.Current.RandomNumber}. Last Time Loaded {Configuration.Current.LastTimeLoaded}");
                Thread.Sleep(1000);
            }
            #endregion
            
            #region Proxy Sample
            Console.WriteLine("Proxy Sample");
            ExecuteProxySample(new ProxyRoomPicture());
            #endregion

            #region Command Sample
            Console.WriteLine("");
            Console.WriteLine("Command Sample");
            var package = new Package("Shopping in New York");
            var likeCommand = new LikeCmd(package);
            var dislikeCommand = new DislikeCmd(package);
            var loveCommand = new LoveCmd(package);
            var commandInvoker = new CommandInvoker();
            var keepAsking = true;

            while (keepAsking)
            {
                Console.WriteLine($"Your oppinion about {package.Name}:");
                Console.WriteLine("1 - Like");
                Console.WriteLine("2 - Dislike");
                Console.WriteLine("3 - Love");
                Console.WriteLine("4 - Undo last command");
                Console.WriteLine("Any other key - Exit");
                var key = Console.ReadKey();
                Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        commandInvoker.Command = likeCommand;
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        commandInvoker.Command = dislikeCommand;
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        commandInvoker.Command = loveCommand;
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        if (commandInvoker.Command != null)
                        {
                            commandInvoker.Undo();
                            commandInvoker.Command = null;
                        }
                        else
                            Console.WriteLine("There is no Command to Undo!");
                        break;
                    default:
                        keepAsking = false;
                        break;
                }
                if ((keepAsking) && (commandInvoker.Command != null))
                    commandInvoker.Invoke();
            }



            #endregion
    
            #region Dependency Injection Sample
            var userAddress = new UserAddress { City = "São Paulo", Country = "Brazil", ZipCode = "01001-001" };
            var destinationAddress = new UserAddress { City = "Rio de Janeiro", Country = "Brazil", ZipCode = "22460-050" };
            var distanceCalculator = new DistanceCalculator(userAddress, destinationAddress);
            distanceCalculator.Calculate();
            #endregion

            Console.ReadKey();
        }

        private static void ExecuteProxySample(IRoomPicture roomPicture)
        {
            Console.WriteLine($"Picture Id: {roomPicture.Id}");
            Console.WriteLine($"Picture FileName: {roomPicture.FileName}");
            Console.WriteLine($"Tags: {string.Join(";", roomPicture.Tags)}");
            Console.WriteLine($"1st call: Picture Data");
            Console.WriteLine($"Image: {roomPicture.PictureData}");
            Console.WriteLine($"2nd call: Picture Data");
            Console.WriteLine($"Image: {roomPicture.PictureData}");
        }

        private static void ProcessCharging(PaymentServiceFactory.ServicesAvailable serviceToCharge, 
            string emailToCharge, decimal moneyToCharge, EnumChargingOptions optionToCharge)
        {
            PaymentServiceFactory factory = new PaymentServiceFactory();
            var service = factory.Create(serviceToCharge);
            service.EmailToCharge = emailToCharge;
            service.MoneyToCharge = moneyToCharge;
            service.OptionToCharge = optionToCharge;
            service.ProcessCharging();
        }
    }
}
