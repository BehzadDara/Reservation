using System.ComponentModel.DataAnnotations;
using System.Data;
using Reservation.Domain.Enums;
using Reservation.Domain.Implementations;
using Reservation.Domain.Statics;

namespace Reservation.Domain.Models
{
    public class HourCapacity: TrackableEntity
    {
        [Required] public WeekDay WeekDay { get; set; }
        [Required] public int Hour { get; set; }
        [Required] public int Capacity { get; set; }

        public void ChangeCapacity(int newCapacity)
        {
            Capacity = newCapacity;
        }

        public void CheckThursdayHour()
        {
            if (WeekDay != WeekDay.Thursday) return;
            if (Hour > 13)
                throw new DataException(Error.ThursdayHourIsNotInValidRange);
        }
    }
}