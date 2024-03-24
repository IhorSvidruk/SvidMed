using System;
using System.Globalization;
using SvidMed.Common;

namespace SvidMed.Services.DateTimeParser
{
    public class DateTimeParserService : IDateTimeParserService
    {
        public DateTime ConvertStrings(string date, string time)
        {
            var dateString = date + " " + time;
            const string format = GlobalConstants.DateTimeFormats.DateTimeFormat;

            var dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}
