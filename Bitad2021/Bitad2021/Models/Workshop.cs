using System;

namespace Bitad2021.Models
{
    public class Workshop
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantsNumber { get; set; }
        public string Room { get; set; }
        public Speaker Speaker { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Code { get; set; }
        public int MaxParticipants { get; set; }
    }
}