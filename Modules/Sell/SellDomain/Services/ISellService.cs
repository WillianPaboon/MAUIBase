using Domain.Mediator.Contracts;
using MediatR;
using System.Threading.Tasks;

namespace SellDomain.Services
{
    /// <summary>
    /// Interface for selling services.
    /// </summary>
    public interface ISellService
    {
        /// <summary>
        /// Sells a product.
        /// </summary>
        Task<string> SellProduct();
    }

    /// <summary>
    /// Service for selling products.
    /// </summary>
    public class SellService : ISellService
    {
        private IMediator _mediator;

        public SellService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sells a product.
        /// </summary>
        public async Task<string> SellProduct()
        {
            var request = new GetBuyDataRequest {};
            string result = await _mediator.Send(request).ConfigureAwait(true);
            return result;
        }
    }
}
