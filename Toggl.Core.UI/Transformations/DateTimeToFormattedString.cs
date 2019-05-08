using System;
using System.Globalization;
using Toggl.Core.Analytics;

namespace Toggl.Core.UI.Transformations
{
    public class DateTimeToFormattedString
    {
        public static string Convert(
            DateTimeOffset date,
            string format,
            TimeZoneInfo timeZoneInfo = null,
            IAnalyticsService analyticsService = null)
        {
            if (timeZoneInfo == null)
            {
                timeZoneInfo = TimeZoneInfo.Local;
            }

            return getDateTimeOffsetInCorrectTimeZone(date, timeZoneInfo).ToString(format, CultureInfo.InvariantCulture);
        }

        private static DateTimeOffset getDateTimeOffsetInCorrectTimeZone(
            DateTimeOffset value,
            TimeZoneInfo timeZone,
            IAnalyticsService analyticsService = null)
        {
            try
            {
                return value == default(DateTimeOffset) ? value : TimeZoneInfo.ConvertTime(value, timeZone);
            }
            catch (ArgumentOutOfRangeException)
            {
                analyticsService?.UnrepresentableDateErrorWhenConvertingDateToFormattedString?.Track(
                    value,
                    $"{timeZone.DisplayName}, {timeZone.BaseUtcOffset}"
                );
                return value;
            }
        }
    }
}
