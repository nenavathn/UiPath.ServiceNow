using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Security;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using ActivityDesignerLibrary2;

namespace ServiceNow
{
    public class GetIncidents : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> SnowInstance { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> UserName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> Password { get; set; }

        [Category("Output")]
        public OutArgument<JObject> IncidentList { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var userName = UserName.Get(context);
            var password = Password.Get(context).ToString();

            Uri callUri = new Uri((SnowInstance.Get(context) + "/api/now/table/incident"), UriKind.Absolute);

            var client = new RestClient(callUri);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JObject json = JObject.Parse(response.Content);

            Console.WriteLine("response - " + response.Content);

            Console.WriteLine("json - " + json.ToString());


            //Object resp1 = json;

            IncidentList.Set(context, json);
        }
    }
}
