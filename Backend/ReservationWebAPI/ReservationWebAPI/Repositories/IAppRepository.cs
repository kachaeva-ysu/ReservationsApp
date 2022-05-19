using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IAppRepository: IVillaRepository, IUserRepository, IReservationRepository
    {
        public void Dispose();
        //public ValueTask DisposeAsync();
    }
}
