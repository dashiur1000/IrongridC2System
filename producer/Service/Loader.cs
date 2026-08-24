using producer.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var file = JsonSerializer.Deserialize<List<LiveStatus>>(str, options);
                return file ?? new List<LiveStatus>();
            }
            catch
            {
                return new List<LiveStatus>();
            }
        }
    }
}
