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
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                User user = _mapper.Map<User>(request);
                // hash password 
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.password);
                await _unitOfWork.User.AddAsync(user);

                Cart cart = new()
                {
                    UserId = user.Id,
                    //TotalAmount = 0
                };
                await _unitOfWork.Carts.AddAsync(cart);

                await _unitOfWork.CommitTransactionAsync();
                return ResultResponse<Guid>.SuccessResponse(user.Id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Failed to create user and cart", ex);
            }
        }
    }
}
