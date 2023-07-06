using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.ActorsMovies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownValues()
        {
            var response = new NewMovieDropdownVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.ActorName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.ProducerName).ToListAsync()
            };

            return response;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add movie actors
            foreach(var actorId in data.ActorIds)
            {
                var newActorMovie = new ActorMovie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };

                await _context.ActorsMovies.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
        }
    }
}
