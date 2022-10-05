namespace Entities.Settings
{
    public class CloudinarySettings
    {
        private string apiKey = Environment.GetEnvironmentVariable("CLOUDKEY");
        private string apiSecret = Environment.GetEnvironmentVariable("CLOUDSECRET");
        public string CloudName { get; set; }
        public string ApiKey 
        { 
            get { return apiKey; }
            set { apiKey = value; } 
        }
        public string ApiSecret 
        { 
            get { return apiSecret; } 
            set { apiSecret = value; } 
        }
    }
}