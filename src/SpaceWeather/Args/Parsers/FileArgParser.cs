using SpaceWeather.Models;

namespace SpaceWeather.Args.Parsers
{
    internal class FileArgParser : IArgParser
    {
        public Task Parse(string arg, InputModel model)
        {
            model.FileLocation = arg;

            // TODO: Uncomment for production
            // TODO: Check if file is actually a csv

            if (File.Exists(arg))
            {
                model.FileLocation = arg;
            }
            else
            {
                throw new FileNotFoundException(arg);
            }

            return Task.CompletedTask;
        }
    }
}
