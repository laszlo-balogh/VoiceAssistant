using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VoiceAssistant.Data;
using VoiceAssistant.Models;

namespace VoiceAssistant.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CommandController : ControllerBase
    {
        ICommandRepository commandRepository;
        public CommandController(ICommandRepository commandRepository)
        {
            this.commandRepository = commandRepository;
        }

        [HttpGet]
        public IEnumerable<CommandDataModel> GetResults()
        {
            return commandRepository.Commands;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Command command)
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");


            if (command != null)
            {
                Console.WriteLine($"Received command UID: {command.Id}, CommandString: {command.CommandSentense}");
                // do something with the command...
                return Ok("yoo");
            }
            else
            {
                return BadRequest("no yoo");
            }
        }
    }
}
