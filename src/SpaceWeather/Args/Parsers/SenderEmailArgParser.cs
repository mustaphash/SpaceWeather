using SpaceWeather.Exceptions;
using SpaceWeather.Models;

namespace SpaceWeather.Args.Parsers
{
    internal class SenderEmailArgParser : IArgParser
    {
        public Task Parse(string arg, InputModel model)
        {
            string[] checkAddress = arg.Split("@");
            if (checkAddress.Length > 0)
            {
                model.SenderEmail = arg;
            }
            else
            {
                throw new ArgException("Please enter valid email!");
            }

            return Task.CompletedTask;
        }
    }
}
