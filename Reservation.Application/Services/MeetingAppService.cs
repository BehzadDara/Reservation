using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Contracts;
using Reservation.Domain.Interfaces;
using Reservation.Domain.Models;
using Reservation.Domain.Specifications;
using Reservation.Domain.Statics;

namespace Reservation.Application.Services
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class MeetingAppService: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MeetingAppService(IMapper mapper, IUnitOfWork unitOfWork,
            IRepository<Meeting> repository, IRepository<HourCapacity> hourCapacityRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unitOfWork.MeetingRepository = repository;
            _unitOfWork.HourCapacityRepository = hourCapacityRepository;
        }
        
        [HttpPost("Meeting")]
        public async Task<IActionResult> CreateMeeting(MeetingCreateDto input)
        {
            var myMeeting = _mapper.Map<Meeting>(input);

            var date = new DateTime(myMeeting.Year,myMeeting.Month,myMeeting.Day,myMeeting.Time,0,0);
            if (date < DateTime.Now.AddHours(2))
            {
                return BadRequest(new ResponseDto(Error.HourPolicy));
            }

            var meetingExist = await _unitOfWork.MeetingRepository.GetAsync(new GetMeetingByDayAndTimeAndUsername(myMeeting.Year,myMeeting.Month,myMeeting.Day,myMeeting.Time,myMeeting.Username));
            if (!(meetingExist is null))
            {
                return BadRequest(new ResponseDto(Error.MeetingExist));
            }
            
            var hourCapacity = await _unitOfWork.HourCapacityRepository.GetAsync(new GetHourCapacityByWeekDayAndHour(date.DayOfWeek.ToString(),myMeeting.Time));
            if (hourCapacity is null)
            {
                return NotFound(new ResponseDto(Error.HourCapacityNotFound));
            }
            
            var reserved = await _unitOfWork.MeetingRepository.ListAsync(new GetMeetingsByDayAndTime(myMeeting.Year,myMeeting.Month,myMeeting.Day,myMeeting.Time));
            if (hourCapacity.Capacity - reserved.Count <= 0)
            {
                return BadRequest(new ResponseDto(Error.HourCapacityFinished));
            }
            
            await _unitOfWork.MeetingRepository.Add(myMeeting);
            await _unitOfWork.CompleteAsync();
            
            return Ok(_mapper.Map<MeetingDto>(myMeeting));
        }

        [HttpGet("Meetings/{username}")]
        public async Task<IActionResult> GetMeetingsByUsername(string username)
        {
            var myMeetings = await _unitOfWork.MeetingRepository.ListAsync(new GetMeetingsByUsername(username));
            var myMeetingsOrdered = myMeetings.OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ThenBy(x => x.Day)
                .ThenBy(x => x.Time);
            return Ok(_mapper.Map<IList<MeetingDto>>(myMeetingsOrdered));
        }
        [HttpGet("Meetings")]
        public async Task<IActionResult> GetMeetings()
        {
            var myMeetings = await _unitOfWork.MeetingRepository.ListAsync(new GetMeetings());
            var myMeetingsOrdered = myMeetings.OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ThenBy(x => x.Day)
                .ThenBy(x => x.Time);
            return Ok(_mapper.Map<IList<MeetingDto>>(myMeetingsOrdered));
        }
        
        [HttpDelete("Meeting")]
        public async Task<IActionResult> DeleteMeeting(Guid input)
        {
            var myMeeting = await _unitOfWork.MeetingRepository.GetByIdAsync(input);
            if (myMeeting is null)
            {
                return NotFound(new ResponseDto(Error.MeetingNotFound));
            }

            await _unitOfWork.MeetingRepository.DeleteAsync(myMeeting);
            await _unitOfWork.CompleteAsync();
            
            return NoContent();
        }
        
    }
}