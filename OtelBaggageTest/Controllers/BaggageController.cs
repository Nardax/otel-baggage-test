using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace OtelBaggageTest.Controllers;

[ApiController]
[Route("[controller]")]
public class BaggageController : ControllerBase
{
    private static int _receivedOrder = 0;
    private static int _returnedOrder = 0;

    [HttpGet(Name = "GetBaggageDetails")]
    public async Task<BaggageDetails> Get()
    {
        var baggageDetails = new BaggageDetails
        {
            Id = Baggage.GetBaggage(Constants.IdKey),
            IsTest = Baggage.GetBaggage(Constants.IsTestKey),
            WaitTime = Baggage.GetBaggage(Constants.WaitTimeKey),
            RecievedOrder = Interlocked.Increment(ref _receivedOrder)
        };

        if (!string.IsNullOrEmpty(baggageDetails.WaitTime))
        {
            await Task.Delay(int.Parse(baggageDetails.WaitTime));
        }

        baggageDetails.ReturnedOrder = Interlocked.Increment(ref _returnedOrder);

        return baggageDetails;
    }
}