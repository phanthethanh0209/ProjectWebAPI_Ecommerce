using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<ResultResponse<Guid>>
    {
        //public OrderDTO Order { get; set; }
        public Guid UserId { get; set; }
        public string ShippingAddress { get; set; }
        //public string Status { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        //public PaymentDTO Payment { get; set; }
    }
}
