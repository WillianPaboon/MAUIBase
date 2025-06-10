using MediatR;

namespace Domain.Mediator.Contracts
{
    public class GetBuyDataRequest: IRequest<string>
    {
        /// <summary>
        /// if we need to pass any parameters, we can add them here.
        /// </summary>
        public int BuyId { get; set; }
    }
}
