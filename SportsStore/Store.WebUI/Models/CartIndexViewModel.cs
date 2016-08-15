﻿using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WebUI.Models
{
    public class CartIndexViewModel
    {
        public CartIndexViewModel(ICart cart, string url)
        {
            this.Cart = cart;
            this.ReturnUrl = url;
        }
        public string ReturnUrl { get; set; }
        public ICart Cart { get; set; }
    }
}