
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Talabat.API.Errors;
using Talabat.API.Extentions;
using Talabat.API.Helpers;
using Talabat.API.MiddleWares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository.Contract;
using Talabat.repository;
using Talabat.repository.Data;
using Talabat.repository.Identity;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services  & can add 6 Configuration 
            // Add services to the container.
            webApplicationBuilder.Services.AddControllers();

            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            webApplicationBuilder.Services.AddOpenApi();



            webApplicationBuilder.Services.AddDbContext<TalabatDbContext>(option =>
            {
                option.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });


            #region Redis
            webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            {
                try
                {
                    var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
                    return ConnectionMultiplexer.Connect(connection);
                }
                catch
                {
                    return null;
                }
            }); 
            #endregion





            webApplicationBuilder.Services.AddDbContext<TalabatIdentityDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("Identity"));
            });


            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddIdentityService(webApplicationBuilder.Configuration);


            #endregion

            var app = webApplicationBuilder.Build();





            #region Configure Kestrel Middlewares 
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerMiddleware();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            #endregion





            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _dbContext = service.GetRequiredService<TalabatDbContext>();
            var _identityDbContext = service.GetRequiredService<TalabatIdentityDbContext>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();


            ILoggerFactory loggerFactory = service.GetRequiredService<ILoggerFactory>();
            ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await TalabatDbContextSeed.SeedAsync(_dbContext);

                await _identityDbContext.Database.MigrateAsync();
                await TalabatIdentityDbContextSeed.SeedAsync(userManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "an error occure when migrate database");
            }


            #region 3 Way Of Declare ModdleWare علي السخان
            //app.Use(async (context, next) =>
            // {
            //     try
            //     {
            //         await next.Invoke(context);
            //     }
            //     catch (Exception ex)
            //     {
            //         logger.LogError(ex.Message);       // Log Error On Console in Development Environment
            //                                            // In Production log Exception in Database | File


            //         // 1. Response Request
            //         context.Response.ContentType = "application/json";
            //         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            //         // 2. Response Body
            //         var responseBody = webApplicationBuilder.Environment.IsDevelopment() ? new ApiExceptionResponse(ex.StackTrace, null, (int)HttpStatusCode.InternalServerError)
            //                       : new ApiExceptionResponse(null, null, (int)HttpStatusCode.InternalServerError);


            //         var jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


            //         var json = JsonSerializer.Serialize(responseBody, jsonOptions);
            //         await context.Response.WriteAsync(json);


            //     }
            //});
            #endregion

            app.UseMiddlewares();
            app.MapGet("/", () => Results.Redirect("/swagger"));
            app.Run();
        }

     
    }
}
