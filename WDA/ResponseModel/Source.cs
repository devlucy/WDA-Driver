
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;


public partial class Source
{
    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }
}

