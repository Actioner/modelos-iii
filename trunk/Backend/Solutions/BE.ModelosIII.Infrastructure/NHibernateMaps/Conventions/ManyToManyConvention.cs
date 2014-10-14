using System.Diagnostics;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Conventions
{
    #region Using Directives

    using FluentNHibernate.Conventions;

    #endregion

    public class ManyToManyConvention : IHasManyToManyConvention
    {
        #region IConvention<IManyToManyCollectionInspector,IManyToManyCollectionInstance> Members

        public void Apply(IManyToManyCollectionInstance instance)
        {
            Debug.Assert(instance.OtherSide != null);

            // Hack:  the cast is nessesary because the compiler tries to take the Method and not the property
            if (((IManyToManyCollectionInspector)instance.OtherSide).Inverse)
            {
                instance.Table(
                   string.Format(
                       "{0}To{1}",
                       instance.EntityType.Name.InflectTo().Pluralized,
                       instance.ChildType.Name.InflectTo().Pluralized));
            }
            else
            {
                instance.Inverse();
            }
            instance.Cascade.All();
        }

        #endregion
    }
}