using AutoMapper;
using ECommerce.Application.Features.Carts.Commands.AddToCart;
using ECommerce.Application.Features.Carts.Commands.UpdateCart;
using ECommerce.Application.Features.Carts.DTOs;
using ECommerce.Application.Features.Categories.Commands.CreateCategory;
using ECommerce.Application.Features.Categories.Commands.UpdateCategory;
using ECommerce.Application.Features.Categories.DTOs;
using ECommerce.Application.Features.Coupons.Commands.CreateCoupon;
using ECommerce.Application.Features.Coupons.DTOs;
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

            // Mapping category
            CreateMap<Category, GetCategoryResponse>();
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();

            // Mapping User
            CreateMap<RegisterCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, GetUserResponse>();

            // Mapping Order
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(t => t.UpdateAt, opt => opt.Ignore());
            CreateMap<Order, OrderDTO>();

            // Mapping OrderItem
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<OrderItem, OrderItems>();

            // Mapping Payment
            CreateMap<PaymentDTO, Payment>();

            // Mapping Cart
            CreateMap<AddToCartCommand, CartItem>().ReverseMap();
            CreateMap<Cart, CartDTO>();
            CreateMap<CartItem, CartItemDTO>();
            CreateMap<UpdateCartCommand, CartItem>();

            // Mapping Coupon
            CreateMap<CreateCouponCommand, Coupon>()
                .ForMember(t => t.UpdatedAt, opt => opt.Ignore());

            // Mapping Product Coupon
            CreateMap<Coupon, CouponResponse>()
                .ForMember(dest => dest.Products, opt =>
                opt.MapFrom(src => src.Product_Coupons.Select(cp => cp.Product)));
            CreateMap<Product_Coupons, ProductDTO>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
