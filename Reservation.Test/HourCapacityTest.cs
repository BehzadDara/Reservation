using NUnit.Framework;
using Reservation.Domain.Enums;
using Reservation.Domain.Models;

namespace Reservation.Test
{
    [TestFixture]
    public class HourCapacityTest
    {

        [Test]
        public void ChangeCapacity()
        {
            var myHourCapacity = new HourCapacity
            {
                WeekDay = WeekDay.Saturday,
                Hour = 10,
                Capacity = 3
            };
            myHourCapacity.ChangeCapacity(4);

            Assert.AreEqual(4, myHourCapacity.Capacity);
        }
        [Test]
        public void CheckThursdayHour()
        {
            var myHourCapacity = new HourCapacity
            {
                WeekDay = WeekDay.Thursday,
                Hour = 12,
                Capacity = 3
            };
            myHourCapacity.CheckThursdayHour();

            Assert.Pass();
        }
    }
}