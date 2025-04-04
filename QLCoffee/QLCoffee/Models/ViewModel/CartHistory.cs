using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class CartHistory
    {
        private Stack<ProductMemento> _history = new Stack<ProductMemento>();

        public void Save(ProductMemento memento)
        {
            _history.Push(memento);
        }

        public ProductMemento Undo()
        {
            return _history.Count > 0 ? _history.Pop() : null;
        }
    }
}