using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Menu
{
	public abstract class CategoryMenu
	{
        protected IMenuRenderer renderer;
        public List<string> items;
        public List<LOAISANPHAM> sp = new QuanLyQuanCoffeeEntities().LOAISANPHAMs.ToList();

        public CategoryMenu(IMenuRenderer renderer)
        {
            this.renderer = renderer;
            items = new List<string> { "Danh sách loại sản phẩm", "Create New" };
        }
        public abstract string Show();
    }
}