using System.ComponentModel;

namespace BE.ModelosIII.Domain
{
    public enum Genre
    {
        [Description("Acci�n")]
        Action = 1,
        [Description("Aventura")]
        Adventure,
        [Description("Animaci�n")]
        Animation,
        [Description("Biograf�a")]
        Biography,
        [Description("Comedia")]
        Comedy,
        [Description("Crimen")]
        Crime,
        [Description("Documental")]
        Documentary,
        [Description("Drama")]
        Drama,
        [Description("Familia")]
        Family,
        [Description("Fantas�a")]
        Fantasy,
        [Description("Historia")]
        History,
        [Description("Horror")]
        Horror,
        [Description("Musical")]
        Musical,
        [Description("Misterio")]
        Mistery,
        [Description("Romance")]
        Romance,
        [Description("Ciencia Ficci�n")]
        SciFi,
        [Description("Deporte")]
        Sports,
        [Description("Thriller")]
        Thriller,
        [Description("Guerra")]
        War,
        [Description("Western")]
        Western
    }
}