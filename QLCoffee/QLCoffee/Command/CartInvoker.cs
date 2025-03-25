using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Input;

namespace QLCoffee.Commands
{
    //// ➡ Invoker này sẽ lưu lại các lệnh để có thể Undo khi cần.
    //public class CartInvoker
    //{
    //    private Stack<ICommand> _commandHistory = new Stack<ICommand>();

    //    public void ExecuteCommand(ICommand command)
    //    {
    //        command.Execute();
    //        _commandHistory.Push(command);
    //    }

    //    public void Undo()
    //    {
    //        if (_commandHistory.Count > 0)
    //        {
    //            ICommand lastCommand = _commandHistory.Pop();
    //            lastCommand.Undo();
    //        }
    //    }
    //}
}