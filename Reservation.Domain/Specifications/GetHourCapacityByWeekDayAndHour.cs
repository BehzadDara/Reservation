using System;
using System.Linq.Expressions;
using Reservation.Domain.Enums;
using Reservation.Domain.Interfaces;
using Reservation.Domain.Models;

namespace Reservation.Domain.Specifications
{
    public class GetHourCapacityByWeekDayAndHour : Specification<HourCapacity>
    {

        private readonly WeekDay _weekDay;
        private readonly int _hour;

        public GetHourCapacityByWeekDayAndHour(string weekDay, int hour)
        {
            _weekDay = (WeekDay) Enum.Parse(typeof(WeekDay), weekDay, true);
            _hour = hour;
        }

        public override Expression<Func<HourCapacity, bool>> Criteria =>
            myHourCapacity => myHourCapacity.WeekDay == _weekDay && myHourCapacity.Hour == _hour;
    }
    
    
}