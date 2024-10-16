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
        // ������������ ��������� ���
        this.Title = _pageTitle; // ������������� ��������� �������� ��� ���������
    }
}