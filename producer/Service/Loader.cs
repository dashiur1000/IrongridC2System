using producer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace producer.Service
{
    public class Loader
    {
        public List<LiveStatus> LoadLiveStatusFromJson(string json)
        {
            var str = File.ReadAllText(json);
            try
            {
                var file = JsonSerializer.Deserialize<List<LiveStatus>>(str);
                return file;
            }
            catch
            {
                return new List<LiveStatus>();
            }
        }
    }
}
