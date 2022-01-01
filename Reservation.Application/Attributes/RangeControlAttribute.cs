using System.ComponentModel.DataAnnotations;

namespace Reservation.Application.Attributes
{
    public class RangeControlAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public RangeControlAttribute( string errorMessage,int min = int.MinValue, int max = int.MaxValue)
        {
            ErrorMessage = errorMessage;
            _min = min;
            _max = max;
        }

        public override bool IsValid(object value)
        {
            var myInt = int.Parse(value.ToString());
            return myInt >= _min && myInt <= _max;
        }
    }
}