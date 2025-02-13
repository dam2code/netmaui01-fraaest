namespace MovieCatalog.Views;

public partial class MovieDetailPage : ContentPage
{
	public MovieDetailPage(ViewModels.MovieViewModel movie)
    {
        BindingContext = App.MainViewModel.SelectedMovie;
        InitializeComponent();
	}
}