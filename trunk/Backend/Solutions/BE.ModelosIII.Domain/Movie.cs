using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Movie : Entity
    {
        public virtual string Title { get; set; }
        [DomainSignature]
        public virtual string OriginalTitle { get; set; }
        [DomainSignature]
        public virtual int YearOfRelease { get; set; }
        public virtual string Director { get; set; }
        [LongText]
        public virtual string MainCast { get; set; }
        public virtual string Trailer { get; set; }
        [LongText]
        public virtual string Synopsis { get; set; }
        public virtual string SmallPoster { get; set; }
        public virtual string Poster { get; set; }
        public virtual int Runtime { get; set; }
        public virtual Rating Rating { get; set; }
        public virtual IList<Genre> Genres { get; set; }
        public virtual IList<Screening> Screenings { get; set; }

        public Movie()
        {
            Genres = new List<Genre>();
        }
    }

}