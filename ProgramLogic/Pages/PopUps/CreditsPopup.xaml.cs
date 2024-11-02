using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace Listifyr.ProgramLogic.Pages.PopUps;

public partial class CreditsPopup : Popup
{
    public CreditsPopup()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public ICommand OpenApiPage => new Command<string>(LaunchBrowser);

    private async void LaunchBrowser(string url)
    {
        await Browser.OpenAsync(url);
    }

    private void OnOKButtonClicked(object sender, EventArgs e) => Close();
}