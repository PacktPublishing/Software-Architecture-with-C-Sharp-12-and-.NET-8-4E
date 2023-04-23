using System;

namespace DesignPatternsSample.SingletonSample
{

    public sealed class SingletonDemo
    {
        #region This is the Singleton definition
        private static SingletonDemo _instance;

        public static SingletonDemo Current => _instance ??= new SingletonDemo();
        
        #endregion

        public string Message { get; set; }

        public void Print()
        {
            Console.WriteLine(Message);
            Console.WriteLine("");
        }
    }
}
