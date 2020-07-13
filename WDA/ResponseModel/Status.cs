
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class Status
{
    [JsonProperty("value")]
    public Value Value { get; set; }

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }

    [JsonProperty("status")]
    public long StatusStatus { get; set; }
}

public partial class Value
{
    [JsonProperty("build")]
    public Build Build { get; set; }

    [JsonProperty("device")]
    public Device Device { get; set; }

    [JsonProperty("ios")]
    public Ios Ios { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("os")]
    public Os Os { get; set; }

    [JsonProperty("ready")]
    public bool Ready { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }
}

public partial class Build
{
    [JsonProperty("productBundleIdentifier")]
    public string ProductBundleIdentifier { get; set; }

    [JsonProperty("time")]
    public string Time { get; set; }
}

public partial class Device
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("udid")]
    public string Udid { get; set; }
}

public partial class Ios
{
    [JsonProperty("ip")]
    public object Ip { get; set; }

    [JsonProperty("simulatorVersion")]
    public string SimulatorVersion { get; set; }
}

public partial class Os
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("sdkVersion")]
    public string SdkVersion { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }
}

