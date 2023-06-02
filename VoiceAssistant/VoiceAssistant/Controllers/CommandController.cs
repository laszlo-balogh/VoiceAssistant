using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using VoiceAssistant.Data;
using VoiceAssistant.Models;

namespace VoiceAssistant.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandRepository commandRepository;
        private readonly HttpClient _httpClient;
        public HomeCondition HomeCondition { get; set; }
        public CommandController(ICommandRepository commandRepository, IHttpClientFactory httpClientFactory)
        {
            this.commandRepository = commandRepository;
            _httpClient = httpClientFactory.CreateClient();
            HomeCondition = new HomeCondition();
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
                //switch (command.CommandSentense)
                //{
                //    case "bongeszo":
                //        HomeCondition.OpenBrowser();
                //        break;
                //    case "fazok":
                //        HomeCondition.Warmer();
                //        break;
                //    case "melegem_van":
                //        HomeCondition.Colder();
                //        break;
                //    case "homerseklet_alap":
                //        HomeCondition.TemperatureToBasicSituation();
                //        break;
                //    case "homerseklet_vissza":
                //        HomeCondition.ResetBackTemperature();
                //        break;
                //    case "klima_be":
                //        HomeCondition.TurnAirConditionerOn();
                //        break;
                //    case "klima_le":
                //        HomeCondition.TurnAirConditionerOff();
                //        break;
                //    case "mennyi_az_ido":
                //        HomeCondition.WhatsTheTime();
                //        break;
                //    case "vicc":
                //        HomeCondition.TellAJoke();
                //        break;
                //    case "villany_fel":
                //        HomeCondition.TurnLightsOn();
                //        break;
                //    case "villany_le":
                //        HomeCondition.TurnLightsOff();
                //        break;
                //    case "nincs_match":
                //        break;
                //    default:
                //        break;
                //}

                //magának a HOmeConditionnek a továbbküldése jsonben

                return Ok("yoo");


            }
            else
            {
                return BadRequest("no yoo");
            }
        }





    }
}
