using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (user == null) return ResultResponse<Unit>.FailResponse("User not found");

            await _unitOfWork.User.Delete(user);
            await _unitOfWork.SaveChangesAsync();
            return ResultResponse<Unit>.SuccessResponse(Unit.Value);
        }
    }
}
