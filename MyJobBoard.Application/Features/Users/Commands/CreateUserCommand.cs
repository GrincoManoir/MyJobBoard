using MediatR;
using Microsoft.AspNetCore.Identity;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set;}
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
            };

            return await _userService.CreateUser(user, request.Password);

        }
    }
}
