using MediatR;
using System;

namespace Services.Commands.AddReview
{
    public class AddReviewCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public int Stars { get; set; }
        public string? Comment { get; set; }
    }
}
