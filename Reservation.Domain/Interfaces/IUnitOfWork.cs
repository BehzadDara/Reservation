using System.Threading;
using System.Threading.Tasks;
using Reservation.Domain.Models;

namespace Reservation.Domain.Interfaces
{
    public interface IUnitOfWork
    {

        public IRepository<HourCapacity> HourCapacityRepository { get; set; }
        public IRepository<Meeting> MeetingRepository { get; set; }
        Task<bool> CompleteAsync(CancellationToken cancellationToken = default);
    }
}