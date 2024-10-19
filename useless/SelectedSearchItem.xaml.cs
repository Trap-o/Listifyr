//using Listifyr.ItemTypes;

//namespace Listifyr.ProgramLogic.Pages.NestedPages;

//public partial class SelectedSearchItem : ContentPage
//{
//    public SelectedSearchItem() => InitializeComponent();
//    public SelectedSearchItem(MediaItems mediaItem)
//	{
//		InitializeComponent();

//        Title = mediaItem.Name;
//        MovieTitleLabel.Text = mediaItem.Name ?? "Немає назви";
//        ReleaseDateLabel.Text = "Рік випуску: " + mediaItem.Release_Date;
//        OverviewLabel.Text = mediaItem.Description;
//        MoviePoster.Source = "https://image.tmdb.org/t/p/w500" + mediaItem.Poster;
//    }
//}