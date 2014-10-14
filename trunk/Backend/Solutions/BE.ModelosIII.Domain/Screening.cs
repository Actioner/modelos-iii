using System;
using BE.ModelosIII.Domain.Settings;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Screening : Entity
    {
        public virtual DateTime StartDate { get; set; }
        public virtual Screen Screen { get; set; }
        public virtual Movie Movie { get; set; }

        public virtual DateTime EndDate
        {
            get { return StartDate.AddMinutes(Movie.Runtime + BackendSettings.ScreeningIntervalInMinutes); } 
        }
    }
}