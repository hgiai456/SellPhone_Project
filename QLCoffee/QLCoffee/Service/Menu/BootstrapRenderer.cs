using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QLCoffee.Service.Menu
{
    public class BootstrapRenderer : IMenuRenderer
    {
        public ActionResult Render(List<LOAISANPHAM> items)
        {
            var viewData = new ViewDataDictionary
            {
                Model = items
            };

            return new PartialViewResult
            {
                ViewName = "~/Views/Shared/View1.cshtml",
                ViewData = viewData
            };
        }
    }
}
