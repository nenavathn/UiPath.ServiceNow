using System;
using System.Activities;
using System.ComponentModel;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ServiceNow
{
    [Description("Update an Incident")]
    public class UpdateIncident : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> IncidentSysID { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Body (JSON string)")]
        public InArgument<String> Body { get; set; }

        [Category("Output")]
        public OutArgument<JObject> IncidentObject { get; set; }

        public UpdateIncident()
        {
            this.Constraints.Add(ActivityConstraints.HasParentType<UpdateIncident, ServiceNowScope>(string.Format("Activity is valid only inside {0}", (object)typeof(ServiceNowScope).Name)));
        }

        protected override void Execute(CodeActivityContext context)
        {
            ServiceNowProp snowDetails = (ServiceNowProp)context.DataContext.GetProperties()["snowDetails"].GetValue(context.DataContext);

            var userName = snowDetails.UserName;
            var password = snowDetails.Password;
            var snowInstance = snowDetails.SnowInstance;
            var incidentNumber = IncidentSysID.Get(context);
            var body = Body.Get(context);

            if (incidentNumber == null)
                throw new ArgumentException("IncidentSysID");

            if (body == null)
                throw new ArgumentException("Body");

            JObject jbody = JsonConvert.DeserializeObject<JObject>(body);
            Object jbody1 = jbody;

            //Console.WriteLine(jbody1.ToString());

            string uri = snowInstance + "/api/now/table/";

            Uri callUri = new Uri((uri), UriKind.Absolute);

            var client = new RestClient(callUri);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);

            var request = new RestRequest("incident/" + incidentNumber,Method.PATCH);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            //request.AddParameter("application/json", "{\"urgency\":\"1\"}", ParameterType.RequestBody);
            //request.AddJsonBody(jbody);
            //request.AddBody(jbody);
            //request.AddBody(jbody1);

            IRestResponse response = client.Execute(request);

            JObject json = JsonConvert.DeserializeObject<JObject>(response.Content);

            JObject jr1 = JsonConvert.DeserializeObject<JObject>(json.SelectToken("result").ToString());

            IncidentObject.Set(context, jr1);

            //Console.WriteLine("upd - " + json.ToString());
        }
    }
}
