using System;
using System.Globalization;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Account;
using BE.ModelosIII.Tasks.Commands.Movie;
using BE.ModelosIII.Tasks.Commands.Multiplex;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using BE.ModelosIII.Tasks.Commands.Promotion;
using BE.ModelosIII.Tasks.Commands.Screening;
using BE.ModelosIII.Tasks.Commands.Utility;
using SharpArch.Domain.DomainModel;
using Row = BE.ModelosIII.Domain.Row;
using Seat = BE.ModelosIII.Domain.Seat;

namespace BE.ModelosIII.Tasks
{
    public static class AutoMappingsCreator
    {
        public static void CreateCommandMappings()
        {
            Mapper.CreateMap<Entity, int>().ConvertUsing(entity => entity.Id);

            Mapper.CreateMap<CreateMovieCommand, Movie>()
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.Screenings, opt => opt.Ignore());
            Mapper.CreateMap<EditMovieCommand, Movie>()
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.Screenings, opt => opt.Ignore());
            Mapper.CreateMap<Movie, EditMovieCommand>()
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(orig => orig.Rating))
                .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(orig => orig.Genres))
                .ForMember(dest => dest.NewPoster, opt => opt.Ignore())
                .ForMember(dest => dest.NewSmallPoster, opt => opt.Ignore());

            Mapper.CreateMap<CreateMultiplexCommand, Multiplex>()
                .ForMember(dest => dest.Screens, opt => opt.Ignore());
            Mapper.CreateMap<EditMultiplexCommand, Multiplex>()
                .ForMember(dest => dest.Screens, opt => opt.Ignore());
            Mapper.CreateMap<Multiplex, EditMultiplexCommand>()
                .ForMember(dest => dest.NewPoster, opt => opt.Ignore());

            Mapper.CreateMap<Row, RowModel>()
                .ForMember(dest => dest.ScreenId, opt => opt.MapFrom(src => src.Screen.Id));
            Mapper.CreateMap<RowModel, Row>()
                .ForMember(dest => dest.Screen, opt => opt.Ignore());
            Mapper.CreateMap<Seat, SeatModel>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.Row.Id));
            Mapper.CreateMap<SeatModel, Seat>()
                .ForMember(dest => dest.Reservations, opt => opt.Ignore())
                .ForMember(dest => dest.Row, opt => opt.Ignore());

            Mapper.CreateMap<CreateScreenCommand, Screen>()
                .ForMember(dest => dest.Rows, opt => opt.Ignore())
                .ForMember(dest => dest.Multiplex, opt => opt.Ignore())
                .ForMember(dest => dest.Screenings, opt => opt.Ignore());
            Mapper.CreateMap<EditScreenCommand, Screen>()
                .ForMember(dest => dest.Rows, opt => opt.Ignore())
                .ForMember(dest => dest.Multiplex, opt => opt.Ignore())
                .ForMember(dest => dest.Screenings, opt => opt.Ignore());
            Mapper.CreateMap<Screen, EditScreenCommand>()
                .ForMember(dest => dest.LayoutFile, opt => opt.Ignore());

            Mapper.CreateMap<EditScreeningCommand, Screening>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.Date + DateTime.ParseExact(src.StartTime, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay))
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.Screen, opt => opt.Ignore());
            Mapper.CreateMap<Screening, EditScreeningCommand>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.Date))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartDate.ToString("HH:mm")))
                .ForMember(dest => dest.ScreenId, opt => opt.MapFrom(src => src.Screen))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Movie))
                .ForMember(dest => dest.MultiplexId, opt => opt.MapFrom(src => src.Screen.Multiplex));


            Mapper.CreateMap<CreatePromotionCommand, Promotion>();
            Mapper.CreateMap<EditPromotionCommand, Promotion>();
            Mapper.CreateMap<Promotion, EditPromotionCommand>()
                .ForMember(dest => dest.NewPoster, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
