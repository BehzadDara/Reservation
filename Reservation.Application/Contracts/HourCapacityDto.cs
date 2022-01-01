using System.ComponentModel.DataAnnotations;

namespace Reservation.Application.Contracts
{
    public class HourCapacityDto : EntityDto
    {
        public string WeekDay { get; set; }
        public string WeekDayValue { get; set; }
        public int Hour { get; set; }
        public int Capacity { get; set; }
    }
}