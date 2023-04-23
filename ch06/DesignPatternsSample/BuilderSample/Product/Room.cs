using System;

namespace DesignPatternsSample.BuilderSample
{
    public class Room
    {
        private readonly string _name;
        private bool wiFiFreeOfCharge;

        private int numberOfBeds;
        private bool balconyAvailable;

        public Room(string name)
        {
            _name = name;
        }
        public Room WithBalcony()
        {
            balconyAvailable = true;
            return this;
        }

        public Room WithBed(int numberOfBeds)
        {
            this.numberOfBeds = numberOfBeds;
            return this;
        }

        public Room WithWiFi()
        {
            wiFiFreeOfCharge = true;
            return this;
        }

        public void Describe()
        {
            var wifi = wiFiFreeOfCharge ? "is" : "is not";
            var balcony = balconyAvailable ? "is" : "is not";
            Console.WriteLine($"{_name} room");
            Console.WriteLine($"Number of bed(s): {numberOfBeds}");
            Console.WriteLine($"There {balcony} a balcony.");
            Console.WriteLine($"This room {wifi} wi-fi Free");
            Console.WriteLine("");
        }

    }
}