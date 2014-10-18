using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System;
using System.Globalization;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Mvc.Components.Html;
using BE.ModelosIII.Mvc.Models.Scenario;

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


            Mapper.CreateMap<Scenario, ScenarioListModel>();

            Mapper.CreateMap<Scenario, ScenarioModel>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
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
