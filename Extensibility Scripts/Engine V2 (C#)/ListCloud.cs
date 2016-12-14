using System;
using MyMis.Scripting.Core.Contracts;
using MyMis.Scripting.ConnectorClient;
using System.Collections.Generic;
using System.Linq;

/*
Developed by: NumbersBelieve
Function: Script that is executed as a button on a list of the platform, and gets a file from an external system. Paired with ListConnector.cs

Parameters: List based off a non-external entity, with the Agent:#Company.Code parameter.
*/

namespace myMIS
{
    public class Script
    {
        public ScriptResponse Execute(ContextData context, Entity document, Dictionary<string, object> parameters)
        {
            var externalSystem = context.ExternalSystems.Where(e => e.Key == (string)document["Agent:#Company.Code"]).FirstOrDefault().Value;

            var fileToExecute = context.Files.Where(f => f.ExecutionType == "Remote").FirstOrDefault();
            if (fileToExecute == null)
            {
                throw new Exception("File not found");
            }

            ConnectorClient client = new ConnectorClient(context.Api.Endpoint, externalSystem.ConnectorCode);
            var connectorResponse = client.Execute(context.Tenant, new MyMis.Scripting.Core.Contracts.Script()
            {
                Code = fileToExecute.Name,
                Version = fileToExecute.Version
            }, context, document, parameters
            );

            return new ScriptResponse
            {
                RedirectUrl = connectorResponse.Result.FirstOrDefault().Actions["URL"]
            };
        }
		
		//must use a dictionary, so we can access the Company field by Agent:#Company.Code
        public class Entity : Dictionary<string,object>
        {
        }
    }
}