using System;
using Newtonsoft.Json;

public class DateFormatConverter : JsonConverter<System.DateOnly>
{
    private readonly string _format;

    public DateFormatConverter(string format)
    {
        _format = format;
    }

    public override System.DateOnly ReadJson(JsonReader reader, Type objectType, System.DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value is string dateStr)
        {
            if (DateTime.TryParseExact(dateStr, _format, null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                return new System.DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
            }
        }

        throw new JsonSerializationException($"Invalid date format: {reader.Value}");
    }

    public override void WriteJson(JsonWriter writer, System.DateOnly value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(_format));
    }
}
