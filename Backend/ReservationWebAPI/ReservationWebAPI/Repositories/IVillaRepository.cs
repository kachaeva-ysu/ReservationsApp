using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IVillaRepository
    {
        public Task<IEnumerable<Villa>> GetVillasAsync();
        public Task<Villa> GetVillaAsync(int villaId);
        public Task AddVillaAsync(Villa villa);
        public Task DeleteVillaAsync(Villa villa);
    }
}
