using System.Configuration;

namespace JkvoXyz.Config
{
    public class CoreDbConfig : System.Configuration.ConfigurationSection
    {

        protected static ConfigurationPropertyCollection _Properties;

        private static readonly ConfigurationProperty _Host =
            new ConfigurationProperty("Host",
                typeof(string),
                "jkvolocal"
            );

        private static readonly ConfigurationProperty _Username =
            new ConfigurationProperty("Username",
                typeof(string),
                "root"
            );

        private static readonly ConfigurationProperty _Shards =
            new ConfigurationProperty("Shards",
                typeof(int),
                3
            );

        private static readonly ConfigurationProperty _Database =
            new ConfigurationProperty("Database",
                typeof(string),
                "Jkvo"
            );

        protected override ConfigurationPropertyCollection Properties => _Properties;

        public CoreDbConfig()
        {
            _Properties = new ConfigurationPropertyCollection();

            _Properties.Add(_Host);
            _Properties.Add(_Username);
            _Properties.Add(_Shards);
            _Properties.Add(_Database);
        }

        protected override object GetRuntimeObject()
        {
            // To enable property setting just assign true to
            // the following flag.
            return base.GetRuntimeObject();
        }

        public string Host
        {
            get => (string)this["Host"];
            set => this["Host"] = value;
        }
        public string Username {
            get => (string)this["Username"];
            set => this["Username"] = value;
        }
        public int Shards {
            get => (int)this["Shards"];
            set => this["Shards"] = value;
        }
        public string Database {
            get => (string)this["Database"];
            set => this["Database"] = value;
        }
    }
}
