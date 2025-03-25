using QLCoffee.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Command
{
    public class RemoveFromCartCommand : ICommand
    {
        private CartService _cartService;
        private int _productId;

        public RemoveFromCartCommand(CartService cartService, int productId)
        {
            _cartService = cartService;
            _productId = productId;
        }

        public void Execute()
        {
            _cartService.RemoveFromCart(_productId);
        }

        public void Undo()
        {
            _cartService.AddToCart(_productId);
        }   
    }
}