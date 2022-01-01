using AutoMapper;
using Humanizer;
using Reservation.Application.Contracts;
using Reservation.Domain.Models;

namespace Reservation.Api
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<HourCapacityCreateDto, HourCapacity>();
            CreateMap<HourCapacity, HourCapacityDto>()
                .ForMember(x => x.WeekDayValue, opt
                    => opt.MapFrom(c => c.WeekDay.Humanize()));

            CreateMap<MeetingCreateDto, Meeting>();
            CreateMap<Meeting, MeetingDto>()
                .ForMember(x=>x.Date,opt
                    =>opt.MapFrom(c=>c.Year+"/"+c.Month+"/"+c.Day));

        }

    }
}