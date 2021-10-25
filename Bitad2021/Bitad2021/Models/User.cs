#nullable enable
namespace Bitad2021.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int CurrentScore { get; set; }
        public Workshop Workshop { get; set; }
        public string AttendanceCode { get; set; }
        public string? WorkshopAttendanceCode { get; set; }
    }
}