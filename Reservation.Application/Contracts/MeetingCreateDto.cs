using System.ComponentModel.DataAnnotations;
using Reservation.Application.Attributes;
using Reservation.Domain.Statics;

namespace Reservation.Application.Contracts
{
    public class MeetingCreateDto
    {
        [RangeControl(Error.HourIsNotInValidRange,min:2022)][Required] public int Year { get; set; }
        [RangeControl(Error.HourIsNotInValidRange,min:1,max:12)][Required] public int Month { get; set; }
        [RangeControl(Error.HourIsNotInValidRange,min:1,max:31)][Required] public int Day { get; set; }
        [Required] public int Time { get; set; }
        [MaxLength(20)][Required] public string Username { get; set; }
    }
}