using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using Fasterflect;
using Machine.Specifications;
using Machine.Specifications.AutoMocking.Rhino;

namespace MSpecTests.BE.ModelosIII.Infraestructure.Repositories
{
    public class context_for_movie_repository : Specification<IScenarioRepository>
    {
        protected static Scenario scenario_one;
        protected static Scenario scenario_two;
        protected static Scenario scenario_three;

        Establish context = () =>
        {
            scenario_one = new Scenario();
            scenario_two = new Scenario();
            scenario_three = new Scenario();
            scenario_one.SetPropertyValue("Id", 1);
            scenario_one.SetPropertyValue("Name", "NameOne");

            scenario_two.SetPropertyValue("Id", 2);
            scenario_two.SetPropertyValue("Name", "NameTwo");

            scenario_three.SetPropertyValue("Id", 3);
            scenario_three.SetPropertyValue("Name", "NameThree");
        };
    }


    [Subject(typeof(IScenarioRepository))]
    public class When_find_by_name_return_equals : context_for_movie_repository
    {
        private static Scenario result;

        Because of = () => result = subject.GetByName("NameTwo");

        It should_return_scenario = () => result.ShouldNotBeNull();
        It should_return_scenario_two = () => result.ShouldBeTheSameAs(scenario_two);
    }
}
