﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class CartService//Để tương tác với giỏ hàng trong session

    {
        private readonly HttpSessionStateBase session;
        public CartService(HttpSessionStateBase session)
        {
            this.session = session;

        }
        public Cart GetCart()
        {
            var cart = (Cart)session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                session["Cart"] = cart;
            }
            return cart;
        }
        public void ClearCart()
        {
            session["Cart"] = null;
        }

        internal void AddToCart(int productId)
        {
            throw new NotImplementedException();
        }

        internal void RemoveFromCart(int productId)
        {
            throw new NotImplementedException();
        }
    }
}