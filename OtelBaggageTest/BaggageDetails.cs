namespace OtelBaggageTest;

public class BaggageDetails
{
    public string Id { get; set; }

    public string? IsTest { get; set; }

    public int RecievedOrder { get; set; }

    public int ReturnedOrder { get; set; }

    public string? WaitTime { get; set; }
}