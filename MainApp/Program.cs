using System;
using Infrastructure.Services;
using Infrastructure.Models;



using System;
using Infrastructure.Services;
using Infrastructure.Models;

MovieService movieService = new MovieService();
bool running = true;

while (running)
{
    Console.WriteLine("1. List all movies");
    Console.WriteLine("2. Get movie by ID");
    Console.WriteLine("3. Add new movie");
    Console.WriteLine("4. Update movie");
    Console.WriteLine("5. Delete movie");
    Console.WriteLine("6. Exit");


    Console.Write("Choose a variation: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            var movies = movieService.getmovies();
            Console.WriteLine("\n--- List of Movies ---");
            foreach (var movie in movies)
            {
                Console.WriteLine($"ID: {movie.Id}, Title: {movie.Title}, Author: {movie.Author}, Price: {movie.Price:C}");
            }
            break;

        case "2":
            Console.Write("Enter movie ID: ");
            int id = int.Parse(Console.ReadLine());
            var movieById = movieService.getMovieById(id);
            if (movieById != null)
            {
                Console.WriteLine($"ID: {movieById.Id}, Title: {movieById.Title}, Author: {movieById.Author}, Description: {movieById.Description}, Price: {movieById.Price:C}");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
            break;

        case "3":
            Console.Write("Enter movie title: ");
            string title = Console.ReadLine();
            Console.Write("Enter movie author: ");
            string author = Console.ReadLine();
            Console.Write("Enter movie description: ");
            string description = Console.ReadLine();
            Console.Write("Enter movie price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            var newMovie = new Movie
            {
                Title = title,
                Author = author,
                Description = description,
                Price = price
            };

            movieService.addMovie(newMovie);
            Console.WriteLine("Movie added successfully.");
            break;


        case "4":
            Console.Write("Enter the ID of the movie to update: ");
            int updateId = int.Parse(Console.ReadLine());

            var movieToUpdate = movieService.getMovieById(updateId);
            if (movieToUpdate != null)
            {
                Console.Write("Enter new title (leave blank to keep current): ");
                string newTitle = Console.ReadLine();
                Console.Write("Enter new author (leave blank to keep current): ");
                string newAuthor = Console.ReadLine();
                Console.Write("Enter new description (leave blank to keep current): ");
                string newDescription = Console.ReadLine();
                Console.Write("Enter new price (leave blank to keep current): ");
                string newPrice = Console.ReadLine();

                movieToUpdate.Title = string.IsNullOrWhiteSpace(newTitle) ? movieToUpdate.Title : newTitle;
                movieToUpdate.Author = string.IsNullOrWhiteSpace(newAuthor) ? movieToUpdate.Author : newAuthor;
                movieToUpdate.Description = string.IsNullOrWhiteSpace(newDescription) ? movieToUpdate.Description : newDescription;
                movieToUpdate.Price = string.IsNullOrWhiteSpace(newPrice) ? movieToUpdate.Price : decimal.Parse(newPrice);

                movieService.updateMovie(movieToUpdate);
                Console.WriteLine("Movie updated successfully.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
            break;

        case "5":
            Console.Write("Enter the ID of the movie to delete: ");
            int deleteId = int.Parse(Console.ReadLine());
            var movieToDelete = movieService.getMovieById(deleteId);

            if (movieToDelete != null)
            {
                movieService.deletemovie(deleteId);
                Console.WriteLine("Movie deleted successfully.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
            break;

        case "6":
            running = false;
            Console.WriteLine("Exiting the program. Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}



    