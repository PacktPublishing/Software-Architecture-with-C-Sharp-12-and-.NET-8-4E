using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.SingletonSample
{

    public sealed class Configuration
    {
        #region Singleton Implementation
        private static Configuration _instance;

        public static Configuration Current
        {
            get
            {
                if (_instance == null)
                    _instance = new Configuration();
                else
                {
                    _instance.CheckCache();
                }
                return _instance;
            }
        }


        #endregion


        public DateTime LastTimeLoaded { get; private set; }

        /// <summary>
        /// Emulation of a Configuration Parameter
        /// </summary>
        public int RandomNumber { get; private set; }

        private Configuration()
        {
            LoadConfiguration();
        }

        private void CheckCache()
        {
            // Since last time the configuration was loaded was 5 seconds ago, it will reload the configuration
            // On the other hand, the Configuration will be the same
            if (DateTime.Now.Subtract(LastTimeLoaded).TotalSeconds > 5)
                LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            Console.WriteLine("Loading the configuration");
            var random = new Random(DateTime.Now.Second);
            RandomNumber = random.Next(1024);
            LastTimeLoaded = DateTime.Now;
        }




    }
}
