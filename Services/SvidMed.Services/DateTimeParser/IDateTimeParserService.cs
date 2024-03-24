using System;

namespace SvidMed.Services.DateTimeParser
{
    public interface IDateTimeParserService
    {
        DateTime ConvertStrings(string date, string time);
    }
}
