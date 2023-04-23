using DesignPatternsSample.CommandSample.CommandInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.CommandSample.Invoker
{
    class CommandInvoker
    {   
        public ICommand Command { get; set; }
        public void Invoke()
        {
            Command.Execute();
        }

        internal void Undo()
        {
            Command.Undo();
        }
    }
}
