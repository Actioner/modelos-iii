using System.ComponentModel;
using BE.ModelosIII.Infrastructure.Helpers;
using Machine.Specifications;

namespace MSpecTests.BE.ModelosIII.Domain.Helpers
{
    public enum TestEnum
    {
        [Description("valor con descripcion")]
        ValorConDescripcion,
        ValorSinDescripcion
    }

    [Subject(typeof(Formatting))]
    public class When_to_description_of_attribute_with_description
    {
        static string result;

        Because of = () => result = TestEnum.ValorConDescripcion.ToDescription();

        It should_return_description_attribute_value = () => result.ShouldEqual("valor con descripcion");
    }

    [Subject(typeof(Formatting))]
    public class When_to_description_of_attribute_without_description
    {
        static string result;

        Because of = () => result = TestEnum.ValorSinDescripcion.ToDescription();

        It should_return_enum_value_as_string = () => result.ShouldEqual(TestEnum.ValorSinDescripcion.ToString());
    }
}
