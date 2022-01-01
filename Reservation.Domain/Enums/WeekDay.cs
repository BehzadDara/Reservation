using System.ComponentModel;

namespace Reservation.Domain.Enums
{
    public enum WeekDay
    {
        [Description("شنبه")]
        Saturday,
        [Description("یک شنبه")]
        Sunday,
        [Description("دو شنبه")]
        Monday,
        [Description("سه شنبه")]
        Tuesday,
        [Description("چهار شنبه")]
        Wednesday,
        [Description("پنج شنبه")]
        Thursday, 
    }
}