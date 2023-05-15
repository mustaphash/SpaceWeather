using SpaceWeather.Exceptions;
using SpaceWeather.Models;
using System.Text.RegularExpressions;

namespace SpaceWeather.Args.Parsers
{
    public class PasswordArgParser : IArgParser
    {
        public Task Parse(string arg, InputModel model)
        {
            Regex regex = new Regex("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match match = regex.Match(arg);

            if (match.Success)
            {
                model.Password = arg;
            }
            else
            {
                throw new ArgException("Password is not strong enough");
            }

            return Task.CompletedTask;
        }
    }
}
