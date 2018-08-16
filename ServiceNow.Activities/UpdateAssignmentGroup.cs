using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;

namespace ServiceNow.Activities
{

    public sealed class UpdateAssignmentGroup : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> IncidentNumber { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> AssignmentGroup { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string incNum = context.GetValue(this.IncidentNumber);
            string asgnGrp = context.GetValue(this.AssignmentGroup);

            ServiceNowProp snowDetails = (ServiceNowProp)context.DataContext.GetProperties()["snowDetails"].GetValue(context.DataContext);

            var userName = snowDetails.UserName;
            var password = snowDetails.Password;
            var snowInstance = snowDetails.SnowInstance;

            Uri callUri = new Uri((snowInstance + "/api/now/table/sys_user_group?name=" + asgnGrp), UriKind.Absolute);

            var client = new RestClient(callUri);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);
            
            JObject json = JObject.Parse(response.Content);

            Console.WriteLine("response - " + response.Content);

            Console.WriteLine("json - " + json.ToString());
            if (json.SelectToken("result[0].name") != null)
            {
                Console.WriteLine("json not null");


            } else
            {
                Console.WriteLine("json null");

            }


            //Object resp1 = json;

            //IncidentList.Set(context, json);
        }
    }
}
