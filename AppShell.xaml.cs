//using Listifyr.ProgramLogic.Pages.NestedPages;

namespace Listifyr
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            pageTitle.Text = Current.CurrentPage.Title;

            base.OnNavigated(args);
        }
    }
}
