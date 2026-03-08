using MediatR;
using Domains.Entities;
using Domains.Enums;
using Repositories.Interfaces;
using Data.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.AddReview
{
    public class AddReviewHandler : IRequestHandler<AddReviewCommand, bool>
    {
        private readonly IBaseRepository<Review> _reviewRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        private readonly IUnitOfWork _uow;

        public AddReviewHandler(
            IBaseRepository<Review> reviewRepository, 
            IBaseRepository<Order> orderRepository,
            IBaseRepository<Restaurant> restaurantRepository,
            IUnitOfWork uow)
        {
            _reviewRepository = reviewRepository;
            _orderRepository = orderRepository;
            _restaurantRepository = restaurantRepository;
            _uow = uow;
        }

        public async Task<bool> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null || order.UserId != request.UserId) return false;
            if (order.Status != OrderStatus.Delivered.ToString()) return false;

            // Check duplicate
            var existing = await _reviewRepository.FindAsync(r => r.OrderId == request.OrderId);
            if (existing.Any()) return false;

            var review = new Review
            {
                OrderId = request.OrderId,
                UserId = request.UserId,
                RestaurantId = order.RestaurantId,
                Stars = request.Stars,
                Comment = request.Comment
            };

            await _reviewRepository.AddAsync(review);

            // Update Restaurant Average
            var restaurant = await _restaurantRepository.GetByIdAsync(order.RestaurantId);
            if (restaurant != null)
            {
                double totalStars = (restaurant.AverageRating * restaurant.ReviewCount) + request.Stars;
                restaurant.ReviewCount++;
                restaurant.AverageRating = totalStars / restaurant.ReviewCount;
                await _restaurantRepository.UpdateAsync(restaurant);
            }

            return await _uow.CommitAsync();
        }
    }
}
