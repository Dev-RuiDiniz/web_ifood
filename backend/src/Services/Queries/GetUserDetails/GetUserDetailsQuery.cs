using MediatR;
using Dtos.Users;
using System;

namespace Services.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailDto?>
    {
        public Guid Id { get; set; }

        public GetUserDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
}
