using System.Configuration;

namespace JkvoXyz.Config
{
    public class FullDbConfig : CoreDbConfig
    {
        private static readonly ConfigurationProperty _Password =
            new ConfigurationProperty("Password",
                typeof(string),
                "password"
            );

        public FullDbConfig() : base()
        {
            _Properties.Add(_Password);
        }

        public string Password
        {
            get => (string)this["Password"];
            set => this["Password"] = value;
        }

    }
}
