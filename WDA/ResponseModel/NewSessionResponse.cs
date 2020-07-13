using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

    public partial class SessionResponse
    {
        [JsonProperty("value")]
        public Value Value { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }

    public partial class Value
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("capabilities")]
        public Capabilities Capabilities { get; set; }
    }

    public partial class Capabilities
    {
        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("browserName")]
        public string BrowserName { get; set; }

        [JsonProperty("sdkVersion")]
        public string SdkVersion { get; set; }

        [JsonProperty("CFBundleIdentifier")]
        public string CfBundleIdentifier { get; set; }
    }


