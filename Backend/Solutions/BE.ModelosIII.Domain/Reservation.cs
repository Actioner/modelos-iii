using System;
using System.Collections.Generic;
using System.ComponentModel;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Reservation : Entity
    {
        public virtual string Code { get; set; }
        public virtual string Promotion { get; set; }
        public virtual double Total { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Seat> Seats { get; set; }
        public virtual Screening Screening { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual ReservationPaymentStatus ReservationPaymentStatus { get; set; }
    }

    public enum ReservationStatus
    {
        [Description("En Proceso")]
        InProcess,
        [Description("Activa")]
        Active,
        [Description("Cancelada")]
        Cancel, 
        [Description("Retirada")]
        Retire,
    }

    public enum ReservationPaymentStatus
    {
        [Description("Pagada")]
        Paid,
        [Description("No Pagada")]
        NotPaid
    }
}