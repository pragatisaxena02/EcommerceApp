using Microsoft.Extensions.DependencyInjection;
using Payments.Application.Services.PaymentService;
using Payments.Infrastructure.Repository;

namespace Payments.Application.Extensions;

    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServiceCollection( IServiceCollection services)
        {
		services.AddScoped<IDeliveryMethodRepository, DeliveryMethodRepository>();

		services.AddScoped<IBasketRepository, BasketRepository>();
		services.AddScoped<IPaymentService, PaymentService>();
		
            //services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            return services;
        }
    }
