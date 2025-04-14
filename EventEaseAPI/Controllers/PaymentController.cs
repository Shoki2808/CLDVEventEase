using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PaymentController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [Route("GetAllPayments")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
        {
            try
            {
                var payments = await context.Payments.Where(e => e.IsActive).ToListAsync();
                if (payments.Count == 0)
                {
                    return NotFound("No active payments found.");
                }

                return Ok(payments);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching payments.\n{e.Message}");
            }
        }


        [HttpGet]
        [Route("GetPaymentById")]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            try
            {
                var payment = await context.Payments.FirstOrDefaultAsync(e => e.PaymentId == id);

                if (!await PaymentExists(id))
                {
                    return NotFound($"The Payment with Payment ID {id} does not exist");
                }
                else if (!payment.IsActive)
                {
                    return NotFound($"No Payment found for Payment ID {id}");
                }


                return Ok(payment);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching the Payment specified. Payment ID {id}\n{e.Message}");
            }

        }


        [HttpPost]
        [Route("CreatePayment")]
        public async Task<ActionResult<Payment>> CreatePayment([FromBody] Payment payment)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var bookingExists = await context.Bookings.AnyAsync(b => b.BookingId == payment.BookingId && b.IsActive);

                if (!bookingExists)
                {
                    return BadRequest($"The specified booking with ID ({payment.BookingId}) does not exist.");
                }

                payment.IsActive = true;
                context.Payments.Add(payment);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentId }, payment);
            }
            catch (DbUpdateException dbue)
            {
                // Logs can be added here
                return StatusCode(500, $"An error occurred while saving the Payment to the database.\n{dbue.Message}");
            }
            catch (Exception e)
            {
                // Logs can be added here
                return StatusCode(500, $"An unexpected error occurred. Please try again later.\n{e.Message}");
            }
        }


        [HttpPut]
        [Route("UpdatePayment")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
        {
            try
            {

                if (id != payment.PaymentId)
                {
                    return BadRequest("Payment ID mismatch.");
                }
                else if (!await PaymentExists(id))
                {
                    return NotFound($"The Payment ID specified does not exist. Payment ID: {id}");
                }
                else if (!payment.IsActive)
                {
                    return NotFound($"No Active Payments found for ID: {id}");
                }

                var bookingExists = await context.Bookings.AnyAsync(b => b.BookingId == payment.BookingId && b.IsActive);

                if (!bookingExists)
                {
                    return BadRequest($"The specified booking with ID ({payment.BookingId}) does not exist.");
                }

                context.Entry(payment).State = EntityState.Modified;
                await context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException dbce)
            {
                return StatusCode(500, $"Concurrency error while updating.\n{dbce.Message}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An unxpcected error occurred.\n{e.Message}");
            }

            return NoContent();
        }


        [HttpDelete]
        [Route("DeletePayment")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var payment = await context.Events.FindAsync(id);
                bool hasBookings = await context.Bookings.AnyAsync(b => b.EventId == id);

                if (!await PaymentExists(id) || payment == null)
                {
                    return NotFound($"The Payment ID specified does not exist.Payment ID: {id}");
                }
                else if (!payment.IsActive)
                {
                    return NotFound($"No Active Payments found for ID: {id}");
                }
                else if (hasBookings)
                {
                    return BadRequest("Cannot delete Payment with existing bookings.");
                }

                payment.IsActive = false;
                context.Update(payment);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                return StatusCode(500, $"Concurrency error while updating.\n{dbce.Message}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An unxpcected error occurred.\n{e.Message}");

            }
        }

        private async Task<bool> PaymentExists(int id)
        {
            return await context.Payments.AnyAsync(p => p.PaymentId == id);
        }
    }
}
