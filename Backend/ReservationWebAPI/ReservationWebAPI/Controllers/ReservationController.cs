using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationHandler _reservationHandler;

        public ReservationController(IReservationHandler reservationHandler)
        {
            _reservationHandler = reservationHandler;
        }

        [TokenAuthorizationFilter]
        [HttpGet("{reservationId}")]
        public async Task<ActionResult<Reservation>> GetReservationAsync(int reservationId)
        {
            return Ok(await _reservationHandler.GetReservationAsync(reservationId));
        }

        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync([FromBody] Reservation reservation)
        {
            await _reservationHandler.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservationAsync), new { reservationId = reservation.Id }, reservation);
        }

        [TokenAuthorizationFilter]
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservationAsync(int reservationId)
        {
            await _reservationHandler.DeleteReservationAsync(reservationId);
            return NoContent();
        }
    }
}