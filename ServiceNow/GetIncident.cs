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

namespace ServiceNow
{  

    public class GetIncident : CodeActivity
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

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> IncidentNumber { get; set; }

        [Category("Output")]
        public OutArgument<Object> IncidentObject { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var userName = UserName.Get(context);
            var password = Password.Get(context).ToString();

            Uri callUri = new Uri((SnowInstance.Get(context) + "/api/now/table/incident?sysparm_query=number=" + IncidentNumber), UriKind.Absolute);

            var client = new RestClient(callUri);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JObject json = JObject.Parse(response.Content);

            Object resp1 = json;

            IncidentObject.Set(context, resp1);
        }

    }
}
