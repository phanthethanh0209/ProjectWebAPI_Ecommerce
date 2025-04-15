using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<ResultResponse<Guid>>
    {
        public UpdateUserCommand(Guid id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


    }
}
