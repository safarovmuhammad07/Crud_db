using Npgsql;
using Infrastructure.Interface;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    List<Movie> movies = new List<Movie>();
    string connectionString = @"Server=127.0.0.1;Port=5432;Database=movies_db;User Id=postgres;Password=832111;";


    public List<Movie> getmovies()
    {
        List<Movie> lib = new List<Movie>();

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM movies";
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Movie movie = new Movie()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Description = reader.GetString(3),
                            Price = reader.GetDecimal(4)
                        };
                        lib.Add(movie);
                    }
                }
            }
        }
        return lib;
    }


    public Movie getMovieById(int id)
    {
        Movie movie = null;

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            using (NpgsqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM movies WHERE id = @id;";
                command.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        movie = new Movie()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Description = reader.GetString(3),
                            Price = reader.GetDecimal(4)
                        };
                    }
                }
            }
        }
        return movie;
    }


    public void addMovie(Movie movie)
{
    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();

        using (NpgsqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @"
                INSERT INTO movies (title, author, description, price)
                VALUES (@title, @author, @description, @price);";

            command.Parameters.AddWithValue("@title", movie.Title);
            command.Parameters.AddWithValue("@description", movie.Description);
            command.Parameters.AddWithValue("@author", movie.Author);
            command.Parameters.AddWithValue("@price", movie.Price);

            command.ExecuteNonQuery();
        }
    }
}


    public void updateMovie(Movie movie)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
                UPDATE movies 
                SET 
                    title = @title, 
                    author = @author, 
                    description = @description, 
                    price = @price
                WHERE id = @id;";

                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@author", movie.Author);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@price", movie.Price);
                command.Parameters.AddWithValue("@id", movie.Id);

                command.ExecuteNonQuery();
            }
        }
    }

    public void deletemovie(int idd)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = $"delete from movies where id = {idd};";
            command.ExecuteNonQuery();
        }
    }

}