using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class AppContext : DbContext, IAppRepository
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        private DbSet<Reservation> Reservations { get; set; }
        private DbSet<User> Users { get; set; }
        private DbSet<Villa> Villas { get; set; }

        public async Task<IEnumerable<Villa>> GetVillasAsync()
        {
            return await Villas.ToListAsync();
        }

        public async Task<Villa> GetVillaAsync(int villaId)
        {
            return await Villas.FindAsync(villaId);
        }

        public async Task AddVillaAsync(Villa villa)
        {
            Villas.Add(villa);
            await SaveChangesAsync();
        }

        public async Task DeleteVillaAsync(Villa villa)
        {
            Villas.Remove(villa);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await Reservations.ToListAsync();
        }

        

        public async Task<Reservation> GetReservationAsync(int reservationId)
        {
            return await Reservations.FindAsync(reservationId);
        }
        public async Task<IEnumerable<UsersReservation>> GetUsersReservationsAsync(int userId)
        {
            return await Reservations.Where(r => r.UserId == userId).OrderByDescending(r => r.StartDate)
                .Join(Villas, r => r.VillaId, v => v.Id, (r, v) => new UsersReservation(r.Id, r.StartDate, r.EndDate, v.Id, v.Name, v.PriceForDay))
                .ToListAsync();
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            Reservations.Add(reservation);
            await SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            Reservations.Remove(reservation);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await Users.FindAsync(userId);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await Users.FirstAsync(user => user.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            Users.Add(user);
            await SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            Users.Remove(user);
            await SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            Users.Update(user);
            await SaveChangesAsync();
        }
    }
}
