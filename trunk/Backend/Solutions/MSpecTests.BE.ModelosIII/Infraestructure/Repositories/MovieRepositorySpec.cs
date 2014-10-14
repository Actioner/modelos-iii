using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using Fasterflect;
using Machine.Specifications;
using Machine.Specifications.AutoMocking.Rhino;

namespace MSpecTests.BE.ModelosIII.Infraestructure.Repositories
{
    public class context_for_movie_repository : Specification<IMovieRepository>
    {
        protected static Multiplex multiplex_one;
        protected static Multiplex multiplex_two;
        protected static Multiplex multiplex_three;
        protected static Screen screen_one_for_multiplex_one;
        protected static Screen screen_two_for_multiplex_one;
        protected static Screen screen_one_for_multiplex_two;
        protected static Screen screen_one_for_multiplex_three;
        protected static Movie movie_with_screening_for_multiplex_one;
        protected static Movie movie_with_screening_for_multiplex_one_and_two;
        protected static Movie movie_with_multiple_screening_for_multiplex_two;

        Establish context = () =>
        {
            screen_one_for_multiplex_one = new Screen();
            screen_one_for_multiplex_one.SetPropertyValue("Id", 1);
            screen_two_for_multiplex_one = new Screen();
            screen_two_for_multiplex_one.SetPropertyValue("Id", 2);
            screen_one_for_multiplex_two = new Screen();
            screen_one_for_multiplex_two.SetPropertyValue("Id", 3);
            screen_one_for_multiplex_three = new Screen();
            screen_one_for_multiplex_three.SetPropertyValue("Id", 4);

            movie_with_screening_for_multiplex_one = new Movie();
            movie_with_screening_for_multiplex_one.SetPropertyValue("Id", 1);
            
            multiplex_one = new Multiplex();
            multiplex_one.SetPropertyValue("Id", 1);
            multiplex_two = new Multiplex();
            multiplex_two.SetPropertyValue("Id", 2);
            multiplex_three = new Multiplex();
            multiplex_three.SetPropertyValue("Id", 3);
        };
    }


    [Subject(typeof(IMovieRepository))]
    public class When_find_by_multiplex_with_screening : context_for_movie_repository
    {
        private static IList<Movie> result;

        Because of = () => result = subject.FindAllWithScreeningAt(multiplex_one, DateTime.Now, DateTime.Now.AddDays(7));

        It should_return_movie = () => result.ShouldContainOnly(new List<Movie>
                                                              {
                                                                  movie_with_screening_for_multiplex_one,
                                                                  movie_with_screening_for_multiplex_one_and_two,
                                                              });
    }
}
