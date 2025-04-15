using ECommerce.Application.Common.Behaviors;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register Mediator and validation
            services.AddMediatR(typeof(GetProductByIdQueryHandler).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssemblyContaining<CreateProductCommand>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
