using System.ComponentModel;

namespace BE.ModelosIII.Domain
{
    public enum Genre
    {
        [Description("Acción")]
        Action = 1,
        [Description("Aventura")]
        Adventure,
        [Description("Animación")]
        Animation,
        [Description("Biografía")]
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
        [Description("Fantasía")]
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
        [Description("Ciencia Ficción")]
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