using BuyDomain.Services;
using Domain.Mediator.Contracts;
using MediatR;

namespace BuyDomain.Mediator.Handlers
{
    public class GetBuyDataHandler : IRequestHandler<GetBuyDataRequest, string>
    {
        private IBuyService _buyService;

        public GetBuyDataHandler(IBuyService buyService)
        {
            _buyService = buyService ?? throw new ArgumentNullException(nameof(buyService), "BuyService cannot be null");
        }
        public Task<string> Handle(GetBuyDataRequest request, CancellationToken cancellationToken)
        {
            string importantData = _buyService.GetSampleData();
            
            return Task.FromResult(importantData);
        }
    }
}
