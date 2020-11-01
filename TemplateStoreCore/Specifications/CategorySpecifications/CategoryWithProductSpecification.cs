using StoreTemplateCore.Entities;
using StoreTemplateCore.Specifications.Base;

namespace StoreTemplateCore.Specifications.CategorySpecifications
{
    public class CategoryWithProductSpecification : CategorySpecification
    {
        public CategoryWithProductSpecification(int id)
            : base(category => category.Id == id)
        {
            AddInclude(category => category.Products);
        }

        public CategoryWithProductSpecification(string name)
            : base(category => category.Name.ToLower().Contains(name.ToLower()))
        {
            AddInclude(category => category.Products);            
        }
    }
}
