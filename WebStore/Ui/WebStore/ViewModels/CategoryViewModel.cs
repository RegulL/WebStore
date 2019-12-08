using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class CategoryViewModel : INamedEntity, IOrderedEntity
    {
        public CategoryViewModel()
        {
            ChildCategories = new List<CategoryViewModel>();
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public int Order { get; set; }

        public List<CategoryViewModel> ChildCategories { get; set; }

        public CategoryViewModel ParentCategory { get; set; }
    }
}
