using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure;
using BE.ModelosIII.Tasks.Commands.Movie;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Movie
{
    public abstract class MovieHandler<T> : ICommandHandler<T>
        where T : MovieCommand
    {
        protected IMovieRepository MovieRepository;
        protected IRepository<Rating> RatingRepository;
        protected IRepository<Genre> GenreRepository;
        protected IMappingEngine MappingEngine;

        protected MovieHandler(
            IMovieRepository movieRepository, 
            IRepository<Rating> ratingRepository,
            IRepository<Genre> genreRepository,
            IMappingEngine mappingEngine)
        {
            this.MovieRepository = movieRepository;
            this.RatingRepository = ratingRepository;
            this.GenreRepository = genreRepository;
            this.MappingEngine = mappingEngine;
        }

        public virtual void Handle(T command)
        {
            var movie = MapCommandToMovie(command);

            if (!string.IsNullOrWhiteSpace(command.NewPoster.NewFile))
            {
                string sourcePath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.TempUploadFolder);
                string destPath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.PosterUploadFolder);
                string fileName = Path.GetFileName(command.NewPoster.NewFile);
                File.Move(sourcePath + @"\" + fileName, destPath + @"\" + fileName);

                movie.Poster = "/" + BackendSettings.PosterUploadFolder + "/" + fileName;
                TryDelete(sourcePath + @"\" + fileName);
            }

            if (!string.IsNullOrWhiteSpace(command.NewSmallPoster.NewFile))
            {
                string sourcePath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.TempUploadFolder);
                string destPath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.PosterUploadFolder);
                string fileName = Path.GetFileName(command.NewSmallPoster.NewFile);
                File.Move(sourcePath + @"\" + fileName, destPath + @"\" + fileName);

                movie.SmallPoster = "/" + BackendSettings.PosterUploadFolder + "/" + fileName;
                TryDelete(sourcePath + @"\" + fileName);
            }

            movie.Rating = RatingRepository.Get(command.RatingId);
            movie.Genres.Clear();
            foreach (int genreId in command.GenreIds)
            {
                movie.Genres.Add((Genre) genreId);
            }

            var result = MovieRepository.SaveOrUpdate(movie);
            
            command.Id = result.Id;
        }

        private bool TryDelete(string file)
        {
            try
            {
                File.Delete(file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected abstract Domain.Movie MapCommandToMovie(T command);
    }
}
