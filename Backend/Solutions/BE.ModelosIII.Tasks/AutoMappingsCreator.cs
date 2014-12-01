using System;
using System.Globalization;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Run;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.DomainModel;
using BE.ModelosIII.Tasks.Commands.Configuration;

namespace BE.ModelosIII.Tasks
{
    public static class AutoMappingsCreator
    {
        public static void CreateCommandMappings()
        {
            Mapper.CreateMap<Entity, int>().ConvertUsing(entity => entity.Id);

            Mapper.CreateMap<ConfigurationCommand, Scenario>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ScenarioId))
                .ForMember(dest => dest.CrossoverProbability, opt => opt.MapFrom(src => src.CrossoverProbability / 100))
                .ForMember(dest => dest.MutationProbability, opt => opt.MapFrom(src => src.MutationProbability / 100))
                .ForMember(dest => dest.BinSize, opt => opt.Ignore())
                .ForMember(dest => dest.Runs, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore());

            Mapper.CreateMap<CreateScenarioCommand, Scenario>()
                .ForMember(dest => dest.Runs, opt => opt.Ignore())
                .ForMember(dest => dest.CrossoverProbability, opt => opt.MapFrom(src => src.Configuration.CrossoverProbability / 100))
                .ForMember(dest => dest.MutationProbability, opt => opt.MapFrom(src => src.Configuration.MutationProbability / 100))
                .ForMember(dest => dest.PopulationSize, opt => opt.MapFrom(src => src.Configuration.PopulationSize))
                .ForMember(dest => dest.StopCriterion, opt => opt.MapFrom(src => src.Configuration.StopCriterion))
                .ForMember(dest => dest.StopDepth, opt => opt.MapFrom(src => src.Configuration.StopDepth))
                .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Configuration.Report));
            Mapper.CreateMap<Item, ItemCommand>();
            Mapper.CreateMap<ItemCommand, Item>()
                .ForMember(dest => dest.Scenario, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
