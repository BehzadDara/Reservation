namespace Reservation.Domain.Statics
{
    public static class Error
    {
        public const string InvalidDayOfWeek = "روز هفته مجاز نیست";
        public const string CapacityCanNotBeNegative = "ظرفیت منفی مجاز نیست";
        public const string HourIsNotInValidRange = "ساعت بین ۹ تا ۱۷ مجاز است";
        public const string ThursdayHourIsNotInValidRange = "قرار ملاقات پنج شنبه ها تا ساعت ۱۳ است";
        public const string HourCapacityNotFound = "ظرفیتی یافت نشد";
        public const string HourCapacityFinished = "ظرفیت به اتمام رسیده است";
        public const string MeetingExist = "قرار ملاقات از قبل ثبت شده است";
        public const string MeetingNotFound = "قرار ملاقات یافت نشد";
        public const string HourPolicy = "زمان قرار ملاقات باید بیش از دو ساعت دیگر باشد";
    }
}