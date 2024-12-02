using Infrastructure.Models;
namespace Infrastructure.Interface;

public interface IMovieService
{
    List<Movie> getmovies();
    Movie getMovieById(int id);
    void addMovie(Movie movie);
    void updateMovie(Movie movie);
    void deletemovie(int id);
}