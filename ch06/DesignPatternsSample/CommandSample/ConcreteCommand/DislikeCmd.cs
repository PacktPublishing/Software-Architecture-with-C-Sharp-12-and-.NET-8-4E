using DesignPatternsSample.CommandSample.CommandInterface;
using DesignPatternsSample.CommandSample.Receiver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.CommandSample.ConcreteCommand
{
    class DislikeCmd : ICommand
    {
        private Package _package;
        public DislikeCmd(Package package)
        {
            _package = package;
        }
        public void Execute()
        {
            _package.Dislike();
        }
        public void Undo()
        {
            _package.UndoDislike();
        }
    }
}
