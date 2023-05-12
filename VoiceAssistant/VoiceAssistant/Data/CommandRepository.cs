using System.Xml.Linq;
using VoiceAssistant.Models;

namespace VoiceAssistant.Data
{
    public class CommandRepository : ICommandRepository
    {
        public List<CommandDataModel>? Commands { get; set; }
        public CommandRepository()
        {
            Refresh();
        }

        public void Refresh()
        {
            Commands = new List<CommandDataModel>();
            var xdoc = XDocument.Load("Data/commands.xml");
            foreach (var item in xdoc.Descendants("Command"))
            {
                Commands.Add(new CommandDataModel()
                {
                    CommandRec = item.Value,
                    CommandCode = item.Attribute("toSend").Value
                });
            }
        }
    }
}
