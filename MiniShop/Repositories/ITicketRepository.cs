using MiniShop.Models;

namespace MiniShop.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<List<Ticket>> GetAvailableTicketsAsync();
    }
}
