namespace hhs_p6_webshop_project.App.Config
{
    public class SecureAppConfig
    {
        public SecureAppConfig()
        {
            // Set default values.
        }

        public virtual string Owner { get; set; }

        public virtual string SparkpostApiKey { get; set; }
        public virtual string DbServer { get; set; }
        public virtual string DbUser { get; set; }
        public virtual string DbPassword { get; set; }
        public virtual string DbName { get; set; }

        public virtual string GoogleClientId { get; set; }
        public virtual string GoogleClientSecret { get; set; }
    }
}