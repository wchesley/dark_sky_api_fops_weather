using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dark_sky_weather
{
    class JSON_Config
    {
        public class City
        {
            
            public string city { get; set; }
            public string location { get; set; }
        }

        public class Database
        {
            public string connection { get; set; }
        }

        public class Api
        {
            public string URL { get; set; }
        }

        public class RootObject
        {
            public List<City> cities { get; set; }
            public Database database { get; set; }
            public Api Api { get; set; }
        }
    }
}
