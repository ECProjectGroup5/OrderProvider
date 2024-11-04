using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<OrderEntity, DataContext>
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }
    }
}
