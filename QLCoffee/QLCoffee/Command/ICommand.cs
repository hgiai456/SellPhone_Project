using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}