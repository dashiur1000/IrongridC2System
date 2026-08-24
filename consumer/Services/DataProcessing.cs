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
            else if (valid.Contains("nini"))
            {
                ProcessedStatus = "Warning";
                IsVerified = true;
            }
            else
            {
                ProcessedStatus = "Warning";
                IsVerified = false;
            }

            var existingStatus = _consumerDbContext.AssetLiveStatus
                .FirstOrDefault(x => x.assetId == c.assetId);

            if (existingStatus != null)
            {
                existingStatus.assetType = c.assetType;
                existingStatus.rawValue = c.rawValue;
                existingStatus.ProcessedStatus = ProcessedStatus;
                existingStatus.IsVerified = IsVerified;
                existingStatus.LastUpdate = DateTime.Now;
            }
            else
            {
                var newStatus = new AssetLiveStatus()
                {
                    assetId = c.assetId,
                    assetType = c.assetType,
                    rawValue = c.rawValue,
                    ProcessedStatus = ProcessedStatus,
                    IsVerified = IsVerified,
                    LastUpdate = DateTime.Now
                };
                _consumerDbContext.AssetLiveStatus.Add(newStatus);
            }

            _consumerDbContext.SaveChanges();
            return true;
        }
        public bool LiveStatusToDbPerimeter(string model, List<string> valid)
        {
            var c = JsonSerializer.Deserialize<LiveStatus>(model);
            if (c == null)
            {
                return false;
            }
            string? Normalization = string.Empty;
            string ProcessedStatus = string.Empty;
            bool IsVerified = false;

            if (valid.Contains("true"))
            {
                Normalization = "Good";
                ProcessedStatus = "Stable";
                IsVerified = true;
            }
            else if (valid.Contains("nini"))
            {
                Normalization = "Bad";
                ProcessedStatus = "Warning";
                IsVerified = true;
            }
            else
            {
                Normalization = c.rawValue;
                ProcessedStatus = "Warning";
                IsVerified = false;
            }

            var existingStatus = _consumerDbContext.AssetLiveStatus
                .FirstOrDefault(x => x.assetId == c.assetId);

            if (existingStatus != null)
            {
                existingStatus.assetType = c.assetType;
                existingStatus.rawValue = Normalization;
                existingStatus.ProcessedStatus = ProcessedStatus;
                existingStatus.IsVerified = IsVerified;
                existingStatus.LastUpdate = DateTime.Now;
            }
            else
            {
                var newStatus = new AssetLiveStatus()
                {
                    assetId = c.assetId,
                    assetType = c.assetType,
                    rawValue = Normalization,
                    ProcessedStatus = ProcessedStatus,
                    IsVerified = IsVerified,
                    LastUpdate = DateTime.Now
                };
                _consumerDbContext.AssetLiveStatus.Add(newStatus);
            }

            _consumerDbContext.SaveChanges();
            return true;
        }
    }
}
