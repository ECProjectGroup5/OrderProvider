using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class OrderRepository(DataContext context) : BaseRepository<OrderEntity, DataContext>(context)
{

}
