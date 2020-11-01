namespace Infrastructure.Specifications.CategorySpecifications
{
    public class CategoryWithProductsSpecification : CategorySpecification
    {
        public CategoryWithProductsSpecification(int id)
            : base(category => category.Id == id)
        {
            AddInclude(category => category.Products);
        }

        public CategoryWithProductsSpecification(string name)
            : base(category => category.Name.ToLower().Contains(name.ToLower()))
        {
            AddInclude(category => category.Products);            
        }
    }
}
