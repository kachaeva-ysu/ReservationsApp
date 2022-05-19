using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IVillaHandler
    {
        public Task<IEnumerable<Villa>> GetVillasAsync(VillaFilterParameters parameters);
        public Task<VillaDetails> GetVillaDetailsAsync(int villaId);
        public Task AddVillaAsync(Villa villa);
        public Task DeleteVillaAsync(int villaId);

    }
}
