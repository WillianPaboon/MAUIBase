using BaseMAUI.ViewModels;

namespace BaseMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}
