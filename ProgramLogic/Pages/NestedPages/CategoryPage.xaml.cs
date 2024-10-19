//using Listifyr.ProgramLogic.Pages.NestedPages;

namespace Listifyr.View;
public partial class CategoryPage : ContentPage
{
    private string? _pageTitle;
    public CategoryPage() => InitializeComponent();
    public CategoryPage(string title)
    {
        InitializeComponent();
        _pageTitle = title;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // ������������ ��������� ���
        this.Title = _pageTitle;
        //this.LabelForEmptyCategory.Text = $"������� {ChangeLabelText()}, ���������� ������ ����� �������!";
    }

    private string ChangeLabelText()
    {
        string emptyElementName = "";
        switch (Title)
        {
            case "Գ����":
                emptyElementName = "��� ������ �����";
                break;
            case "������":
                emptyElementName = "��� ������ �����";
                break;
            case "�����":
                emptyElementName = "���� ����� �����";
                break;
            case "������":
                emptyElementName = "��� ������ �����";
                break;
            case "�����":
                emptyElementName = "���� ����� �����";
                break;
            case "������":
                emptyElementName = "��� ����� ������";
                break;
            case "����":
                emptyElementName = "���� ����� ���";
                break;
            case "����":
                emptyElementName = "��� ����� ����";
                break;
        }
        return emptyElementName;
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        ////string newCatalogueName = await DisplayPromptAsync("����� ������", "������ ����� ������");
        ////var addedCatalogue = new Catalogues { Name = newCatalogueName };
        ////await App.Database.AddItemAsync<Catalogues>(addedCatalogue);
        //CataloguesViewModel cataloguesViewModel = new CataloguesViewModel();
        //cataloguesViewModel.AddCatalogue();
        //OnAppearing();
        //await DisplayAlert("��������", $"��������� �� ������ ���������", "OK");

        string data = Title;
        //await Navigation.PushAsync(new SearchPage(data));
    }
}