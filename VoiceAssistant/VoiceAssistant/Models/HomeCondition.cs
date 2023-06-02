using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
namespace VoiceAssistant.Models
{
    public class HomeCondition
    {
        public int Temperature { get; set; } = 16;
        public bool AirConditionerOn { get; set; } = false;
        public bool LightsOn { get; set; } = false;

        public bool TemperatureBasicSituation = true;

        private readonly IConfiguration config;

        public HomeCondition()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public void TurnLightsOn()
        {
            if (!this.LightsOn)
            {
                this.LightsOn = true;
            }
        }

        public void TurnLightsOff()
        {
            if (this.LightsOn)
            {
                this.LightsOn = false;
            }
        }

        public string TellAJoke()
        {
            return "haha";
        }

        public string WhatsTheTime()
        {
            var hour = DateTime.Now.Hour.ToString();
            var minute = DateTime.Now.Minute.ToString();
            
            return $"{hour}:{minute}";
            
        }

        public void TurnAirConditionerOn()
        {
            if (!this.AirConditionerOn)
            {
                this.AirConditionerOn = true;
            }
        }

        public void TurnAirConditionerOff()
        {
            if (this.AirConditionerOn)
            {
                this.AirConditionerOn = false;
            }
        }

        public void OpenBrowser()
        {
            string url = "https://www.google.com";
            Process.Start(url);
        }

        public void TemperatureToBasicSituation()
        {
            if (!this.TemperatureBasicSituation)
            {                
                var xmlDoc = new XmlDocument();
                var homeXMlPath = config.GetValue<string>("HomeXmlPath");
                xmlDoc.Load(homeXMlPath);

                var root = xmlDoc.DocumentElement;
                
                var temperatureElement = root.SelectSingleNode(nameof(this.Temperature));
                var airConditionerOnElement = root.SelectSingleNode(nameof(this.AirConditionerOn));
                var lightsOnElement = root.SelectSingleNode(nameof(this.LightsOn));
                var temperatureBasicSituationElement = root.SelectSingleNode(nameof(this.TemperatureBasicSituation));

                temperatureElement.InnerText = this.Temperature.ToString();
                airConditionerOnElement.InnerText = this.AirConditionerOn.ToString();
                lightsOnElement.InnerText = this.LightsOn.ToString();
                temperatureBasicSituationElement.InnerText = this.TemperatureBasicSituation.ToString();

                xmlDoc.Save(homeXMlPath);
                this.TemperatureBasicSituation = true;
                this.Temperature = 16;                
            }
        }

        public void ResetBackTemperature()
        {
            var xmlDoc = new XmlDocument();
            var homeXMlPath = config.GetValue<string>("HomeXmlPath");
            xmlDoc.Load(homeXMlPath);

            var root = xmlDoc.DocumentElement;

            var temperatureElement = root.SelectSingleNode(nameof(this.Temperature)).InnerText;
            var airConditionerOnElement = root.SelectSingleNode(nameof(this.AirConditionerOn)).InnerText;
            var lightsOnElement = root.SelectSingleNode(nameof(this.LightsOn)).InnerText;
            var temperatureBasicSituationElement = root.SelectSingleNode(nameof(this.TemperatureBasicSituation)).InnerText;

            this.TemperatureBasicSituation = bool.Parse(temperatureBasicSituationElement);
            this.Temperature = int.Parse(temperatureElement);
        }

        public void Warmer()
        {
            this.Temperature += 1;
        }

        public void Colder()
        {
            this.Temperature -= 1;
        }
    }
}
