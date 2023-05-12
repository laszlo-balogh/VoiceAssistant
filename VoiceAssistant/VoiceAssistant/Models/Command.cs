namespace VoiceAssistant.Models
{
    public class Command
    {
        public string? Id { get; set; }
        public string? CommandSentense { get; set; }

        public Command(string? id, string? commandSentense)
        {
            Id = id;
            CommandSentense = commandSentense;
        }
    }
}
