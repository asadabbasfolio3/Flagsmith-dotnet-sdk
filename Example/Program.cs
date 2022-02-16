using Example.Extensions;
using Flagsmith;
var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterFlagsmithClientAsSingleton(builder.Configuration);
var app = builder.Build();

app.MapGet("/", async (FlagsmithClient flagsmithClient) =>
{
    var flags = await flagsmithClient.GetFeatureFlags();
    return flags.Select(async f => new
    {
        name = f.GetFeature().GetName(),
        isEnabled = f.IsEnabled(),
        value = await f.GetValue()
    });
});
app.MapPost("/", () => "");

app.Run();
