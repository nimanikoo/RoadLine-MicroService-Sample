using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Road.Api.Data;
using Road.Api.Models;
using Road.Api.Services.Interfaces;

namespace Road.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IMessageProducer _messageProducer;
    private readonly AppDataContext _context;
    public BookingController(IMessageProducer messageProducer, AppDataContext context)
    {
        _messageProducer = messageProducer;
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateingBooking(Booking newBooking)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Bookings.Add(newBooking);
        _messageProducer.SendingMessage<Booking>(newBooking);
        return Ok();
    }
}
