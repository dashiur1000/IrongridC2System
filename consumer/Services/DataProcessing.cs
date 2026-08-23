using consumer.Data;
using consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace consumer.Services
{
    public class DataProcessing
    {
        private readonly consumerDbContext _consumerDbContext;
        public DataProcessing(consumerDbContext consumerDbContext)
        {
            _consumerDbContext = consumerDbContext;
        }
        public bool LiveStatusToDb(string model, List<string> valid)
        {
            var c = JsonSerializer.Deserialize<LiveStatus>(model);
            if (c == null)
            {
                return false;
            }
            string ProcessedStatus = string.Empty;
            bool IsVerified = false;
            if (valid.Contains("true"))
            {
                ProcessedStatus = "Stable";
                IsVerified = true;
            }
            if(valid.Contains("nini"))
            {
                ProcessedStatus = "Warning";
                IsVerified = true;
            }
            else
            {
                ProcessedStatus = "Warning";
                IsVerified = false;
            }
            var status = new AssetLiveStatus()
            {
                assetId = c.assetId,
                assetType = c.assetType,
                rawValue = c.rawValue,
                ProcessedStatus = ProcessedStatus,
                IsVerified = IsVerified,
                LastUpdate = DateTime.Now
            };
            _consumerDbContext.AssetLiveStatus.Add(status);
            _consumerDbContext.SaveChanges();
            return true;
        }
        public List<string> ValidUAV(LiveStatus json)
        {
            int value = Convert.ToInt32(json.rawValue);
            var list = new List<string>();
            if (value >= 20 && value <= 100)
            {
                list.Add("true");
                return list;
            }
            if(value >= 0)
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
            if(json.rawValue?.ToLower() == "good")
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
