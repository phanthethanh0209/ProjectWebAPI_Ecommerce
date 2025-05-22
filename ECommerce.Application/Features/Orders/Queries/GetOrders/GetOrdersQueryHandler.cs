using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.QueryFilters;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace ECommerce.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ResultResponse<PagedList<OrderDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateFilterService _dateFilterService;

        public GetOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IDateFilterService dateFilterService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateFilterService = dateFilterService;
        }

        public async Task<ResultResponse<PagedList<OrderDTO>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Order, bool>>? status = null;
            if (request.status != null)
            {
                status = t => t.Status == request.status;
            }

            // lấy thgian cần lọc
            (DateTime? startDate, DateTime? endDate) = _dateFilterService.DateFilter(request.filterType, request.startDate, request.endDate);

            // get all orders
            IEnumerable<Order> Items = await _unitOfWork.Orders.GetOrdersAsync(request.pageNumber, request.pageSize,
                status, startDate, endDate);

            // map và trả kq với phân trang
            IEnumerable<OrderDTO> data = _mapper.Map<IEnumerable<OrderDTO>>(Items);
            PagedList<OrderDTO> result = PagedList<OrderDTO>.CreateAsync(data, request.pageNumber, request.pageSize);

            return ResultResponse<PagedList<OrderDTO>>.SuccessResponse(result);
        }
    }
}
