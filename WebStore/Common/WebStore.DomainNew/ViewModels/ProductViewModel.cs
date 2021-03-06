﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.ViewModels
{
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int Order { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string BrandName { get; set; }
    }
}
