using DesignPatternsSample.CommandSample.CommandInterface;
using DesignPatternsSample.CommandSample.Receiver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.CommandSample.ConcreteCommand
{
    class LikeCmd : ICommand
    {
        private Package _package;
        public LikeCmd(Package package)
        {
            _package = package;
        }
        public void Execute()
        {
            _package.Like();
        }

        public void Undo()
        {
            _package.UndoLike();
        }
    }
}
