using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

public class Client
{
    public string Url { get; set; }
    private RestClient client { get; set; }
    private RestRequest restRequest { get; set; }
    private IRestResponse restResponse { get; set; }

    public Client(string url)        //http://25.39.210.168:8100/
    {
        Url = url;
        client = new RestClient(Url);
    } 
    

    public T FireRequest<T>(string resource, RestSharp.Method method, string data=null)
    {
        restRequest = new RestRequest(resource, method);
        restRequest.RequestFormat = DataFormat.Json;
        T response = default(T);

        if(method == Method.POST)
        {
            restRequest.AddParameter("application/json", data, ParameterType.RequestBody);
        }
        try
        {
            restResponse = client.Execute(restRequest);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<T>(restResponse.Content);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error);
        }

        return response;
    }

}

