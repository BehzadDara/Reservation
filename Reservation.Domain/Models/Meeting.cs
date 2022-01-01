using System.ComponentModel.DataAnnotations;
using Reservation.Domain.Implementations;

namespace Reservation.Domain.Models
{
    public class Meeting: TrackableEntity
    {
        [Required] public int Year { get; set; }
        [Required] public int Month { get; set; }
        [Required] public int Day { get; set; }
        [Required] public int Time { get; set; }
        [Required] public string Username { get; set; }
    }
}