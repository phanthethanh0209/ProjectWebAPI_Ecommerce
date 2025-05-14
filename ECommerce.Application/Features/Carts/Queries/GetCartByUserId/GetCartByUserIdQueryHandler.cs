using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Carts.DTOs;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Carts.Queries.GetCartByUserId
{
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, ResultResponse<CartDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetCartByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<CartDTO>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();

            Cart? cart = await _unitOfWork.Carts.GetCartByUserIdWithItemsAsync(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }

            CartDTO data = _mapper.Map<CartDTO>(cart);
            return ResultResponse<CartDTO>.SuccessResponse(data);
        }
    }
}
