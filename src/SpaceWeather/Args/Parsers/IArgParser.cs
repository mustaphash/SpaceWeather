using SpaceWeather.Models;

namespace SpaceWeather.Args.Parsers
{
    public interface IArgParser
    {
        Task Parse(string arg, InputModel model);
    }
}
