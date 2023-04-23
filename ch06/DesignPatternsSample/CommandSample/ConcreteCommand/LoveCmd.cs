using DesignPatternsSample.CommandSample.CommandInterface;
using DesignPatternsSample.CommandSample.Receiver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.CommandSample.ConcreteCommand
{
    class LoveCmd : ICommand
    {
        private Package _package;
        public LoveCmd(Package package)
        {
            _package = package;
        }
        public void Execute()
        {
            _package.Love();
        }
        public void Undo()
        {
            _package.UndoLove();
        }
    }
}
