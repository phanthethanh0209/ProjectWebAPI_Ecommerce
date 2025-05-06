using AutoMapper;
using ECommerce.Application.Features.Orders.Commands.CreateOrder;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.UpdateProduct;
using ECommerce.Application.Features.Products.DTOs;
using ECommerce.Application.Features.Users.Commands.Register;
using ECommerce.Application.Features.Users.Commands.UpdateUser;
using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Common.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            // Mapping Product
            CreateMap<Product, GetProductResponse>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            //CreateMap<DeleteProductCommand, Product>();

            // Mapping User
            CreateMap<RegisterCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, GetUserResponse>();

            // Mapping Order
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(t => t.UpdateAt, opt => opt.Ignore());

            // Mapping OrderItem
            CreateMap<OrderItemDTO, OrderItem>();

            // Mapping Payment
            CreateMap<PaymentDTO, Payment>();

        }
    }
}
