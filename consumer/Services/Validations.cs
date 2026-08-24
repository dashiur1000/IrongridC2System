



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
            var list = new List<string>();

            if (!int.TryParse(json.rawValue, out int value))
            {
                list.Add("false");
                return list;
            }

            if (value >= 20 && value <= 100)
            {
                list.Add("true");
                return list;
            }
            else if (value >= 0 && value <= 19)
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
            var raw = json.rawValue?.Trim().ToLower();

            if (raw == "good" || raw == "gud")
            {
                list.Add("true");
                return list;
            }
            else if (raw == "bad" || raw == "bed")
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