using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Reflection;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.repository;
using Talabat.repository.Identity;
using Talabat.Services;
namespace Talabat.API.Extentions
{
    public static class ApplicationService
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            ///webApplicationBuilder.Services.AddScoped<IGenaricRepository<Product>, GenaricRepository<Product>>();
            ///webApplicationBuilder.Services.AddScoped<IGenaricRepository<ProductCategory>, GenaricRepository<ProductCategory>>();
            ///webApplicationBuilder.Services.AddScoped<IGenaricRepository<ProductBrand>, GenaricRepository<ProductBrand>>();
            ///services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));


            services.AddAutoMapper(M => M.AddProfile(new MappingProfile()) , AppDomain.CurrentDomain.GetAssemblies());


            //services.AddAutoMapper((configure) =>
            //{
            //    configure.AddProfile(typeof(MappingProfile));
            //});

            //webApplicationBuilder.Services.AddLogging()

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                             .SelectMany(M => M.Value.Errors)
                             .Select(M => M.ErrorMessage)
                             .ToArray();
                    var apiValidationErrorResponse = new ApiValidationErrorResponse(errors);
                    return new BadRequestObjectResult(apiValidationErrorResponse);
                });
            });

            return services;



        }







        public static WebApplication UseSwaggerMiddleware(this WebApplication webApplication)
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
            return webApplication;
        }


        public static WebApplication UseMiddlewares(this WebApplication webApplication)
        {
            webApplication.UseStatusCodePagesWithReExecute("/Errors/{0}");
            webApplication.UseHttpsRedirection();
            webApplication.UseStaticFiles();
            webApplication.UseAuthentication();
            webApplication.UseAuthorization();
            webApplication.MapControllers();
            return webApplication;
        }
    }
}
