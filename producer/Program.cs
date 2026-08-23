using producer.Models;
using producer.Service;

var load = new Loader();
var loadData = load.LoadLiveStatusFromJson("Data/field_reports.json");
Console.WriteLine($"Loaded: {loadData.Count} reports");
var producer = new ProducerService();

var maxNum = loadData.Count();

try
{
    for (int i = 0; i < maxNum; i++)
    {
        if (string.IsNullOrEmpty(loadData[i].assetId.ToString()))
        {
            Console.WriteLine("nnn");
        }
        if(loadData[i].assetType.Equals("PerimeterSensor"))
        {
            await producer.SendAsync<LiveStatus>(loadData[i], "PerimeterSensor-topic");
        }
        else if(loadData[i].assetType.Equals("UAV"))
        {
            await producer.SendAsync<LiveStatus>(loadData[i], "UAV-topic");
        }
    }   
}
catch (Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}
producer.Dispose();