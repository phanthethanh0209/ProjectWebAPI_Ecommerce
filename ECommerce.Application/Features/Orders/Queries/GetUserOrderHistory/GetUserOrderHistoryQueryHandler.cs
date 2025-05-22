using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Features.Orders.Queries.GetUserOrderHistory;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace ECommerce.Application.Features.Orders.Queries.GetOrders
{
    public class GetUserOrderHistoryQueryHandler : IRequestHandler<GetUserOrderHistoryQuery, ResultResponse<PagedList<OrderDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUserOrderHistoryQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<ResultResponse<PagedList<OrderDTO>>> Handle(GetUserOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();

            Expression<Func<Order, bool>>? filter = null;
            if (request.status != null)
            {
                filter = t => t.Status == request.status;
            }

            IEnumerable<Order> orders = await _unitOfWork.Orders.GetUserOrdersAsync(userId, request.pageNumber, request.pageSize, filter: filter);
            IEnumerable<OrderDTO> data = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            PagedList<OrderDTO> result = PagedList<OrderDTO>.CreateAsync(data, request.pageNumber, request.pageSize);

            return ResultResponse<PagedList<OrderDTO>>.SuccessResponse(result);
        }
    }
}
