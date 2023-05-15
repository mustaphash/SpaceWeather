using SpaceWeather.Args.Parsers;
using SpaceWeather.Exceptions;
using SpaceWeather.Models;

namespace SpaceWeather.Parsers
{
    public class CommandLineArgumentsParser
    {
        private readonly Dictionary<string, IArgParser> _argParsers;

        public CommandLineArgumentsParser()
        {
            _argParsers = new Dictionary<string, IArgParser>()
            {
                { "-f", new FileArgParser() },
                { "--file", new FileArgParser() },
                { "-e", new SenderEmailArgParser() },
                { "--sender-email", new SenderEmailArgParser() },
                { "-p", new PasswordArgParser() },
                { "--password", new PasswordArgParser() },
                { "-r", new ReceiverEmailArgParser() },
                { "--receiver-email", new ReceiverEmailArgParser() },
            };
        }

        public async Task<InputModel> Parse(string[] args)
        {
            Stack<string> cliArgs = new Stack<string>();
            InputModel inputModel = new InputModel();

            if (args.Any())
            {
                cliArgs = new Stack<string>(args.Reverse());

                while (cliArgs.Count > 0)
                {
                    string currentArgument = cliArgs.Pop();

                    if (!_argParsers.ContainsKey(currentArgument))
                    {
                        throw new ArgNotFoundException(currentArgument);
                    }

                    IArgParser parser = _argParsers[currentArgument];

                    var paramValue = cliArgs.Pop();
                    await parser.Parse(paramValue, inputModel);
                }

                if (!(await inputModel.IsValid()))
                {
                    throw new ArgNotFoundException("Not all required args are supplied.");
                }
            }

            return inputModel;
        }
    }
}
