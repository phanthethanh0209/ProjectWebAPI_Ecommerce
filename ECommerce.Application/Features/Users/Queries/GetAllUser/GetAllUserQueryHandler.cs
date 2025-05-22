using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, ResultResponse<PagedList<GetUserResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<PagedList<GetUserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await _unitOfWork.User.GetAllAsync(null);
            if (!users.Any()) return ResultResponse<PagedList<GetUserResponse>>.FailResponse("Users not found");

            IEnumerable<GetUserResponse> usersResponse = _mapper.Map<IEnumerable<GetUserResponse>>(users);
            PagedList<GetUserResponse> result = PagedList<GetUserResponse>.CreateAsync(usersResponse, request.pageNumber, request.pageSize);
            return ResultResponse<PagedList<GetUserResponse>>.SuccessResponse(result);
        }
    }
}
