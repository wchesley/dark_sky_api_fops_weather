using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json; 

/* 
Written By: Walker Chesley
Date: 06/22/2017
 */

namespace dark_sky_weather
{
    public class WeatherToDatabase
    {
        //TODO: setup database table and test! 
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
                    
                    foreach(var minute in json_response.minutely.data)
                    {
                        minute_time += minute.time.ToString() + ", ";
                        minute_precipIntensity += minute.precipIntensity.ToString() + ", ";
                        minute_precipProbability += minute.precipProbability.ToString() + ", ";
                        minute_precipType += minute.precipType + ", ";
                    }
                    Insert.Parameters.AddWithValue("@minutely_time", minute_time);
                    Insert.Parameters.AddWithValue("@minutely_precipIntensity", minute_precipIntensity);
                    Insert.Parameters.AddWithValue("@minutely_precipProbability", minute_precipProbability);
                    Insert.Parameters.AddWithValue("@minutely_precipType", minute_precipType);
                    Insert.Parameters.AddWithValue("@minutely_summary", json_response.minutely.summary);

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

                    foreach(var hour in json_response.hourly.data)
                    {
                        hourly_time += hour.time.ToString() + ", ";
                        hourly_summary += hour.summary + ", ";
                        hourly_precipIntensity += hour.precipIntensity.ToString() + ", ";
                        hourly_precipProbability += hour.precipProbability.ToString() + ", ";
                        hourly_temperature += hour.temperature.ToString() + ", ";
                        hourly_apparentTemperature += hour.apparentTemperature.ToString() + ", ";
                        hourly_dewPoint += hour.dewPoint.ToString() + ", ";
                        hourly_humidity += hour.humidity.ToString() + ", ";
                        hourly_windSpeed += hour.windSpeed.ToString() + ", ";
                        hourly_windGust += hour.windGust.ToString() + ", ";
                        hourly_windBearing += hour.windBearing.ToString() + ", ";
                        hourly_visibility += hour.visibility.ToString() + ", ";
                        hourly_cloudCover += hour.cloudCover.ToString() + ", ";
                        hourly_pressure += hour.pressure.ToString() + ", ";
                        hourly_ozone += hour.ozone.ToString() + ", ";
                        hourly_uvIndex += hour.uvIndex.ToString() + ", ";
                        hourly_precipType += hour.precipType + ", ";
                    }
                    Insert.Parameters.AddWithValue("@hourly_time", hourly_time);
                    Insert.Parameters.AddWithValue("@hourly_summary", hourly_summary);
                    Insert.Parameters.AddWithValue("@hourly_precipIntensity", hourly_precipIntensity);
                    Insert.Parameters.AddWithValue("@hourly_precipProbability", hourly_precipProbability);
                    Insert.Parameters.AddWithValue("@hourly_temperature", hourly_temperature);
                    Insert.Parameters.AddWithValue("@hourly_apparentTemperature", hourly_apparentTemperature);
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
                    Insert.Parameters.AddWithValue("@hourly_summary_no_list", json_response.hourly.summary);

                    //Daily block: 
                    string daily_time = string.Empty;
                    string daily_summary = string.Empty;
                    string daily_sunriseTime = string.Empty;
                    string daily_sunsetTime = string.Empty;
                    string daily_moonPhase = string.Empty;
                    string daily_precipIntensity = string.Empty;
                    string daily_precipIntensityMax = string.Empty;
                    string daily_precipIntensityMaxTime = string.Empty;
                    string daily_precipProbability = string.Empty;
                    string daily_precipType = string.Empty;
                    string daily_temperatureMin = string.Empty;
                    string daily_temperatureMinTime = string.Empty;
                    string daily_temperatureMax = string.Empty;
                    string daily_temperatureMaxTime = string.Empty;
                    string daily_apparentTemperatureMin = string.Empty;
                    string daily_apparentTemperatureMinTime = string.Empty;
                    string daily_apparentTemperatureMax = string.Empty;
                    string daily_apparentTemperatureMaxTime = string.Empty;
                    string daily_dewPoint = string.Empty;
                    string daily_humidity = string.Empty;
                    string daily_windSpeed = string.Empty;
                    string daily_windGust = string.Empty;
                    string daily_windBearing = string.Empty;
                    string daily_visibility = string.Empty;
                    string daily_cloudCover = string.Empty;
                    string daily_pressure = string.Empty;
                    string daily_ozone = string.Empty;
                    string daily_uvIndex = string.Empty;
                    string daily_uvIndexTime = string.Empty;
                    
                    foreach(var day in json_response.daily.data)
                    {
                        daily_time += day.time.ToString() + ", ";
                        daily_summary += day.summary;
                        daily_sunriseTime += day.sunriseTime.ToString() + ", ";
                        daily_sunsetTime += day.sunsetTime.ToString() + ", ";
                        daily_moonPhase += day.moonPhase.ToString() + ", ";
                        daily_precipIntensity += day.precipIntensity.ToString() + ", ";
                        daily_precipIntensityMax += day.precipIntensityMax.ToString() + ", ";
                        daily_precipIntensityMaxTime += day.precipIntensityMaxTime.ToString() + ", ";
                        daily_precipProbability += day.precipProbability.ToString() + ", ";
                        daily_precipType += day.precipType + ", ";
                        daily_temperatureMin += day.temperatureMin.ToString() + ", ";
                        daily_temperatureMinTime += day.temperatureMinTime.ToString() + ", ";
                        daily_temperatureMax += day.temperatureMax.ToString() + ", ";
                        daily_temperatureMaxTime += day.temperatureMaxTime.ToString() + ", ";
                        daily_apparentTemperatureMin += day.apparentTemperatureMin.ToString() + ", ";
                        daily_apparentTemperatureMinTime += day.apparentTemperatureMinTime.ToString() + ", ";
                        daily_apparentTemperatureMax += day.apparentTemperatureMax.ToString() + ", ";
                        daily_apparentTemperatureMaxTime += day.apparentTemperatureMaxTime.ToString() + ", ";
                        daily_dewPoint += day.dewPoint.ToString() + ", ";
                        daily_humidity += day.humidity.ToString() + ", ";
                        daily_windSpeed += day.windSpeed.ToString() + ", ";
                        daily_windGust += day.windGust.ToString() + ", ";
                        daily_windBearing += day.windBearing.ToString() + ", ";
                        daily_visibility += day.visibility.ToString() + ", ";
                        daily_cloudCover += day.cloudCover.ToString() + ", ";
                        daily_pressure += day.pressure.ToString() + ", ";
                        daily_ozone += day.ozone.ToString() + ", ";
                        daily_uvIndex += day.uvIndex.ToString() + ", ";
                        daily_uvIndexTime += day.uvIndexTime.ToString() + ", ";

                    }
                    Insert.Parameters.AddWithValue("@daily_time", daily_time);
                    Insert.Parameters.AddWithValue("@daily_summary", daily_summary);
                    Insert.Parameters.AddWithValue("@daily_sunriseTime", daily_sunriseTime);
                    Insert.Parameters.AddWithValue("@daily_sunsetTime", daily_sunsetTime);
                    Insert.Parameters.AddWithValue("@daily_moonPhase", daily_moonPhase);
                    Insert.Parameters.AddWithValue("@daily_precipIntensity", daily_precipIntensity);
                    Insert.Parameters.AddWithValue("@daily_precipIntensityMax", daily_precipIntensityMax);
                    Insert.Parameters.AddWithValue("@daily_precipIntensityMaxTime", daily_precipIntensityMaxTime);
                    Insert.Parameters.AddWithValue("@daily_precipProbability", daily_precipProbability);
                    Insert.Parameters.AddWithValue("@daily_precipType", daily_precipType);
                    Insert.Parameters.AddWithValue("@daily_temperatureMin", daily_temperatureMin);
                    Insert.Parameters.AddWithValue("@daily_temperatureMinTime", daily_temperatureMinTime);
                    Insert.Parameters.AddWithValue("@daily_temperatureMax", daily_temperatureMax);
                    Insert.Parameters.AddWithValue("@daily_temperatureMaxTime", daily_temperatureMaxTime);
                    Insert.Parameters.AddWithValue("@daily_apparentTemperatureMin", daily_apparentTemperatureMin);
                    Insert.Parameters.AddWithValue("@daily_apparentTemperatureMinTime", daily_apparentTemperatureMinTime);
                    Insert.Parameters.AddWithValue("@daily_apparenttemperatureMax", daily_apparentTemperatureMax);
                    Insert.Parameters.AddWithValue("@daily_apparentTemperatureMaxTime", daily_apparentTemperatureMaxTime);
                    Insert.Parameters.AddWithValue("@daily_dewPoint", daily_dewPoint);
                    Insert.Parameters.AddWithValue("@daily_humidity", daily_humidity);
                    Insert.Parameters.AddWithValue("@daily_windSpeed", daily_windSpeed);
                    Insert.Parameters.AddWithValue("@daily_windGust", daily_windGust);
                    Insert.Parameters.AddWithValue("@daily_windBearing", daily_windBearing);
                    Insert.Parameters.AddWithValue("@daily_visibility", daily_visibility);
                    Insert.Parameters.AddWithValue("@daily_cloudCover", daily_cloudCover);
                    Insert.Parameters.AddWithValue("@daily_pressure", daily_pressure);
                    Insert.Parameters.AddWithValue("@daily_ozone", daily_ozone);
                    Insert.Parameters.AddWithValue("@daily_uvIndex", daily_uvIndex);
                    Insert.Parameters.AddWithValue("@daily_uvIndexTime", daily_uvIndexTime);
                    Insert.Parameters.AddWithValue("@daily_summary_no_list", json_response.daily.summary);


                    try{
                        conn.Open();
                        try{
                            Insert.ExecuteNonQuery();
                        }
                        catch
                        {

                        }
                    }
                    catch
                    {

                    }
                }
            }  
        }
    }
}
