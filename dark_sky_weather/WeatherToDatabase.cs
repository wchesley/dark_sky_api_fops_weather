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
            //selects table based on city chosen. 
            var json_response = JsonConvert.DeserializeObject<WeatherJSON.RootObject>(raw_json_response);
            string sql_connection = "server location";
            string table_name = city;
            string sql_query;
            using (SqlConnection conn = new SqlConnection(sql_connection))
            {
                conn.ConnectionString = sql_connection; 
                // there are several lists in the response from Dark Sky, will need for loops
                //to iterate over all of em to insert data. 
                //will grab current weather, minutly, hourly, daily and any alerts. 
                sql_query = $"INSERT INTO {table_name}(";
                sql_query += "current_time, current_summary, current_nearestStormDistance, current_nearestStormBearing, ";
                sql_query += "current_precipIntensity, current_precipProbability, current_temperature, ";
                sql_query += "current_apparentTemperature, current_dewPoint, current_humidity, current_windSpeed, ";
                sql_query += "current_windGuest, current_windBearing, current_visibility, current_cloudCover, current_pressure, ";
                sql_query += "current_ozone, current_uvIndex, ";
                //end current class from Dark Sky class.
                 
                //End of Current Values from Dark Sky response

                sql_query += "minutely_time, minutely_precipIntensity, minutely_precipProbability, mintely_precipType, ";
                sql_query += "minutely_summary, ";
                //End of Minutely Values from Dark Sky response

                sql_query += "hourly_time, hourly_summary, hourly_precipIntensity, hourly_precipProbability, ";
                sql_query += "hourly_temperature, hourly_apparentTemperature, hourly_dewPoint, ";
                sql_query += "hourly_humidity, hourly_windSpeed, hourly_windGust, hourly_windBearing, ";
                sql_query += "hourly_visibility, hourly_cloudCover, hourly_pressure, hourly_ozone, hourly_uvIndex, ";
                sql_query += "hourly_precipType, hourly_summary, ";
                //End of hourly Values from Dark Sky response

                sql_query += "daily_time, daily_summary, daily_sunriseTime, daily_sunsetTime, daily_moonPhase, ";
                sql_query += "daily_precipIntensity, daily_precipIntensityMax, daily_precipIntensityMaxTime, ";
                sql_query += "daily_precipProbability, daily_precipType, daily_temperatureMin, ";
                sql_query += "daily_temperatureMinTime, daily_temperatureMax, daily_temperatureMaxTime, ";
                sql_query += "daily_apparentTemperatureMin, daily_apparentTemperatureMinTime, daily_apparentTemperatureMax, ";
                sql_query += "daily_apparentTemperatureMaxTime, daily_dewPoint, daily_humidity, daily_windSpeed, ";
                sql_query += "daily_windGust, daily_windBearing, daily_visibility, daily_cloudCover, daily_pressure, ";
                sql_query += "daily_ozone, daily_uvIndex, daily_uvIndexTime, daily_summary_no_list) ";
                //End Daily values from dark sky response

                //begin Values: 
                sql_query += "VALUES(";
                sql_query += "@current_time, @current_summary, @current_nearestStormDistance, @current_nearestStormBearing, ";
                sql_query += "@current_precipIntensity, @current_precipProbability, @current_temperature, ";
                sql_query += "@current_apparentTemperature, @current_dewPoint, @current_humidity, @current_windSpeed, ";
                sql_query += "@current_windGuest, @current_windBearing, @current_visibility, @current_cloudCover, ";
                sql_query += "@current_pressure, @current_ozone, @current_uvIndex, ";
                sql_query += "@minutely_time, @minutely_precipIntensity, @minutely_precipProbability, @minutely_precipType, @minutely_summary, ";
                //End of minutly values from Dark Sky response
                
                sql_query += "@hourly_time, @hourly_summary, @hourly_precipIntensity, @hourly_precipProbability, ";
                sql_query += "@hourly_temperature, @hourly_apparentTemperature, @hourly_dewPoint, @hourly_humidity, ";
                sql_query += "@hourly_windSpeed, @hourly_windGust, @hourly_windBearing, @hourly_visibility, ";
                sql_query += "@hourly_cloudCover, @hourly_pressure, @hourly_ozone, @hourly_uvIndex, @hourly_precipType, ";
                sql_query += "@hourly_summary, ";
                //End hourly values from Dark Sky response

                sql_query += "@daily_time, @daily_summary, @daily_sunriseTime, @daily_sunsetTime, ";
                sql_query += "@daily_moonPhase, @daily_precipIntensity, @daily_precipIntensityMax, ";
                sql_query += "@daily_precipIntensityMaxTime, @daily_precipProbability, @daily_precipType, ";
                sql_query += "@daily_temperatureMin, @daily_temperatureMinTime, @daily_temperatureMax, @daily_temperatureMaxTime, ";
                sql_query += "@daily_apparentTemperatureMin, @daily_apparentTemperatureMinTime, @daily_apparentTemperatureMax, ";
                sql_query += "@daily_apparentTemperatureMaxTime, @daily_dewPoint, @daily_humidity, @daily_windSpeed, ";
                sql_query += "@daily_windGust, @daily_windBearing, @daily_visibility, @daily_cloudCover, @daily_pressure, @daily_ozone, ";
                sql_query += "@daily_uvIndex, @daily_uvIndexTime, @daily_summary_no_list)";
                //End Daily Values from Dark sky response
                //end sql_query

                //Begin insert command: 
                using(SqlCommand Insert = new SqlCommand(sql_query, conn))
                {
                    //params outside of lists
                    Insert.Parameters.AddWithValue();
                }
            }  
        }
    }
}
