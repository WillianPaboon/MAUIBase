using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Mediator.Contracts;
using MediatR;
using SharedPresentation.Pages;

namespace BaseMAUI.Pages.Home
{
    public partial class MainHomePageViewModel: BaseViewModel
    {
        private IMediator _mediator;

        [ObservableProperty]
        private string buyModuleData = "Ensure that the Buy module is loaded";


        public MainHomePageViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RelayCommand]
        private async void GetBuyDataImplementation(object obj)
        {
            var request = new GetBuyDataRequest { };

            try
            {
                BuyModuleData = await _mediator.Send(request).ConfigureAwait(true); 
            }
            catch (Exception ex)
            {
                if (Application.Current?.MainPage == null)
                    return;

                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "Okay").ConfigureAwait(true);

            }
        }


    }
}
