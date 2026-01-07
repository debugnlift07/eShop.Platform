
using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> products);
    public class GetProductQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {

            var product = await session.Query<Product>().ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
            return new GetProductResult(product);
        }
    }
}
