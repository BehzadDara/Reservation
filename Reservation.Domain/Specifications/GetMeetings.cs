using System;
using System.Linq.Expressions;
using Reservation.Domain.Interfaces;
using Reservation.Domain.Models;

namespace Reservation.Domain.Specifications
{
    public class GetMeetings : Specification<Meeting>
    {

        private readonly int _year;
        private readonly int _month;
        private readonly int _day;
        private readonly int _time;

        public GetMeetings()
        {
            var dateTime = DateTime.Now;
            _year = dateTime.Year;
            _month = dateTime.Month;
            _day = dateTime.Day;
            _time = dateTime.Hour;
        }

        public override Expression<Func<Meeting, bool>> Criteria =>
            myMeeting => myMeeting.Year > _year || (myMeeting.Year == _year &&
                         myMeeting.Month > _month || (myMeeting.Month == _month &&
                         myMeeting.Day > _day || (myMeeting.Day == _day &&
                         myMeeting.Time >= _time
                         )));
    }
    
    
}