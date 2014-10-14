using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System;
using System.Globalization;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Mvc.Components.Html;
using BE.ModelosIII.Mvc.Models.Movie;
using BE.ModelosIII.Mvc.Models.Multiplex;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Mvc.Models.Screen;
using BE.ModelosIII.Mvc.Models.Screening;
using ScreeningModel = BE.ModelosIII.Mvc.Models.Movie.ScreeningModel;

namespace BE.ModelosIII.Mvc
{
    public static class AutoMappingsCreator
    {
        public static void CreateViewModelMappings()
        {
            Mapper.CreateMap<bool, string>().ConvertUsing<BoolToSiNoConverter>();
            Mapper.CreateMap<int?, string>().ConvertUsing<NullableTypeToStringConverter<int>>();
            Mapper.CreateMap<decimal?, string>().ConvertUsing(new NullableTypeToStringConverter<decimal>("n"));
            Mapper.CreateMap<DateTime?, string>().ConvertUsing(new NullableTypeToStringConverter<DateTime>("dd/MM/yyyy"));
            Mapper.CreateMap<IEnumerable<Genre>, string>().ConvertUsing(new EnumerableEnumToStringConverter<Genre>());
            Mapper.CreateMap<IEnumerable<Rating>, string>().ConvertUsing(new EnumerableTypeToStringConverter<Rating>());


            Mapper.CreateMap<Movie, MovieListModel>();
            Mapper.CreateMap<Movie, MovieModel>()
                .ForMember(dest => dest.Poster, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Poster) ? string.Empty : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + src.Poster))
                .ForMember(dest => dest.SmallPoster, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SmallPoster) ? string.Empty : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + src.SmallPoster))
                .ForMember(dest => dest.ScreeningOptions, opt => opt.Ignore());
            Mapper.CreateMap<Rating, RatingModel>();
            Mapper.CreateMap<Screening, ScreeningModel>()
                .ForMember(dest => dest.Screen, opt => opt.MapFrom(src => src.Screen.Name));
            Mapper.CreateMap<Genre, GenreModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToDescription()));
            Mapper.CreateMap<Multiplex, MultiplexModel>()
                .ForMember(dest => dest.Poster, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Poster) ? string.Empty : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + src.Poster));
            Mapper.CreateMap<Screen, ScreenModel>()
                .ForMember(dest => dest.MultiplexId, opt => opt.MapFrom(src => src.Multiplex.Id));
            Mapper.CreateMap<Row, RowModel>();
            Mapper.CreateMap<Seat, SeatModel>()
                .ForMember(dest => dest.Available, opt => opt.Ignore())
                .ForMember(dest => dest.Reserved, opt => opt.Ignore())
                .ForMember(dest => dest.Purchased, opt => opt.Ignore());
            Mapper.CreateMap<Screening, Models.Screening.ScreeningModel>()
                .ForMember(dest => dest.Screen, opt => opt.MapFrom(src => src.Screen.Name))
                .ForMember(dest => dest.Multiplex, opt => opt.MapFrom(src => src.Screen.Multiplex.Name))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartDate.ToString("HH:mm")))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Rows, opt => opt.ResolveUsing(ScreeningSeatAvailabilityResolver))
                .ForMember(dest => dest.Report, opt => opt.Ignore());
            Mapper.CreateMap<Reservation, Models.Reservation.ReservationModel>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Screening.Movie.Title))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.ReservationPaymentStatus.ToDescription()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ReservationStatus.ToDescription()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Time.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time.ToString("HH:mm")))
                .ForMember(dest => dest.ScreeningDate, opt => opt.MapFrom(src => src.Screening.StartDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.Screening.StartDate.ToString("HH:mm")))
                .ForMember(dest => dest.ScreeningEndTime, opt => opt.MapFrom(src => src.Screening.EndDate.ToString("HH:mm")))
                .ForMember(dest => dest.Multiplex, opt => opt.MapFrom(src => src.Screening.Screen.Multiplex.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ReservationPaymentStatus == ReservationPaymentStatus.Paid ? "Compra" : "Reserva"))
                .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => string.Join(", ", src.Seats)))
                .ForMember(dest => dest.MoviePoster, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Screening.Movie.SmallPoster) ? string.Empty : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + src.Screening.Movie.SmallPoster));

            Mapper.CreateMap<Promotion, Models.Promotion.PromotionModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active ? "Activa" : "Inactiva"))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToDescription()))
                .ForMember(dest => dest.Poster, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Poster) ? string.Empty : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + src.Poster))
                .ForMember(dest => dest.Days, opt => opt.MapFrom(src => string.Join(", ", src.Days.Select(d => d.ToLocalizedLabel(CultureInfo.CurrentCulture)))));
            Mapper.CreateMap<Promotion, Models.Promotion.PromotionOptionModel>()
                .ForMember(dest => dest.Total, opt => opt.Ignore())
                .ForMember(dest => dest.Poster, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Poster) ? string.Empty : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + src.Poster));

            Mapper.CreateMap<MostSoldHourReportModel, Infrastructure.ApplicationServices.Models.MostSoldHour.ReportDataItem>();
            Mapper.CreateMap<MostSoldMovieReportModel, Infrastructure.ApplicationServices.Models.MostSoldMovie.ReportDataItem>();


            Mapper.AssertConfigurationIsValid();
        }

        private static IList<RowModel> ScreeningSeatAvailabilityResolver(Screening src)
        {
            var result = new List<RowModel>();
            foreach (var row in src.Screen.Rows)
            {
                var rowModel = Mapper.Map<RowModel>(row);
                foreach (var seat in row.Seats)
                {
                    rowModel.Seats.Single(s => s.Id == seat.Id).Available = seat.AvailableForScreening(src);
                    rowModel.Seats.Single(s => s.Id == seat.Id).Reserved = seat.ReservedForScreening(src);
                    rowModel.Seats.Single(s => s.Id == seat.Id).Purchased = seat.PurchasedForScreening(src);
                }

                result.Add(rowModel);
            }
            return result;
        }
    }
    
    public class EnumToStringConverter<TSource> : TypeConverter<TSource, string>
        where TSource : struct
    {
        protected override string ConvertCore(TSource source)
        {
            return ((Enum)(object)source).ToDescription();
        }
    }

    public class NullableEnumToStringConverter<TSource> : TypeConverter<TSource?, string>
        where TSource : struct
    {
        protected override string ConvertCore(TSource? source)
        {
            if (!source.HasValue)
                return string.Empty;
            return ((Enum)(object)source.Value).ToDescription();
        }
    }

    public class NullableTypeToStringConverter<TSource> : TypeConverter<TSource?, string>
        where TSource : struct, IFormattable
    {
        private readonly string _formatString;

        public NullableTypeToStringConverter()
        {
            _formatString = string.Empty;
        }

        public NullableTypeToStringConverter(string formatString)
        {
            _formatString = formatString;
        }

        protected override string ConvertCore(TSource? source)
        {
            return source.HasValue ? source.Value.ToString(_formatString, CultureInfo.CurrentUICulture) : string.Empty;
        }
    }

    public class EnumerableEnumToStringConverter<TSource> : TypeConverter<IEnumerable<TSource>, string>
        where TSource : struct
    {
        private readonly string _separator;

        public EnumerableEnumToStringConverter()
        {
            _separator = ", ";
        }

        public EnumerableEnumToStringConverter(string separator)
        {
            _separator = separator;
        }

        protected override string ConvertCore(IEnumerable<TSource> source)
        {
            return string.Join(_separator, source.Select(s => ((Enum)(object)s).ToDescription()));
        }
    }

    public class EnumerableTypeToStringConverter<TSource> : TypeConverter<IEnumerable<TSource>, string>
        where TSource : class
    {
        private readonly string _separator;

        public EnumerableTypeToStringConverter()
        {
            _separator = ", ";
        }

        public EnumerableTypeToStringConverter(string separator)
        {
            _separator = separator;
        }

        protected override string ConvertCore(IEnumerable<TSource> source)
        {
            return string.Join(_separator, source.Select(s => s.ToString()));
        }
    }

    public class BoolToSiNoConverter : TypeConverter<bool, string>
    {
        protected override string ConvertCore(bool source)
        {
            return source ? "Si" : "No";
        }
    }

}
