namespace SpaceWeather.Models
{
    public class InputModel
    {
        public string FileLocation { get; set; }

        public string SenderEmail { get; set; }

        public string Password { get; set; }

        public string ReceiverEmail { get; set; }

        public Task<bool> IsValid()
        {
            return Task.FromResult(
                FileLocation != null &&
                SenderEmail != null &&
                Password != null &&
                ReceiverEmail != null);
        }
    }
}
