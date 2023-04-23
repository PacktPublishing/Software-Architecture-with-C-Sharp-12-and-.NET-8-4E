using DesignPatternsSample.ProxySample.RealSubject;
using DesignPatternsSample.ProxySample.Subject;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.ProxySample.Proxy
{
    class ProxyRoomPicture : IRoomPicture
    {
        private RoomPicture roomPicture;
        
        public Guid Id { get => roomPicture.Id; }
        public string FileName { get => roomPicture.FileName;  }
        public string[] Tags { get => roomPicture.Tags;  }

        public string PictureData
        {
            get
            {
                if (!roomPicture.Loaded)
                    roomPicture.Load();
                return roomPicture.PictureData;
            }
        }

        public ProxyRoomPicture()
        {
            roomPicture = new RoomPicture();
            roomPicture.Get();
        }

    }
}
