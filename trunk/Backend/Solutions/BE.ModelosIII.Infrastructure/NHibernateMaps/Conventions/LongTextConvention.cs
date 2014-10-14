using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts;
using Fasterflect;
using FluentNHibernate.Conventions.Instances;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Conventions
{
    #region Using Directives

    using FluentNHibernate.Conventions;

    #endregion

    public class LongTextConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.MemberInfo.HasAttribute<LongTextAttribute>())
            {
                instance.CustomSqlType("text");
            }
        }
    }
}