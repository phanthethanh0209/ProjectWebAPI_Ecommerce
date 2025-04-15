using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResultResponse<GetUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<GetUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (user == null) return ResultResponse<GetUserResponse>.FailResponse("User not found");

            GetUserResponse userResponse = _mapper.Map<GetUserResponse>(user);
            return ResultResponse<GetUserResponse>.SuccessResponse(userResponse);
        }
    }
}
