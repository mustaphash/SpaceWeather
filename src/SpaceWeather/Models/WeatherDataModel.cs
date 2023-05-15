using SpaceWeather.Models.Enums;

namespace SpaceWeather.Models
{
    public class WeatherDataModel
    {
        public int Temperature { get; set; }

        public int Wind { get; set; }

        public int Humidity { get; set; }

        public int Precipitation { get; set; }

        public bool Lightning { get; set; }

        public Clouds Cloud { get; set; }

        public bool IsClearToTakeoff =>
            Wind <= 10 &&
            Temperature >= 2 && Temperature <= 31 &&
            Humidity < 60 &&
            Precipitation == 0 &&
            !Lightning &&
            Cloud != Clouds.Cumulus && Cloud != Clouds.Nimbus;
    }
}
