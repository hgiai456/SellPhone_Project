using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Service.Menu
{
    public class MaterialRenderer : IMenuRenderer
    {
       
        public ActionResult Render(List<LOAISANPHAM> items)
        {
            var viewData = new ViewDataDictionary
            {
                Model = items
            };

            return new PartialViewResult
            {
                ViewName = "~/Views/Shared/View.cshtml",
                ViewData = viewData
            };
        }
    }
}