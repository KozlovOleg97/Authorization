namespace Authorization.FacebookDemo
{
    public class OwnConfiguration
    {
        private readonly IConfiguration _configuration;

        public IConfiguration Configuration { get; }
        public OwnConfiguration(IConfiguration configuration) 
        { 
               _configuration = configuration;
        }

    }
}
