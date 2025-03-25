using QLCoffee.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Command
{

    public class AddToCartCommand : ICommand
    {
        private CartService _cartService;
        private int _productId;

        public AddToCartCommand(CartService cartService, int productId)
        {
            _cartService = cartService;
            _productId = productId;
        }

        public void Execute() => _cartService.AddToCart(_productId);

        public void Undo()
        {
            _cartService.RemoveFromCart(_productId);
        }
    }
}
