namespace Bitad2021.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int CurrentScore { get; set; }
        public Workshop Workshop { get; set; }
        public string AttendanceCode { get; set; }
    }
}