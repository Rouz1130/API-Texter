using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwilloText
{
     public class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.twilio.com/2010-04-01");
            //1- We're making a GET request to the URL now. We've tacked on .json to the end of the URL to get the response in JSON format.
            var request = new RestRequest("Accounts/{{Account SID}}/Messages.json", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator("{{Account SID}}", "{{Auth Token}}");

            //2-We initialize a new RestResponse variable named response
            var response = new RestResponse();

            //3a-The request is made with an asynchronous method, and Task.Run with Wait() allows us to await asynchronous calls in a "synchronous" way. 
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();


            //4-The response has a Content property, which we write to the console.
            Console.WriteLine(response.Content);
            Console.ReadLine();
        }

        //3b- We set response equal to the response from our request, which we make in the method shown in 3b, and then cast as the type RestResponse.
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

    }
}