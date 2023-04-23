using DesignPatternsSample.ProxySample.Subject;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.ProxySample.RealSubject
{
    class RoomPicture : IRoomPicture
    {
        public bool Loaded { get; private set; }

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string[] Tags { get; set; }
        public string PictureData { get; private set; }
        
        public void Get()
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
                FileName = "picture.jpg";
                Tags = new string[] { "incredible picture", "must seeing!" };
            }
        }

        public void Load()
        {
            PictureData = "[DATA FROM PICTURE]";
            Loaded = true;
            Console.WriteLine("Now the picture is loaded!");
        }
    }
}