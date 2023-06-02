using Microsoft.AspNetCore.Mvc;
using VoiceAssistant.Models;

namespace VoiceAssistant.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceiveCommand([FromBody] Command command)
        {
            if (command != null)
            {
                Console.WriteLine($"Received command in HomeController  UID: {command.Id}, CommandString: {command.CommandSentense}");
                // Az adatok feldolgozása vagy további logika végrehajtása itt...
                return Ok("Received command successfully.");
            }
            else
            {
                return BadRequest("Invalid command data.");
            }
        }
    }
}
