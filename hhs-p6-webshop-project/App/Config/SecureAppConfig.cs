namespace hhs_p6_webshop_project.App.Config
{
    public class SecureAppConfig
    {
        public SecureAppConfig()
        {
            // Set default values.
        }

        public string Owner { get; set; }

        public string SparkpostApiKey { get; set; }
        public string DbServer { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string DbName { get; set; }

        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
    }
}