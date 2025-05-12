using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  // 발급자 검증
            ValidateAudience = true,  // 대상 검증
            ValidateLifetime = true,  // 토큰 유효 기간 검증
            ValidateIssuerSigningKey = true,  // 서명된 키 검증
            ValidIssuer = jwtSettings["Issuer"],  // 발급자 URL
            ValidAudience = jwtSettings["Audience"],  // 대상 URL
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))  // 비밀키
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();