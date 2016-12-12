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
            //1-Make a connection with the server where the API is located.
            var client = new RestClient("https://api.twilio.com/2010-04-01");

            //2-  Create the request, and add the physical path to the specific API controller and choose the HTTP method.
            var request = new RestRequest("Accounts/{{ec711bf2721a30f2d7cf4594a66a28a2}}/Messages", Method.POST);

            //3-Add parameters to our request. Here we've set the text message's sender, recipient, and actual message.
            request.AddParameter("To", "+3312145769");
            request.AddParameter("From", "{{sender's phone number}}");
            request.AddParameter("Body", "Hello world!");


            //4-Give the client the appropriate credentials.
            client.Authenticator = new HttpBasicAuthenticator("{{Account SID}}", "{{Auth Token}}");


            //5-Execute the request to the client. Note that the second argument for 
            // ExecuteAsync needs callback (which we include here as a Console.WriteLine()) to process the request.
            client.ExecuteAsync(request, response =>
            {
                Console.WriteLine(response);
            });
            Console.ReadLine();
        }
    }
}