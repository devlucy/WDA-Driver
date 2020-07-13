
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
namespace WDA{
    public class iOSElement
    {
        private string elementId = null;
        private string elementRef { get; set;}
        private Client client = null;

       public  iOSElement(string elementId, string elementRef, Client client) {
            this.elementId = elementId;
            this.elementRef = elementRef;
            this.client = client;
        }

        public void Click()
        {
            client.FireRequest<Source>(elementRef + elementId + "/click", Method.POST, null);
        }
        public void Clear() {
            client.FireRequest<Source>(elementRef + elementId + "/clear", Method.POST, null);
        }

        public void SendKeys(string keys)
        {
            var body = new Dictionary<string, object>()
            {
                { "value", keys.ToArray()}
            };
            client.FireRequest<Source>(elementRef + elementId + "/value", Method.POST, JsonConvert.SerializeObject(body));
        }
    }
}