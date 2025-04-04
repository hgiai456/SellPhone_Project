using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QLCoffee.Service.Menu
{
    public interface IMenuRenderer
    {
        ActionResult Render(List<LOAISANPHAM> items);
    }
}
