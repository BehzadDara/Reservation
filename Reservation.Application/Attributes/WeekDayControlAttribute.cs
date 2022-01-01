using System;
using System.ComponentModel.DataAnnotations;
using Reservation.Domain.Enums;

namespace Reservation.Application.Attributes
{
    public class WeekDayControlAttribute : ValidationAttribute
    {

        public WeekDayControlAttribute( string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            return  Enum.TryParse(value.ToString(), out WeekDay _);
        }
    }
}