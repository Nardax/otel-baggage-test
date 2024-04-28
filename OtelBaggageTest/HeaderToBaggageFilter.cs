using Microsoft.AspNetCore.Mvc.Filters;
using OpenTelemetry;

namespace OtelBaggageTest;

public class HeaderToBaggageFilter : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(Constants.IdKey, out var id))
        {
            Baggage.SetBaggage(Constants.IdKey, id);
        }

        if (context.HttpContext.Request.Headers.TryGetValue(Constants.IsTestKey, out var isTest))
        {
            Baggage.SetBaggage(Constants.IsTestKey, isTest);
        }

        if (context.HttpContext.Request.Headers.TryGetValue(Constants.WaitTimeKey, out var weightTime))
        {
            Baggage.SetBaggage(Constants.WaitTimeKey, weightTime);
        }

        await next();
    }
}