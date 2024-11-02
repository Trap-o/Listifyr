using CommunityToolkit.Maui.Views;

namespace Listifyr.ProgramLogic.Pages.PopUps;

public partial class ContactsPopup : Popup
{
    public ContactsPopup() => InitializeComponent();
    private void OnOKButtonClicked(object sender, EventArgs e) => Close();
}