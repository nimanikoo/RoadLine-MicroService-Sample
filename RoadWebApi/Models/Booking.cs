using System.ComponentModel.DataAnnotations;

namespace Road.Api.Models;
public class Booking
{
    [Key]
    public int Id { get; set; }
    public string PassengerName { get; set; }
    public string PassportNum { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public int Status { get; set; }
}
