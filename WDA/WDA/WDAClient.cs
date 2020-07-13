using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
namespace WDA
{
    public class WDAClient
    {
        private Dictionary<string, object> Body { get; set; }
        private Client client = null;
        private WdaApi wdaApi = null;
        public WDAClient(string Url, string bundleId)
        {
            WDASession(Url, bundleId);
        }
        public void Tap(int x, int y)
        {
            Body = new Dictionary<string, object>()
            {
                { "x", x},
                { "y", y}
            };
            client.FireRequest<Source>(wdaApi.Tap, Method.POST, SerialiseJson(Body));
        }
        public void DragForDuration(int fromX, int fromY, int toX, int toY, float duration)
        {
            Body = new Dictionary<string, object>()
            {
                { "fromX", fromX},
                { "fromY", fromY},
                { "toX", toX},
                { "toY", toY},
                { "duration", duration}
            };
            client.FireRequest<Source>(wdaApi.DragFromToForDuration, Method.POST, SerialiseJson(Body));
        }
        public string GetPageSource()
        {
            var src = client.FireRequest<Source>(wdaApi.PageSource, Method.GET, null);
            return src.Value;
        }
        public Rectangle WindowSize()
        {
            var src = client.FireRequest<Window>(wdaApi.WindowSize, Method.GET, null);
            return src.Value;
        }
        public void DeactivateApp(int duration)
        {

            Body = new Dictionary<string, object>()
            {
                { "duration", duration }

            };
            client.FireRequest<Source>(wdaApi.deactivate, Method.POST, SerialiseJson(Body));

        }
        public void ClearWithoutReference()
        {
            string[] array = new string[20];
            for (int i = 0; i <= 20; i++)
            {
                if (i.Equals(20 - 1))
                {
                    array[i] = "\n"; break;
                }
                else
                {
                    array[i] = "\b";
                }
            }

            Body = new Dictionary<string, object>()
            {
                { "value", array}
            };


            client.FireRequest<Source>(wdaApi.WithoutReference, Method.POST, SerialiseJson(Body));
        }
        public void SendKeysWithoutReference(string keys)
        {
            var keylist = keys.ToList<char>();
            keylist.Add('\n');
            Body = new Dictionary<string, object>()
            {
                { "value", keylist},
            };
            client.FireRequest<Source>(wdaApi.WithoutReference, Method.POST, SerialiseJson(Body));
        }
        private void WDASession(string uri, string bundleId)
        {
            client = new Client(uri);
            var body = new Dictionary<string, Dictionary<string, object>> {
            {"capabilities", new Dictionary<string, object> {
             {"bundleId", bundleId}
             }}};
            string output = JsonConvert.SerializeObject(body);
            wdaApi = new WdaApi(client.FireRequest<SessionResponse>("session", Method.POST, output).SessionId);
            TerminateApp(bundleId);
            LauchApp(bundleId);
        }
        public void LauchApp(string bundleId)
        {
            Body = new Dictionary<string, object>()
            {
                { "bundleId", bundleId }

            };
            client.FireRequest<Source>(wdaApi.LaunchApp, Method.POST, SerialiseJson(Body));
        }
        public void TerminateApp(string bundleId)
        {
            Body = new Dictionary<string, object>()
            {
                { "bundleId", bundleId}

            };
            client.FireRequest<Source>(wdaApi.TerminateApp, Method.POST, SerialiseJson(Body));
        }
        public void HideKeyboard()
        {
            client.FireRequest<Source>(wdaApi.HideKeyboard, Method.POST, null);
        }
        public void AppState(string bundleId)
        {
            Body = new Dictionary<string, object>()
            {
                { "bundleId", bundleId}

            };
            client.FireRequest<Source>(wdaApi.State, Method.POST, SerialiseJson(Body));
        }
        public void Home()
        {
            client.FireRequest<Source>(wdaApi.Home, Method.POST, null);
        }
        public void GetWindowSize()
        {
            client.FireRequest<Source>(wdaApi.WindowSize, Method.POST, null);
        }
        public void SetOrientation(string orientation)
        {
            Body = new Dictionary<string, object>()
            {
                { "orientation", orientation}

            };
            client.FireRequest<Source>(wdaApi.Orientation, Method.POST, SerialiseJson(Body));
        }
        public string GetOrientation()
        {
            return client.FireRequest<Source>(wdaApi.Orientation, Method.GET, null).Value;
        }
        public byte[] GetScreenshot()
        {
            var response = client.FireRequest<Source>(wdaApi.Screenshot, Method.GET, null);
            byte[] bytes = Convert.FromBase64String(response.Value);
            return bytes;
        }
        public iOSElement FindElementByClassName(string className)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "class name"},
                { "value", className }
            };
            var element = client.FireRequest<SingleElement>(wdaApi.FindElement, Method.POST, SerialiseJson(Body)).Value;

            return new iOSElement(element.elementId, wdaApi.ElementRef, client);
        }
        public ReadOnlyCollection<iOSElement> FindElementsByClassName(string className)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "class name"},
                { "value", className }
            };
            var col = client.FireRequest<ElementSource>(wdaApi.FindElements, Method.POST, SerialiseJson(Body)).Value;
            return new ReadOnlyCollection<iOSElement>(ElementCollection(col, wdaApi.ElementRef, client));
        }
        public iOSElement FindElementByName(string name)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "link text"},
                { "value", "label="+name}
            };
            var elementRef = client.FireRequest<SingleElement>(wdaApi.FindElement, Method.POST, SerialiseJson(Body)).Value;

            return new iOSElement(elementRef.elementId, wdaApi.ElementRef, client);
        }
        public ReadOnlyCollection<iOSElement> FindElementsByName(string name)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "link text"},
                { "value", "label="+name }
            };
            var col = client.FireRequest<ElementSource>(wdaApi.FindElements, Method.POST, SerialiseJson(Body)).Value;
            return new ReadOnlyCollection<iOSElement>(ElementCollection(col, wdaApi.ElementRef, client));
        }
        public iOSElement FindElementByPartialLinkText(string partiallinktext)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "partial link text"},
                { "value", "label="+partiallinktext}
            };
            var elementRef = client.FireRequest<SingleElement>(wdaApi.FindElement, Method.POST, SerialiseJson(Body)).Value;

            return new iOSElement(elementRef.elementId, wdaApi.ElementRef, client);
        }
        public ReadOnlyCollection<iOSElement> FindElementsByPartialLinkText(string partiallinktext)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "partial link text"},
                { "value", "label="+partiallinktext}
            };
            var col = client.FireRequest<ElementSource>(wdaApi.FindElements, Method.POST, SerialiseJson(Body)).Value;
            return new ReadOnlyCollection<iOSElement>(ElementCollection(col, wdaApi.ElementRef, client));
        }
        public iOSElement FindElementByXpath(string xpath)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "xpath"},
                { "value", xpath}
            };
            var elementRef = client.FireRequest<SingleElement>(wdaApi.FindElement, Method.POST, SerialiseJson(Body)).Value;

            return new iOSElement(elementRef.elementId, wdaApi.ElementRef, client);
        }
        public ReadOnlyCollection<iOSElement> FindElementsByXpath(string xpath)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "xpath"},
                { "value", xpath}
            };
            var col = client.FireRequest<ElementSource>(wdaApi.FindElements, Method.POST, SerialiseJson(Body)).Value;
            return new ReadOnlyCollection<iOSElement>(ElementCollection(col, wdaApi.ElementRef, client));
        }
        public iOSElement FindElementByClassChain(string classChain)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "class chain"},
                { "value", classChain}
            };
            var elementRef = client.FireRequest<SingleElement>(wdaApi.FindElement, Method.POST, SerialiseJson(Body)).Value;

            return new iOSElement(elementRef.elementId, wdaApi.ElementRef, client);
        }
        public ReadOnlyCollection<iOSElement> FindElementsByClassChain(string classchain)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "class chain"},
                { "value", classchain}
            };
            var col = client.FireRequest<ElementSource>(wdaApi.FindElements, Method.POST, SerialiseJson(Body)).Value;
            return new ReadOnlyCollection<iOSElement>(ElementCollection(col, wdaApi.ElementRef, client));
        }
        public iOSElement FindElementByPredicateString(string predicate)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "predicate string"},
                { "value", predicate}
            };
            var elementRef = client.FireRequest<SingleElement>(wdaApi.FindElement, Method.POST, SerialiseJson(Body)).Value;

            return new iOSElement(elementRef.elementId, wdaApi.ElementRef, client);
        }
        public ReadOnlyCollection<iOSElement> FindElementsByPredicateString(string predicate)
        {
            Body = new Dictionary<string, object>()
            {
                { "using", "predicate string"},
                { "value", predicate}
            };
            var col = client.FireRequest<ElementSource>(wdaApi.FindElements, Method.POST, SerialiseJson(Body)).Value;
            return new ReadOnlyCollection<iOSElement>(ElementCollection(col, wdaApi.ElementRef, client));
        }
        private string SerialiseJson(Dictionary<string, object> dictionary)
        {
            return JsonConvert.SerializeObject(Body);
        }
        private List<iOSElement> ElementCollection(ElementRef[] elementId, string wd, Client cl)
        {
            List<iOSElement> list = new List<iOSElement>();
            foreach (var item in elementId)
            {
                list.Add(new iOSElement(item.elementId, wd, cl));
            }
            return list;
        }
        public void Exit() {
            client.FireRequest<ElementSource>("session", Method.DELETE, SerialiseJson(Body));
        }
    }
}
