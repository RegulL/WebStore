using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table(name:"Categories")]
    public class Category : NamedEntity, IOrderedEntity
    {
        public int? ParentId { get; set; }

        public int Order { get; set; }

        [ForeignKey(name:"ParentId")]
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
