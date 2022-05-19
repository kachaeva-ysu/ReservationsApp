using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReservationWebAPI
{
    [ApiController]
    [Route("villas")]
    public class VillaController : ControllerBase
    {
        private readonly IVillaHandler _villaHandler;

        public VillaController(IVillaHandler villaHandler)
        {
            _villaHandler = villaHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Villa>>> GetVillasAsync([FromQuery] VillaFilterParameters parameters)
        {
            return Ok(await _villaHandler.GetVillasAsync(parameters));
        }

        [HttpGet("{villaId}")]
        public async Task<ActionResult<VillaDetails>> GetVillaDetailsAsync(int villaId)
        {
            return Ok(await _villaHandler.GetVillaDetailsAsync(villaId));
        }

        [HttpPost]
        public async Task<IActionResult> AddVillaAsync([FromBody] Villa villa)
        {
            await _villaHandler.AddVillaAsync(villa);
            return CreatedAtAction(nameof(GetVillaDetailsAsync), new { villaId = villa.Id }, villa);
        }

        [HttpDelete("{villaId}")]
        public async Task<IActionResult> DeleteVillaAsync(int villaId)
        {
            await _villaHandler.DeleteVillaAsync(villaId);
            return NoContent();
        }
    }
}
