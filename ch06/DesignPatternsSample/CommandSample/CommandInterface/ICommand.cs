using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.CommandSample.CommandInterface
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
