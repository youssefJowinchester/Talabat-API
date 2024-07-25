using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.productSpec
{
    public class productWithFilterationForCountSpecification : BaseSpecifications<Product>
    {
        public productWithFilterationForCountSpecification(productSpecParams productSpec) :
            base(p =>
            (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search))
            &&
            (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId.Value)
            &&
            (!productSpec.CategoryId.HasValue || p.CategoryId == productSpec.CategoryId.Value)
                )
        {

        }
    }
}