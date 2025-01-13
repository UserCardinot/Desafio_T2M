using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonDateOnlyConverter : JsonConverter<DateTime>
{
    private const string DateFormat = "dd-MM-yyyy";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return DateTime.MinValue;
        }

        var dateString = reader.GetString();
        if (DateTime.TryParseExact(dateString, DateFormat, null, System.Globalization.DateTimeStyles.None, out var result))
        {
            return result;
        }

        throw new JsonException($"Invalid date format. Expected format: {DateFormat}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}
