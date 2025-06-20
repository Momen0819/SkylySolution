using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Skyly.Business.Extensions;
using Skyly.Domain;
using System;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<SkylyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ MVC
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

// ✅ الجلسات
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ Inject Business Layer
builder.Services.AddSkylyBusinessServices(builder.Configuration);

// ✅ JWT Authentication
builder.Services.AddAuthentication("SkylyAdmin") // ← لازم نفس الاسم اللي هتستخدمه بعدين
    .AddJwtBearer("SkylyAdmin", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("tL4m&9eZqB$R1x!WaU7@rM3^NgpXv2#J"))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["SkylyAdminToken"];
                if (!string.IsNullOrEmpty(token))
                    context.Token = token;

                return Task.CompletedTask;
            }
        };
    });

// ✅ صلاحيات (Authorization)
builder.Services.AddAuthorization();

var app = builder.Build();

// ✅ Seed database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SkylyDbContext>();
    DbInitializer.Seed(db);
}

// ✅ Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// ✅ Static Files
app.UseStaticFiles();

// ✅ Routing
app.UseRouting();

// ✅ Session & Auth
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// ✅ Routing الافتراضي
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
