using System;
using System.Globalization;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Run;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Tasks
{
    public static class AutoMappingsCreator
    {
        public static void CreateCommandMappings()
        {
            Mapper.CreateMap<Entity, int>().ConvertUsing(entity => entity.Id);

            Mapper.CreateMap<CreateScenarioCommand, Scenario>();
            Mapper.CreateMap<Item, ItemCommand>();
            Mapper.CreateMap<ItemCommand, Item>()
                .ForMember(dest => dest.Scenario, opt => opt.Ignore());

            Mapper.CreateMap<CreateRunCommand, Run>()
                .ForMember(dest => dest.Scenario, opt => opt.Ignore())
                .ForMember(dest => dest.Generations, opt => opt.Ignore())
                .ForMember(dest => dest.RunOn, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
