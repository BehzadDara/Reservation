using System;
using System.Linq.Expressions;
using Reservation.Domain.Interfaces;
using Reservation.Domain.Models;

namespace Reservation.Domain.Specifications
{
    public class GetMeetingByDayAndTimeAndUsername : Specification<Meeting>
    {

        private readonly int _year;
        private readonly int _month;
        private readonly int _day;
        private readonly int _time;
        private readonly string _username;

        public GetMeetingByDayAndTimeAndUsername(int year, int month, int day, int time, string username)
        {
            _year = year;
            _month = month;
            _day = day;
            _time = time;
            _username = username;
        }

        public override Expression<Func<Meeting, bool>> Criteria =>
            myMeeting => myMeeting.Year == _year && 
                         myMeeting.Month == _month &&
                         myMeeting.Day == _day &&
                         myMeeting.Time == _time &&
                         myMeeting.Username == _username;
    }
    
    
}