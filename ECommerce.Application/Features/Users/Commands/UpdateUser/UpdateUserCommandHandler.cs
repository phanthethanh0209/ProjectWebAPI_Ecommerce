using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();
            if (!_currentUserService.IsInRole("Admin") && userId != request.Id)
                throw new ForbiddenAccessException();

            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (user == null) return ResultResponse<Guid>.FailResponse("User not found");

            if (user.Email != request.Email)
            {
                bool rs = await _unitOfWork.User.AnyAsync(u => u.Email == request.Email);
                if (rs) return ResultResponse<Guid>.FailResponse("Email already exist");
            }

            _mapper.Map(request, user);
            await _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return ResultResponse<Guid>.SuccessResponse(user.Id);
        }
    }
}
