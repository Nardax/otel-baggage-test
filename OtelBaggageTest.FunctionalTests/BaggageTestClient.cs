using Refit;

namespace OtelBaggageTest.UnitTests;

public interface IBaggageApi
{
    [Get("/baggage/")]
    Task<BaggageDetails> GetBaggageDetails([HeaderCollection] IDictionary<string, string?> headers);
}

public class BaggageTestClient
{
    private IBaggageApi _baggageApi;

    public BaggageTestClient()
    {
        _baggageApi = RestService.For<IBaggageApi>("https://localhost:7131/");
    }

    public async Task<BaggageDetails> GetBaggageDetails(string id, bool? isTest, int? weightTime)
    {
        var headers = new Dictionary<string, string?>
        {
            { Constants.IdKey, id },
            { Constants.IsTestKey, isTest?.ToString() },
            { Constants.WaitTimeKey, weightTime?.ToString() }
        };

        var baggageDetails = await _baggageApi.GetBaggageDetails(headers);
        return baggageDetails;
    }
}