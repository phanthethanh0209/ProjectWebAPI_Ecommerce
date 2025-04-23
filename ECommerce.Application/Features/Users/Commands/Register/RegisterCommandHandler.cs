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
            // hash password 
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.password);

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return ResultResponse<Guid>.SuccessResponse(user.Id);
        }
    }
}
