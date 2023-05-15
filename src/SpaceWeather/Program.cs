using SpaceWeather.Exceptions;
using SpaceWeather.Models;
using SpaceWeather.Models.Enums;
using SpaceWeather.Parsers;
using System.Text;

namespace SpaceWeather
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                args = "-f Space.csv -e mshakir9918@gmail.com -p 1qaz@WSX3edc -r e.mehmed01@gmail.com".Split(" ");

                CommandLineArgumentsParser parser = new CommandLineArgumentsParser();
                var inputModel = await parser.Parse(args);

                string[] content = await File.ReadAllLinesAsync(inputModel.FileLocation);

                Dictionary<int, WeatherDataModel> weatherData = new Dictionary<int, WeatherDataModel>();

                // Header Parse
                var header = content[0];
                foreach (var headerItem in header.Split(";").Skip(1))
                {
                    var day = int.Parse(headerItem);
                    weatherData.Add(day, new WeatherDataModel());
                }

                // Content Parse
                foreach (string? row in content.Skip(1))
                {
                    int i = 1;
                    var data = row.Split(";").First();

                    if (data == "Temperature (C)")
                    {
                        foreach (var temperature in row.Split(";").Skip(1))
                        {
                            weatherData[i].Temperature = int.Parse(temperature);
                            i++;
                        }
                    }
                    else if (data == "Wind (m/s)")
                    {
                        foreach (var wind in row.Split(";").Skip(1))
                        {
                            weatherData[i].Wind = int.Parse(wind);
                            i++;
                        }
                    }
                    else if (data == "Humidity (%)")
                    {
                        foreach (var humidity in row.Split(";").Skip(1))
                        {
                            weatherData[i].Humidity = int.Parse(humidity);
                            i++;
                        }
                    }
                    else if (data == "Precipitation (%)")
                    {
                        foreach (var precipitation in row.Split(";").Skip(1))
                        {
                            weatherData[i].Precipitation = int.Parse(precipitation);
                            i++;
                        }
                    }
                    else if (data == "Lightning")
                    {
                        foreach (var lightning in row.Split(";").Skip(1))
                        {
                            weatherData[i].Lightning = lightning.ToLower() == "yes";
                            i++;
                        }
                    }
                    else if (data == "Clouds")
                    {
                        foreach (var cloud in row.Split(";").Skip(1))
                        {
                            Clouds parsedCloud;

                            if (Enum.TryParse(cloud, out parsedCloud))
                            {
                                weatherData[i].Cloud = parsedCloud;
                                i++;
                            }
                        }
                    }
                }

                // Generate a report file:
                StringBuilder sb = new StringBuilder();

                // Header.
                sb.Append("Day/Parameter;");
                foreach (var weather in weatherData)
                {
                    sb.Append($"{weather.Key};");
                }
                sb.AppendLine();

                foreach (var weather in weatherData.Where(c => c.Value.IsClearToTakeoff))
                {
                    sb.Append($"{weather.Value.Temperature};");
                }
                sb.AppendLine();

                foreach (var weather in weatherData.Where(c => c.Value.IsClearToTakeoff))
                {
                    sb.Append($"{weather.Value.Wind};");
                }
                sb.AppendLine();

                foreach (var weather in weatherData.Where(c => c.Value.IsClearToTakeoff))
                {
                    sb.Append($"{weather.Value.Humidity};");
                }
                sb.AppendLine();

                foreach (var weather in weatherData.Where(c => c.Value.IsClearToTakeoff))
                {
                    sb.Append($"{weather.Value.Precipitation};");
                }
                sb.AppendLine();

                foreach (var weather in weatherData.Where(c => c.Value.IsClearToTakeoff))
                {
                    sb.Append($"{weather.Value.Lightning};");
                }
                sb.AppendLine();

                foreach (var weather in weatherData.Where(c => c.Value.IsClearToTakeoff))
                {
                    sb.Append($"{weather.Value.Cloud};");
                }
                sb.AppendLine();

                await File.WriteAllTextAsync("WeatherReport.csv", sb.ToString());

            }
            catch (ArgNotFoundException ex)
            {
            }
            catch (NotSupportedFileTypeException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }
    }
}