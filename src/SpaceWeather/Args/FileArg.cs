namespace SpaceWeather.Args
{
    public class FileArg : IArg
    {
        public FileArg()
        {
            ShortName = "-f";
            LongName = "--file";

            Help = "Path to the config file.";
        }

        public string ShortName { get; }

        public string LongName { get; }

        public string Help { get; }
    }
}
