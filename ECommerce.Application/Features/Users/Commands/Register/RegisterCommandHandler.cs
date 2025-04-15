using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);
            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveChangeAsync();

            return ResultResponse<Guid>.SuccessResponse(user.Id);
        }
    }
}
