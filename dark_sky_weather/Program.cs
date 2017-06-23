using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO; 

namespace dark_sky_weather
{
    class Program
    {
        static void Main(string[] args)
        {
            //variable to deserialize json_config file. 
            JSON_Config.RootObject json_config = new JSON_Config.RootObject();
            
            string file_location;
            //file location, currenlty in bin/debug. 
            string file = "dark_sky_weather_config.json";
            
            //Read file and deserialize json_config into JSON_Config class. 
            file_location = File.ReadAllText(file);
            json_config = JsonConvert.DeserializeObject<JSON_Config.RootObject>(file_location);

            //variable for bland api address (address - latitude and longitude
            var api = json_config.Api.URL;
            foreach (var item in json_config.cities)
            {
                //iterates over all cities in the json_config Cities List<>
                //pulls weather from Dark Sky API. 
                string city_name = item.city;
                string location = item.location;
                using (var client = new WebClient())
                {
                    string apiAddr = api + location; 
                    var result = client.DownloadString(apiAddr);
                    Console.WriteLine(result.ToString());
                    var weather_response = JsonConvert.DeserializeObject<WeatherJSON.RootObject>(result);
                    Console.WriteLine($"Results for: {city_name}, {location}, URL: {api}");
                    Console.WriteLine(weather_response.currently.temperature.ToString() + "F");
                    Console.WriteLine(weather_response.longitude.ToString());
                    Console.WriteLine(weather_response.latitude.ToString());
                }
            }
        }
    }
}
