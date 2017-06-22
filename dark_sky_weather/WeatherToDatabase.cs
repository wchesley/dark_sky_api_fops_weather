using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json; 

namespace dark_sky_weather
{
    public class WeatherToDatabase
    {
        static void SaveWeather(string raw_json_response, string city)
        {
            //class will take raw response as string from dark sky
            //deserialize to WeatherJSON class 
            //saves all variables to Database. 
            var json_response = JsonConvert.DeserializeObject<WeatherJSON.RootObject>(raw_json_response);
            string sql_connection = "server location";
            string table_name = city;
            string sql_query;
            using (SqlConnection conn = new SqlConnection(sql_connection))
            {
                // there are several lists in the response from Dark Sky, will need for loops
                //to iterate over all of em to insert data. 
                //will grab current weather, minutly, hourly, daily and any alerts. 
                sql_query = $"INSERT INTO {table_name} ()";
                foreach(var hour in json_response.hourly.data)
                {

                }
            }  
        }
    }
}
