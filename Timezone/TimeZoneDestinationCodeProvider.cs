using NodaTime;
using NodaTime.TimeZones;

namespace Benchmarks.Timezone
{


    public class TimezoneToCountryMapper
    {
        private readonly IDateTimeZoneProvider _timeZoneProvider;
        public TimezoneToCountryMapper()
        {
            _timeZoneProvider = DateTimeZoneProviders.Tzdb;
        }

        public string GetCountryIsoCode(string timeZoneId)
        {
            try
            {
                var zone = _timeZoneProvider.GetZoneOrNull(timeZoneId);
                if (zone == null) return null!;

                var location = TzdbDateTimeZoneSource.Default.ZoneLocations!
                    .FirstOrDefault(loc => loc.ZoneId == timeZoneId);

                return location?.CountryCode!;
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
