namespace Listifyr.ProgramLogic.Pages.NestedPages;
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
        this.Title = _pageTitle; // Використовуємо збережене значення для заголовка
    }
}