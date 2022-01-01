using System.ComponentModel.DataAnnotations;
using Reservation.Application.Attributes;
using Reservation.Domain.Statics;

namespace Reservation.Application.Contracts
{
    public class HourCapacityCreateDto
    {
        [WeekDayControl(Error.InvalidDayOfWeek)][Required] public string WeekDay { get; set; }
        [RangeControl(Error.HourIsNotInValidRange,min:9,max:17)] [Required] public int Hour { get; set; }
        [RangeControl(Error.CapacityCanNotBeNegative,min:0)] [Required] public int Capacity { get; set; }
    }
}