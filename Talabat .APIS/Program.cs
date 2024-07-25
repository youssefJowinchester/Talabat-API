using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Interfaces;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Repositories;
using Talabat.Seevice;
using Talabat_.APIS.Extentions;
using Talabat_.APIS.MiddleWares;

namespace Talabat_.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region ConfigureService

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.AddApplicationService();

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequiredUniqueChars = 2;
                //options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddScoped<ITokenService, TokenService>();

            #endregion


            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _Context = services.GetRequiredService<StoreDbContext>();

            var _IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();

            var logger = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _Context.Database.MigrateAsync();

                await StoreDbContextSeed.SeedAsync(_Context);

                await _IdentityDbContext.Database.MigrateAsync();

                var _UserManager = services.GetRequiredService<UserManager<AppUser>>();

                await IdentityDbContextSeed.SeedUsersAsync(_UserManager);
            }
            catch (Exception ex)
            {
                var log = logger.CreateLogger<Program>();
                log.LogError(ex, "an Error has been occured during apling the migrations");
            }

            app.UseMiddleware<ExceptionMiddleWares>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
