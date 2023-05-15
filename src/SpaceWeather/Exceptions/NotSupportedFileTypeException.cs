﻿namespace SpaceWeather.Exceptions
{
    public class NotSupportedFileTypeException : Exception
    {
        public NotSupportedFileTypeException(string message)
            : base(message)
        {
        }
    }
}
