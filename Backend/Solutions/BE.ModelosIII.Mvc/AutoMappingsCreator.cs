﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System;
using System.Globalization;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Mvc.Components.Html;
using BE.ModelosIII.Mvc.Models.Scenario;
using BE.ModelosIII.Mvc.Models.Run;
using BE.ModelosIII.Mvc.Models.BinItem;
using BE.ModelosIII.Mvc.Models.Bin;
using BE.ModelosIII.Mvc.Models.Population;
using BE.ModelosIII.Mvc.Models.Generation;
using BE.ModelosIII.Mvc.Models.Item;
using BE.ModelosIII.Mvc.Models.Report;

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
                .ForMember(dest => dest.Configuration, opt => opt.Ignore())
                .AfterMap((s, sm) => {
                    sm.Configuration.CrossoverProbability = s.CrossoverProbability * 100;
                    sm.Configuration.MutationProbability = s.MutationProbability * 100;
                    sm.Configuration.PopulationSize = s.PopulationSize;
                    sm.Configuration.StopCriterion = s.StopCriterion;
                    sm.Configuration.StopDepth = s.StopDepth;
                    sm.Configuration.Report = s.Report;
                });
            Mapper.CreateMap<Item, ItemModel>();

            Mapper.CreateMap<Run, RunListItemModel>();
            Mapper.CreateMap<Run, RunModel>();
            Mapper.CreateMap<Run, RunViewModel>()
                .ForMember(dest => dest.Population, opt => opt.Ignore());
            Mapper.CreateMap<Run, RunViewModel>()
                .ForMember(dest => dest.Population, opt => opt.Ignore());
            Mapper.CreateMap<Run, RunViewModel>()
                .ForMember(dest => dest.BinSize, opt => opt.MapFrom(src => src.Scenario.BinSize))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Scenario.Items))
                .ForMember(dest => dest.Population, opt => opt.Ignore());
            Mapper.CreateMap<Generation, GenerationModel>();
            Mapper.CreateMap<Population, PopulationModel>();
            Mapper.CreateMap<Population, PopulationViewModel>();
            Mapper.CreateMap<Bin, BinModel>();
            Mapper.CreateMap<Bin, BinViewModel>();
            Mapper.CreateMap<BinItem, BinItemModel>();
            Mapper.CreateMap<BinItem, BinItemViewModel>();

            Mapper.CreateMap<GenerationReportModel, Infrastructure.ApplicationServices.Models.GenerationReport.ReportDataItem>();

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
