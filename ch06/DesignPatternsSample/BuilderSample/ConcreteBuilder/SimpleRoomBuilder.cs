using DesignPatternsSample.BuilderSample.BuilderInterface;

namespace DesignPatternsSample.BuilderSample
{
    class SimpleRoomBuilder : IRoomBuilder
    {
        public Room Build()
        {
            return new Room("Simple")
                .WithBed(1);
        }
    }
}
