using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Interfaces;
using Talabat.Core.Services.Interfaces;
using Talabat.Repository;
using Talabat.Repository.Repositories;
using Talabat.Seevice;
using Talabat_.APIS.Errors;
using Talabat_.APIS.Helpers;

namespace Talabat_.APIS.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                            .Select(E => E.ErrorMessage).ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            return services;
        }
    }
}
