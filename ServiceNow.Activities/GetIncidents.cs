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
using Newtonsoft.Json;

namespace ServiceNow
{
    public class GetIncidents : CodeActivity
    {
        [Category("Optional")]
        public InArgument<String> SysParm_query { get; set; }

        [Category("Output")]
        public OutArgument<JObject> IncidentList { get; set; }

        public GetIncidents()
        {
            this.Constraints.Add(ActivityConstraints.HasParentType<GetIncidents, ServiceNowScope>(string.Format("Activity is valid only inside {0}", (object)typeof(ServiceNowScope).Name)));
        }

        protected override void Execute(CodeActivityContext context)
        {
            ServiceNowProp snowDetails = (ServiceNowProp) context.DataContext.GetProperties()["snowDetails"].GetValue(context.DataContext);

            var userName = snowDetails.UserName;
            var password = snowDetails.Password;
            var snowInstance = snowDetails.SnowInstance;
            String sysparm = SysParm_query.Get(context);
            string uri = snowInstance + "/api/now/table/incident";

            if (sysparm != null)
            {
                uri = uri + "?sysparm_query=" + sysparm;
            }

            Console.WriteLine("details - " + userName + password + snowInstance);

            Uri callUri = new Uri(uri, UriKind.Absolute);

            var client = new RestClient(callUri);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JObject json = JsonConvert.DeserializeObject<JObject>(response.Content);

            Console.WriteLine("response - " + response.Content);

            Console.WriteLine("json - " + json.ToString());

            //Object resp1 = json;

            IncidentList.Set(context, json);
        }
    }
}
