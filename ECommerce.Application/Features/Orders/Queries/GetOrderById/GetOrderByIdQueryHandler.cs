using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ResultResponse<OrderDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<OrderDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();
            Order? order = await _unitOfWork.Orders.GetOrderByIdWithItemsAsync(request.orderId);
            if (order == null || order.UserId != userId)
                throw new Exception("Order Id not found");

            OrderDTO data = _mapper.Map<OrderDTO>(order);
            return ResultResponse<OrderDTO>.SuccessResponse(data);
        }
    }
}
