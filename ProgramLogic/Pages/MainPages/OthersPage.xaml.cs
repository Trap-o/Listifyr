using CommunityToolkit.Maui.Views;
using Listifyr.ProgramLogic.Pages.PopUps;

namespace Listifyr.View;

public partial class OthersPage : ContentPage
{
    private const string projectPageURL = "https://github.com/Trap-o/Listifyr";
    private const string instructionURL = "https://github.com/Trap-o/Listifyr/wiki";

    public OthersPage()
	{
		InitializeComponent();
	}

    private async void ProjectPage_Clicked(object sender, EventArgs e)
    {
		try
		{
			Uri uri = new(projectPageURL);
			await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message.ToString());
		}
    }

    private async void InstructionPage_Clicked(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new(instructionURL);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
    }

    private void HelpButton_Clicked(object sender, EventArgs e)
    {
        this.ShowPopup(new ContactsPopup());
    }
}