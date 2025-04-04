using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Menu
{
    public class ProductCategoryMenu : CategoryMenu
    {
        public ProductCategoryMenu(IMenuRenderer renderer) : base(renderer)
        {

        }
        public override string Show()
        {
            return "<div class='category-menu'>" + renderer.Render(sp) + "</div>";
        }
    }
}