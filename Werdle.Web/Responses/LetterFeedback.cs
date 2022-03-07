namespace Werdle.Web.Responses;

using System.Text.Json.Serialization;
using Enums;

public class LetterFeedback
{
    public char Letter { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LetterValidity Validity { get; set; }
}
