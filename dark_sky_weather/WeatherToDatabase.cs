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
                //end of Current Values from dark sky response 

                sql_query += "@minutely_time, @minutely_precipIntensity, @minutely_precipProbability, @minutely_precipType, @minutely_summary, ";
                //End of minutly values from Dark Sky response
                
                sql_query += "@hourly_time, @hourly_summary, @hourly_precipIntensity, @hourly_precipProbability, ";
                sql_query += "@hourly_temperature, @hourly_apparentTemperature, @hourly_dewPoint, @hourly_humidity, ";
                sql_query += "@hourly_windSpeed, @hourly_windGust, @hourly_windBearing, @hourly_visibility, ";
                sql_query += "@hourly_cloudCover, @hourly_pressure, @hourly_ozone, @hourly_uvIndex, @hourly_precipType, ";
                sql_query += "@hourly_summary_no_list, ";
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
                    
                    //current values: 
                    Insert.Parameters.AddWithValue("@current_time", json_response.currently.time);
                    Insert.Parameters.AddWithValue("@current_summary", json_response.currently.summary);
                    Insert.Parameters.AddWithValue("@current_nearestStormDistance", json_response.currently.nearestStormDistance);
                    Insert.Parameters.AddWithValue("@current_nearestStormBearing", json_response.currently.nearestStormBearing);
                    Insert.Parameters.AddWithValue("@current_precipIntensity", json_response.currently.precipIntensity);
                    Insert.Parameters.AddWithValue("@current_precipProbability", json_response.currently.precipProbability);
                    Insert.Parameters.AddWithValue("@current_temperature", json_response.currently.temperature);
                    Insert.Parameters.AddWithValue("@current_apparentTemperature", json_response.currently.apparentTemperature);
                    Insert.Parameters.AddWithValue("@current_dewPoint", json_response.currently.dewPoint);
                    Insert.Parameters.AddWithValue("@current_humidity", json_response.currently.humidity);
                    Insert.Parameters.AddWithValue("@current_windSpeed", json_response.currently.windSpeed);
                    Insert.Parameters.AddWithValue("@current_windGuest", json_response.currently.windGust);
                    Insert.Parameters.AddWithValue("@current_windBearing", json_response.currently.windBearing);
                    Insert.Parameters.AddWithValue("@current_visibility", json_response.currently.visibility);
                    Insert.Parameters.AddWithValue("@current_pressure", json_response.currently.pressure);
                    Insert.Parameters.AddWithValue("@current_cloudCover", json_response.currently.cloudCover);
                    Insert.Parameters.AddWithValue("@current_ozone", json_response.currently.ozone);
                    Insert.Parameters.AddWithValue("@current_uvIndex", json_response.currently.uvIndex);


                    //cannot store list in SQL, will concantinate strings
                    //together to store all values.
                    string minute_time = string.Empty; 
                    string minute_precipIntensity = string.Empty;
                    string minute_precipProbability = string.Empty;
                    string minute_precipType = string.Empty; 
                    //minutely values: 
                    
                    foreach(var minute in json_response.Minutely.data)
                    {
                        minute_time += json_response.Minutely.data.time.ToString() + ", ";
                        minute_precipIntensity += json_response.Minutely.data.precipIntensity.ToString() + ", ";
                        minute_precipProbability += json_response.Minutely.data.precipProbability.ToString() + ", ";
                        minute_precipType += json_response.Minutely.data.precipType + ", ";
                    }
                    Insert.Parameters.AddWithValue("@minutely_time", minute_time);
                    Insert.Parameters.AddWithValue("@minutely_precipIntensity", minute_precipIntensity);
                    Insert.Parameters.AddWithValue("@minutely_precipProbability", minute_precipProbability);
                    Insert.Parameters.AddWithValue("@minutely_precipType", minute_precipType);
                    Insert.Parameters.AddWithValue("@minutely_summary", json_response.Minutely.summary);

                    //Hourly block: 
                    string hourly_time = string.Empty;
                    string hourly_summary = string.Empty;
                    string hourly_precipIntensity = string.Empty;
                    string hourly_precipProbability = string.Empty;
                    string hourly_temperature = string.Empty;
                    string hourly_apparentTemperature = string.Empty;
                    string hourly_dewPoint = string.Empty;
                    string hourly_humidity = string.Empty;
                    string hourly_windSpeed = string.Empty;
                    string hourly_windGust = string.Empty;
                    string hourly_windBearing = string.Empty;
                    string hourly_visibility = string.Empty;
                    string hourly_cloudCover = string.Empty;
                    string hourly_pressure = string.Empty;
                    string hourly_ozone = string.Empty;
                    string hourly_uvIndex = string.Empty;
                    string hourly_precipType = string.Empty;

                    foreach(var hour in json_response.Hourly.data)
                    {
                        hourly_time += json_response.Hourly.data.time.ToString() + ", ";
                        hourly_summary += json_response.Hourly.data.summary + ", ";
                        hourly_precipIntensity += json_response.Hourly.data.precipIntensity.ToString() + ", ";
                        hourly_precipProbability += json_response.Hourly.data.precipProbability.ToString() + ", ";
                        hourly_temperature += json_response.Hourly.data.temperature.ToString() + ", ";
                        hourly_apparentTemperature += json_response.Hourly.data.apparentTemperature.ToString() + ", ";
                        hourly_dewPoint += json_response.Hourly.data.dewPoint.ToString() + ", ";
                        hourly_humidity += json_response.Hourly.data.humidity.ToString() + ", ";
                        hourly_windSpeed += json_response.Hourly.data.windSpeed.ToString() + ", ";
                        hourly_windGust += json_response.Hourly.data.windGust.ToString() + ", ";
                        hourly_windBearing += json_response.Hourly.data.windBearing.ToString() + ", ";
                        hourly_visibility += json_response.Hourly.data.visibility.ToString() + ", ";
                        hourly_cloudCover += json_response.Hourly.data.cloudCover.ToString() + ", ";
                        hourly_pressure += json_response.Hourly.data.pressure.ToString() + ", ";
                        hourly_ozone += json_response.Hourly.data.ozone.ToString() + ", ";
                        hourly_uvIndex += json_response.Hourly.data.uvIndex.ToString() + ", ";
                        hourly_precipType += json_response.Hourly.data.precipType + ", ";
                    }
                    Insert.Parameters.AddWithValue("@hourly_time", hourly_time);
                    Insert.Parameters.AddWithValue("@hourly_summary", hourly_summary);
                    Insert.Parameters.AddWithValue("@hourly_precipIntensity", hourly_precipIntensity);
                    Insert.Parameters.AddWithValue("@hourly_precipProbability", hourly_precipProbability);
                    Insert.Parameters.AddWithValue("@hourly_temperature", hourly_temperature);
                    Insert.Parameters.AddWithValue("@hourly_apparentTemperature", hourly_apparentTemperature;
                    Insert.Parameters.AddWithValue("@hourly_dewPoint", hourly_dewPoint);
                    Insert.Parameters.AddWithValue("@hourly_humidity", hourly_humidity);
                    Insert.Parameters.AddWithValue("@hourly_windSpeed", hourly_windSpeed);
                    Insert.Parameters.AddWithValue("@hourly_windGust", hourly_windGust);
                    Insert.Parameters.AddWithValue("@hourly_windBearing", hourly_windBearing);
                    Insert.Parameters.AddWithValue("@hourly_visibility", hourly_visibility);
                    Insert.Parameters.AddWithValue("@hourly_cloudCover", hourly_cloudCover);
                    Insert.Parameters.AddWithValue("@hourly_pressure", hourly_pressure);
                    Insert.Parameters.AddWithValue("@hourly_ozone", hourly_ozone);
                    Insert.Parameters.AddWithValue("@hourly_uvIndex", hourly_uvIndex);
                    Insert.Parameters.AddWithValue("@hourly_precipType", hourly_precipType);
                    Insert.Parameters.AddWithValue("@hourly_summary_no_list", json_response.Hourly.summary);

                    
                }
            }  
        }
    }
}
