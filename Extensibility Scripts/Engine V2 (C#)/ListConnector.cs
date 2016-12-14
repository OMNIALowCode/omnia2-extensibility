using System;
using MyMis.Scripting.Core.Contracts;
using MyMis.Scripting.PlatformApi;
using MyMis.Scripting.PlatformApi.Models;
using MyMis.Scripting.ConnectorClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/*
Developed by: NumbersBelieve
Function: Script that is executed on an external system to return a file to a list that requested it. Paired with ListCloud.cs

Parameters: List based off an entity, with ID and Code fields.
*/
namespace myMIS
{
    public class Script
    {
        public ScriptResponse Execute(ContextData context, Entity document, Dictionary<string, object> parameters)
        {
            ApiClient client = new ApiClient(context.Api.Endpoint, context.Tenant);
            client.Authenticate(context.Api.Email, context.Api.Password);

            string contents = $"Connector accessed from company {context.ExternalSystems.Values.FirstOrDefault().Code} in tenant {context.Tenant}";
            string fileLocation = null;
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(contents)))
            {
                fileLocation = client.BinaryData.UploadFile(ms, "test.txt");
            }

            var actions = new Dictionary<string, object>();
            actions.Add("URL", fileLocation);

            return new ScriptResponse
            {
                Actions = actions
            };
        }
		public class Entity
		{
			int ID { get; set; }
			string Code { get; set; }
		}
    }
}