using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, ResultResponse<IEnumerable<GetUserResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<IEnumerable<GetUserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await _unitOfWork.User.GetAllAsync(null, request.pageNummber, request.limit);
            if (!users.Any()) return ResultResponse<IEnumerable<GetUserResponse>>.FailResponse("Users not found");

            IEnumerable<GetUserResponse> usersResponse = _mapper.Map<IEnumerable<GetUserResponse>>(users);
            return ResultResponse<IEnumerable<GetUserResponse>>.SuccessResponse(usersResponse);
        }
    }
}
