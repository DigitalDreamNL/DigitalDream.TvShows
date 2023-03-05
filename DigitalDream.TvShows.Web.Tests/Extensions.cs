using System.Text.Json;

namespace DigitalDream.TvShows.Web.Tests;

public static class Extensions
{
    public static string AsJsonString<TValue>(this TValue self)
    {
        return JsonSerializer.Serialize(self, Constants.DefaultJsonSerializerOptions);
    }
}