using MiniShop.Data;
using MiniShop.Models;
using MiniShop.Repositories;

namespace MiniShop
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var db = new ShopDBContext();

           
            if (!db.Tickets.Any())
            {
                db.Tickets.AddRange(
                    new Ticket { Id = 1, Name = "VIP Ticket", Price = 150m, Stock = 10 },
                    new Ticket { Id = 2, Name = "Standard Ticket", Price = 50m, Stock = 100 },
                    new Ticket { Id = 3, Name = "Student Ticket", Price = 25m, Stock = 50 }
                );
                await db.SaveChangesAsync();
            }

            ITicketRepository ticketRepo = new TicketRepository(db);

            Console.WriteLine("========================================");
            Console.WriteLine("  TICKET REPOSITORY - CRUD TEST");
            Console.WriteLine("========================================\n");

            
            Console.WriteLine("--- CREATE: Yeni ticket elave edilir ---");
            var newTicket = new Ticket
            {
                Name = "Premium Ticket",
                Price = 200m,
                Stock = 20
            };
            await ticketRepo.AddAsync(newTicket);
            Console.WriteLine($"Yeni ticket elave edildi: Id={newTicket.Id}, Name={newTicket.Name}, Price={newTicket.Price}, Stock={newTicket.Stock}");

            
            Console.WriteLine("\n--- READ: Butun ticketler ---");
            var allTickets = await ticketRepo.GetAllAsync();
            foreach (var t in allTickets)
            {
                Console.WriteLine($"  Id={t.Id}, Name={t.Name}, Price={t.Price}, Stock={t.Stock}");
            }

            
            Console.WriteLine($"\n--- READ: Id={newTicket.Id} olan ticket ---");
            var foundTicket = await ticketRepo.GetByIdAsync(newTicket.Id);
            if (foundTicket != null)
            {
                Console.WriteLine($"  Tapildi: Id={foundTicket.Id}, Name={foundTicket.Name}, Price={foundTicket.Price}, Stock={foundTicket.Stock}");
            }
            else
            {
                Console.WriteLine("  Ticket tapilmadi!");
            }

           
            Console.WriteLine($"\n--- UPDATE: Id={newTicket.Id} olan ticket yenilenir ---");
            if (foundTicket != null)
            {
                foundTicket.Name = "Premium Ticket (Updated)";
                foundTicket.Price = 250m;
                foundTicket.Stock = 15;
                await ticketRepo.UpdateAsync(foundTicket);
                Console.WriteLine($"  Yenilendi: Id={foundTicket.Id}, Name={foundTicket.Name}, Price={foundTicket.Price}, Stock={foundTicket.Stock}");
            }

            
            Console.WriteLine("\n--- READ: Movcud olan ticketler (Stock > 0) ---");
            var availableTickets = await ticketRepo.GetAvailableTicketsAsync();
            foreach (var t in availableTickets)
            {
                Console.WriteLine($"  Id={t.Id}, Name={t.Name}, Price={t.Price}, Stock={t.Stock}");
            }

            
            Console.WriteLine($"\n--- DELETE: Id={newTicket.Id} olan ticket silinir ---");
            await ticketRepo.DeleteAsync(newTicket.Id);
            Console.WriteLine($"  Ticket (Id={newTicket.Id}) silindi!");

            
            Console.WriteLine("\n--- Silinmeden sonra butun ticketler ---");
            allTickets = await ticketRepo.GetAllAsync();
            foreach (var t in allTickets)
            {
                Console.WriteLine($"  Id={t.Id}, Name={t.Name}, Price={t.Price}, Stock={t.Stock}");
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("  BUTUN CRUD EMELIYYATLARI UGURLA TAMAMLANDI!");
            Console.WriteLine("========================================");
        }
    }
}
