﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static dark_sky_weather.WeatherJSON;
using System.Reflection;

//Written By: Walker Chesley
//Class Generated by Json2csharp.com
//date 06/21/2017

namespace dark_sky_weather
{
    public class WeatherJSON
    {
        public class Currently
        {
            //Sql var ex = current_time, current_summary...etc...
            public int time { get; set; }
            public string summary { get; set; }
            public string icon { get; set; }
            public int nearestStormDistance { get; set; }
            public int nearestStormBearing { get; set; }
            public int precipIntensity { get; set; }
            public int precipProbability { get; set; }
            public double temperature { get; set; }
            public double apparentTemperature { get; set; }
            public double dewPoint { get; set; }
            public double humidity { get; set; }
            public double windSpeed { get; set; }
            public double windGust { get; set; }
            public int windBearing { get; set; }
            public int visibility { get; set; }
            public double cloudCover { get; set; }
            public double pressure { get; set; }
            public double ozone { get; set; }
            public int uvIndex { get; set; }
        }

        public class Datum 
        {
            //minutely, sql var ex = minutely_time, minutely_precipIntensity....ect... 
            public int time { get; set; }
            public double precipIntensity { get; set; }
            public double precipProbability { get; set; }
            public double? precipIntensityError { get; set; }
            public string precipType { get; set; }
        }

        public class Minutely
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum> data { get; set; }
        }

        public class Datum2 
        {
            //sql var ex = hourly_time, hourly_summary...etc... 
            public double time { get; set; }
            public string summary { get; set; }
            public string icon { get; set; }
            public double precipIntensity { get; set; }
            public double precipProbability { get; set; }
            public double temperature { get; set; }
            public double apparentTemperature { get; set; }
            public double dewPoint { get; set; }
            public double humidity { get; set; }
            public double windSpeed { get; set; }
            public double windGust { get; set; }
            public double windBearing { get; set; }
            public double visibility { get; set; }
            public double cloudCover { get; set; }
            public double pressure { get; set; }
            public double ozone { get; set; }
            public double uvIndex { get; set; }
            public string precipType { get; set; }
        }

        public class Hourly
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum2> data { get; set; }
        }

        public class Datum3 //daily
        {
            //Sql var ex = daily_time, daily_summary...etc...etc... 
            public int time { get; set; }
            public string summary { get; set; }
            public string icon { get; set; }
            public int sunriseTime { get; set; }
            public int sunsetTime { get; set; }
            public double moonPhase { get; set; }
            public double precipIntensity { get; set; }
            public double precipIntensityMax { get; set; }
            public int precipIntensityMaxTime { get; set; }
            public double precipProbability { get; set; }
            public string precipType { get; set; }
            public double temperatureMin { get; set; }
            public int temperatureMinTime { get; set; }
            public double temperatureMax { get; set; }
            public int temperatureMaxTime { get; set; }
            public double apparentTemperatureMin { get; set; }
            public int apparentTemperatureMinTime { get; set; }
            public double apparentTemperatureMax { get; set; }
            public int apparentTemperatureMaxTime { get; set; }
            public double dewPoint { get; set; }
            public double humidity { get; set; }
            public double windSpeed { get; set; }
            public double windGust { get; set; }
            public int windBearing { get; set; }
            public int visibility { get; set; }
            public double cloudCover { get; set; }
            public double pressure { get; set; }
            public double ozone { get; set; }
            public int uvIndex { get; set; }
            public int uvIndexTime { get; set; }
        }

        public class Daily
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum3> data { get; set; }
        }

        public class Alert
        {
            public string title { get; set; }
            public List<string> regions { get; set; }
            public string severity { get; set; }
            public int time { get; set; }
            public int expires { get; set; }
            public string description { get; set; }
            public string uri { get; set; }
        }

        public class Flags
        {
            public List<string> sources { get; set; }
            //public List<string> __invalid_name__isd-stations { get; set; }
        public string units { get; set; }
    }

    public class RootObject
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string timezone { get; set; }
        public int offset { get; set; }
        public Currently currently { get; set; }
        public Minutely minutely { get; set; }
        public Hourly hourly { get; set; }
        public Daily daily { get; set; }
        public List<Alert> alerts { get; set; }
        public Flags flags { get; set; }

            //should get everything in the currently class and add it to one dictionary. 
            public IDictionary<string, string> WeatherDict()
            {
                Currently currentdict = new Currently();
                PropertyInfo[] info = currentdict.GetType().GetProperties();

                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach(PropertyInfo i in info)
                {
                   dict.Add(i.Name, i.GetValue(currentdict).ToString());  
                }
                foreach(var key in dict.Keys)
                {
                    Console.WriteLine($"NamerProperty: {key}, value: {dict[key]}");
                }
                return dict;   
                /*
                 * Alt Theory: harder to read but should provide the same results as WeatherDict();
                 * currently.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                 * .ToDictionary(dict => dict.Name, dict => dict.GetValue(currently, null)) 
                 */
            }
    }
}
}
