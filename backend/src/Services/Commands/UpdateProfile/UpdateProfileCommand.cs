using MediatR;
using System;

namespace Services.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
