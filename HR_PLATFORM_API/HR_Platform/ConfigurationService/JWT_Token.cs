//using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;

//namespace HR_PLATFORM.ConfigurationService
//{
//    public class JWT_Token(IConfiguration configuration)
//    {
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        ValidIssuer = configuration["Jwt:Issuer"],
//                        ValidAudience = configuration["Jwt:Issuer"],
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
//                    };
//                });
//            services.AddMvc();
//        }
//    }
//}
