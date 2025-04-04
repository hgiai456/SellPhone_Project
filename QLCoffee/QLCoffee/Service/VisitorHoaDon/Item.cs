using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.VisitorHoaDon
{
	public interface Item
	{
		double Priceitem { get; }
		double Discountitem { get; }
		void Accept(IVisitor ivisitor);
	}
}