using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.DomainNew.ViewModels
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public BrandViewModel()
        {

        }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
        public int? Sum { get; set; }
    }
}
