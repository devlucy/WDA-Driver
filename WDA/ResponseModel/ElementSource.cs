
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class ElementSource{
    [JsonProperty("value")]
public ElementRef[] Value { get; set; }

[JsonProperty("sessionId")]
public string SessionId { get; set; }
}
public partial class ElementRef
{
    [JsonProperty("ELEMENT")]
    public string elementId { get; set; }
}
public partial class SingleElement
{
    [JsonProperty("value")]
    public ElementRef Value { get; set; }

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }
}
public class Window{
    [JsonProperty("value")]
    public Rectangle Value { get; set; }

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }
}
public class Rectangle
{
    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }
}

