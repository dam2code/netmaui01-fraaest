using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MovieCatalog.ViewModels;

public class MovieListViewModel : ObservableObject
{
    public ObservableCollection<MovieViewModel> Movies { get; set; }

    private MovieViewModel _selectedMovie;
    public MovieViewModel SelectedMovie
    {
        get => _selectedMovie;
        set => SetProperty(ref _selectedMovie, value);
    }

    public MovieListViewModel() =>
        Movies = new ObservableCollection<MovieViewModel>();

    public async Task RefreshMovies()
    {
        IEnumerable<Models.Movie> moviesData = await Models.MoviesDatabase.GetMovies();

        foreach (Models.Movie movie in moviesData)
            Movies.Add(new MovieViewModel(movie));
    }

    public void DeleteMovie(MovieViewModel movie) =>
        Movies.Remove(movie);
}
