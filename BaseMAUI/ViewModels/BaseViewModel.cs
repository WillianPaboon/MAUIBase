using TinyMvvm;

namespace BaseMAUI.ViewModels
{
    public abstract class BaseViewModel:TinyViewModel
    {
        public BaseViewModel(IServiceProvider serviceProvider)
        {
            //Initialize Global dependences
        }

        public BaseViewModel()
        {
            //Get dependences without serviceProvider
            //GetRequiredService<IServiceProvider>();
        }


        private T GetRequiredService<T>()
        {
            return Application.Current.Handler.MauiContext.Services.GetRequiredService<T>();
        }
    }
}
