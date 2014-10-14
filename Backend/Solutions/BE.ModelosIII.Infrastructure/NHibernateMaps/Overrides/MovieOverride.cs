using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class MovieOverride : IAutoMappingOverride<Movie>
    {
        public void Override(AutoMapping<Movie> mapping)
        {
            mapping.HasMany(x => x.Genres)
                .Table("MovieGenres")
                .Element("Genre");
        }
    }
}
