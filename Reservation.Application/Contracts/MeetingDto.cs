namespace Reservation.Application.Contracts
{
    public class MeetingDto : EntityDto
    {        
        public string Date { get; set; }
        public int Time { get; set; }
        public string Username { get; set; }
    }
}