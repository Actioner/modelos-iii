using BE.ModelosIII.Domain.Contracts;
using BE.ModelosIII.Infrastructure.Helpers;
using Fasterflect;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Conventions
{
    #region Using Directives

    using FluentNHibernate.Conventions;

    #endregion

    public class TableNameConvention : IClassConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
            var overridenName = instance.EntityType.Attribute<TableNameAttribute>();

            string name = overridenName != null && !string.IsNullOrEmpty(overridenName.Value)
                              ? overridenName.Value
                              : instance.EntityType.Name.InflectTo().Pluralized;

            instance.Table(name);
        }
    }
}