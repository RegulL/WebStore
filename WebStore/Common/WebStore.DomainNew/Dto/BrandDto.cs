﻿using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Dto
{
    public class BrandDto : INamedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
