using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}