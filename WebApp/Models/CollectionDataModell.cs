﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CollectionDataModell
    {
        public PersonalDetails personalDetails { get; set; }
        public List<ShoppingCartVM> shoppingCartVMs { get; set; }
    }
}
