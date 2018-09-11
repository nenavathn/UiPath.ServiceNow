using System;
using System.Activities;
using System.ComponentModel;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ServiceNow
{  
    [DisplayName("Get Incident data")]
    [Description("Retrieves the details of the incident in JSON format - JObject")]
    public class GetIncident : CodeActivity
    {

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> IncidentNumber { get; set; }

        [Category("Output")]
        public OutArgument<JObject> IncidentJObject { get; set; }

        public GetIncident()
        {
            this.Constraints.Add(ActivityConstraints.HasParentType<GetIncident, ServiceNowScope>(string.Format("Activity is valid only inside {0}", (object)typeof(ServiceNowScope).Name)));
        }

        protected override void Execute(CodeActivityContext context)
        {
            ServiceNowProp snowDetails = (ServiceNowProp)context.DataContext.GetProperties()["snowDetails"].GetValue(context.DataContext);

            var userName = snowDetails.UserName;
            var password = snowDetails.Password;
            var snowInstance = snowDetails.SnowInstance;
            var incidentNumber = IncidentNumber.Get(context);

            if (incidentNumber == null)
                throw new ArgumentException("IncidentNumber");

            //Console.WriteLine("Incident Number - " + incidentNumber);

            Uri callUri = new Uri((snowInstance + "/api/now/table/incident?sysparm_query=number=" + incidentNumber) , UriKind.Absolute);

            var client = new RestClient(callUri);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JObject json = JsonConvert.DeserializeObject<JObject>(response.Content);

            JArray jr1 = JsonConvert.DeserializeObject<JArray>(json.SelectToken("result").ToString());

            JObject final = JsonConvert.DeserializeObject<JObject>(jr1.First.ToString());

            //Console.WriteLine("response - " + response.Content);

            //Console.WriteLine("json - " + jr1.ToString());

            //Console.WriteLine("first - " + final.GetValue("sys_id"));

            //Object resp1 = json;

            IncidentJObject.Set(context, final);
        }

    }
}
