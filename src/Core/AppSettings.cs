using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Core {
    public static class AppSettings {
        private static readonly IConfigurationRoot ConfigRoot;
        public static bool IsDevEnvironment {
            get {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                return env != null && env == "Development";
            }
        }

        static AppSettings() {
            ConfigRoot = IsDevEnvironment
                ? new ConfigurationBuilder().AddJsonFile("appsettings_Dev.json").Build()
                : new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public static class Database {
            public static string ConnectionString => ConfigRoot.GetConnectionString("AppDbConnex");
            public static string DesignConnectionString => ConfigRoot.GetConnectionString("DesignDbConnex");
        }

        public static class Admin {
            public static string Email => ConfigRoot["AdminUser:Email"];
            public static string Password => ConfigRoot["AdminUser:Password"];
        }

        public static class JwtToken {
            public static string Issuer => ConfigRoot["JwtToken:Issuer"];
            public static string Audience => ConfigRoot["JwtToken:Audience"];
            public static string SecurityKey => ConfigRoot["JwtToken:SecurityKey"];
        }

        public static class Cors {
            public static string Name => "TrustedOrigins";
            public static string[] TrustedOrigins {
                get {
                    return ConfigRoot.GetSection("TrustedOrigins")
                                     .GetChildren()
                                     .Select(x => x.Value)
                                     .ToArray();
                }
            }
        }

        public static class Path {
            public static string ProfilePictures => ConfigRoot["Path:ProfilePictures"];
            public static string PostBanners => ConfigRoot["Path:PostBanners"];
        }
    }
}
