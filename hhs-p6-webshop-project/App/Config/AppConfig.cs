namespace hhs_p6_webshop_project.App.Config
{
    public class AppConfig
    {
        /// <summary>
        /// True to reset the database on startup, false to use the existing database.
        /// </summary>
        public bool DatabaseReset = false;

        /// <summary>
        /// True to exit the application after the database is reset, false to keep the application running.
        /// The application will never exit if the database is not being reset.
        /// </summary>
        public bool DatabaseExitAfterReset = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="init">True to immediately initialize the configuration, false if not.</param>
        public AppConfig(bool init)
        {
            // Initialize
            if (init)
                Init();
        }

        /// <summary>
        /// Configuration initialization code.
        /// </summary>
        public void Init()
        {
            // Add configuration initialization code here, such as environment variable parsing.
        }
    }
}