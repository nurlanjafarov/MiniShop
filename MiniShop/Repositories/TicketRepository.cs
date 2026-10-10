using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Models;

namespace MiniShop.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ShopDBContext context) : base(context)
        {
        }

        public async Task<List<Ticket>> GetAvailableTicketsAsync()
        {
            return await _dbSet.Where(t => t.Stock > 0).ToListAsync();
        }
    }
}
