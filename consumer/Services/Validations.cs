using consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer.Services
{
    public class Validations
    {
        public List<string> ValidUAV(LiveStatus json)
        {
            int value = Convert.ToInt32(json.rawValue);
            var list = new List<string>();
            if (value >= 20 && value <= 100)
            {
                list.Add("true");
                return list;
            }
            if (value >= 0)
            {
                list.Add("nini");
                return list;
            }
            else
            {
                list.Add("false");
                return list;
            }
        }
        public List<string> ValidPerimeter(LiveStatus json)
        {
            var list = new List<string>();
            if (json.rawValue?.ToLower() == "good")
            {
                list.Add("true");
                return list;
            }
            if (json.rawValue?.ToLower() == "bad")
            {
                list.Add("nini");
                return list;
            }
            else
            {
                list.Add("false");
                return list;
            }
        }
    }
}
