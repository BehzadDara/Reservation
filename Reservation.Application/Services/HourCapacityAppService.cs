using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Reservation.Application.Contracts;
using Reservation.Domain.Interfaces;
using Reservation.Domain.Models;
using Reservation.Domain.Specifications;

namespace Reservation.Application.Services
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class HourCapacityAppService: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HourCapacityAppService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<HourCapacity> repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unitOfWork.HourCapacityRepository = repository;
        }
        
        [HttpPost("HourCapacity")]
        public async Task<IActionResult> CreateHourCapacity(HourCapacityCreateDto input)
        {
            var myHourCapacity = await _unitOfWork.HourCapacityRepository.GetAsync(
                new GetHourCapacityByWeekDayAndHour(input.WeekDay,input.Hour));
            
            if (!(myHourCapacity is null))
            {
                myHourCapacity.ChangeCapacity(input.Capacity);
                await _unitOfWork.HourCapacityRepository.Update(myHourCapacity);
            }
            else
            {
                myHourCapacity = _mapper.Map<HourCapacity>(input);
                myHourCapacity.CheckThursdayHour();
                await _unitOfWork.HourCapacityRepository.Add(myHourCapacity);
            }
            await _unitOfWork.CompleteAsync();
            
            return Ok(_mapper.Map<HourCapacityDto>(myHourCapacity));
        }  
        
        [HttpGet("HourCapacities")]
        public async Task<IActionResult> GetHourCapacities()
        {
            var myHourCapacities = await _unitOfWork.HourCapacityRepository.ListAllAsync();
            var myHourCapacitiesOrdered = myHourCapacities.OrderBy(x => x.Hour).ThenBy(x => x.WeekDay);
            return Ok(_mapper.Map<IList<HourCapacityDto>>(myHourCapacitiesOrdered));
        }
        
        
    }
}