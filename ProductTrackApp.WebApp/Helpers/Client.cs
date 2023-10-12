using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;

namespace ProductTrackApp.WebApp.Helpers
{
    public class Client<T> where T : class
    {
        public List<T> ExecuteList(string url, RestSharp.Method method, string token, string bodyJson)
        {
            var client = new RestClient(url);
            var clientRequest = new RestRequest("", method);
            clientRequest.AddHeader("Content-Type", "application/json");
            //clientRequest.AddHeader("Key", token.ToString());
            if (token != null)
                clientRequest.AddHeader("Authorization", $"Bearer {token}");
            clientRequest.AddParameter("application/json", bodyJson, ParameterType.RequestBody);
            var clientResponse = client.Execute<T>(clientRequest);

            var deserialize = JsonConvert.DeserializeObject<List<T>>(clientResponse.Content);

            return deserialize;
        }

        public T Execute(string url, RestSharp.Method method, string token, string bodyJson)
        {
            var client = new RestClient(url);
            var clientRequest = new RestRequest("", method);
            clientRequest.AddHeader("Content-Type", "application/json");
            //clientRequest.AddHeader("Key", token.ToString());
            if (token != null)
                clientRequest.AddHeader("Authorization", $"Bearer {token}");
            clientRequest.AddParameter("application/json", bodyJson, ParameterType.RequestBody);
            var clientResponse = client.Execute<T>(clientRequest);

            var deserialize = JsonConvert.DeserializeObject<T>(clientResponse.Content);

            return deserialize;
        }
    }
}
