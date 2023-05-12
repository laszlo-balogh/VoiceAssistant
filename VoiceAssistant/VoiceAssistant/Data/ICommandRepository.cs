using VoiceAssistant.Models;

namespace VoiceAssistant.Data
{
    public interface ICommandRepository
    {
        List<CommandDataModel>? Commands { get; set; }

        void Refresh();
    }
}