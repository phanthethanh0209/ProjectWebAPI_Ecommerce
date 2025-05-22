using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResultResponse<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<GetUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // check role admin or user
            Guid userId = _currentUserService.GetUserIdForClaims();
            if (!_currentUserService.IsInRole("Admin") && userId != request.Id)
                throw new ForbiddenAccessException();

            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (user == null) throw new NotFoundException(nameof(User), request.Id);

            GetUserResponse userResponse = _mapper.Map<GetUserResponse>(user);
            return ResultResponse<GetUserResponse>.SuccessResponse(userResponse);
        }
    }
}
