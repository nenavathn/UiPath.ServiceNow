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
    public class GetIncidents : CodeActivity
    {

        [Category("Output")]
        public OutArgument<JObject> IncidentList { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            ServiceNowProp snowDetails = (ServiceNowProp) context.DataContext.GetProperties()["snowDetails"].GetValue(context.DataContext);

            var userName = snowDetails.UserName;
            var password = snowDetails.Password;
            var snowInstance = snowDetails.SnowInstance;

            Console.WriteLine("details - " + userName + password + snowInstance);

            Uri callUri = new Uri((snowInstance + "/api/now/table/incident"), UriKind.Absolute);

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
