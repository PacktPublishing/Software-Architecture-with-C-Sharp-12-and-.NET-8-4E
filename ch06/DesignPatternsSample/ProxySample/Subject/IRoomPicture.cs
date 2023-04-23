using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.ProxySample.Subject
{
    public interface IRoomPicture
    {
        Guid Id { get; }
        string FileName { get; }

        string[] Tags { get;  }

        string PictureData { get; }
    }
}
