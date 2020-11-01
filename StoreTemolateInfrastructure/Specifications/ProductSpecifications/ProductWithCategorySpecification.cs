namespace Infrastructure.Specifications.ProductSpecifications
{
    public class ProductWithCategorySpecification : ProductSpecification
    {
        public ProductWithCategorySpecification(string productName)
        : base(product => product.Name.ToLower().Contains(productName.ToLower()))
        {
            AddInclude(product => product.Category);
        }
        public ProductWithCategorySpecification(int productId)
        : base(product => product.Id == productId)
        {
            AddInclude(product => product.Category);
        }
        public ProductWithCategorySpecification()
        : base(null)
        {
            AddInclude(product => product.Category);
        }
    }
}
