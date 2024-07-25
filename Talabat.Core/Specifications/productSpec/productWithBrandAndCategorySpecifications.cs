using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.productSpec
{
    public class productWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public productWithBrandAndCategorySpecifications(productSpecParams productSpec)
            : base(p =>
            (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId.Value)
            &&
            (!productSpec.CategoryId.HasValue || p.CategoryId == productSpec.CategoryId.Value))
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);

            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedsc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

            ApplyPagination(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);

        }

        public productWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
