using System;

namespace TournamentPlanner
{
    public class EmailOptions
    {
        public const string Settings = "EmailSettings";

        public string Address { get; set; } = String.Empty;
        public string Smtp { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public string Password { get; set; } = String.Empty;
        public string SenderName { get; set; } = String.Empty;
    }
}
