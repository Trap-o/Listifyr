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
        // Встановлюємо заголовок тут
        this.Title = _pageTitle;
        //this.LabelForEmptyCategory.Text = $"Додайте {ChangeLabelText()}, натиснувши кнопку внизу сторінки!";
    }

    private string ChangeLabelText()
    {
        string emptyElementName = "";
        switch (Title)
        {
            case "Фільми":
                emptyElementName = "свій перший фільм";
                break;
            case "Серіали":
                emptyElementName = "свій перший серіал";
                break;
            case "Манга":
                emptyElementName = "свою першу мангу";
                break;
            case "Комікси":
                emptyElementName = "свій перший комікс";
                break;
            case "Книги":
                emptyElementName = "свою першу книгу";
                break;
            case "Ранобе":
                emptyElementName = "своє перше ранобе";
                break;
            case "Ігри":
                emptyElementName = "свою першу гру";
                break;
            case "Аніме":
                emptyElementName = "своє перше аніме";
                break;
        }
        return emptyElementName;
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        ////string newCatalogueName = await DisplayPromptAsync("Новий список", "Введіть назву списку");
        ////var addedCatalogue = new Catalogues { Name = newCatalogueName };
        ////await App.Database.AddItemAsync<Catalogues>(addedCatalogue);
        //CataloguesViewModel cataloguesViewModel = new CataloguesViewModel();
        //cataloguesViewModel.AddCatalogue();
        //OnAppearing();
        //await DisplayAlert("Перемога", $"Натиснуто на кнопку додавання", "OK");

        string data = Title;
        //await Navigation.PushAsync(new SearchPage(data));
    }
}