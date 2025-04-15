using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (user == null) return ResultResponse<Guid>.FailResponse("User not found");

            _mapper.Map(request, user);
            await _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return ResultResponse<Guid>.SuccessResponse(user.Id);
        }
    }
}
