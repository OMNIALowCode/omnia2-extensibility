using System;
using MyMis.Scripting.Core.Contracts;
using MyMis.Scripting.PlatformApi;
using MyMis.Scripting.PlatformApi.Models;
using MyMis.Scripting.ConnectorClient;
using System.Collections.Generic;
using System.Linq;



/*
Developed by: NumbersBelieve
Function: Queries the platform's API to obtain data of an user defined entity to fill a attribute on a entity or interaction.
Parameters: An entity or interaction with a text field OrganizationalUnit and TeamApprover, with an action to execute this script (for example, a Button); A OrganizationalUnit entity of type UserDefinedEntity. 
*/

namespace myMIS
{
    public class Script
    {
        public ScriptResponse Execute(ContextData context, Entity document, Dictionary<string, object> parameters)
        {
            /* **************************************** */
            /* **************************************** */
            /*          ADD YOUR CODE HERE              */

            var externalSystem = context.ExternalSystems.FirstOrDefault().Value;

            ApiClient apiClient = new ApiClient((string)externalSystem.Parameters["ApiUrl"], context.Tenant.ToString());

            //Authenticate on OMNIA API
            apiClient.Authenticate((string)externalSystem.Parameters["ApiUsername"], (string)externalSystem.Parameters["ApiPassword"]);

            //Get OrganizationalUnit information from API
            EntityModel organizationalUnit = apiClient.Entity.GetByCode(document.Attributes.OrganizationalUnit, "OrganizationalUnit", EntityKind.UserDefinedEntity);

            //Retrieve its Code (base attribute)
            string codeOU = organizationalUnit.Code;

            //Retrieve its Approver (non-base attribute)
            string approverOU = organizationalUnit.Attributes["Approver"].ToString();

            //set OMNIA interaction approver attribute with value
            document.Attributes.TeamApprover = approverOU;

            document.MarkToApplyChanges();

            /* **************************************** */
            /* **************************************** */

            return new ScriptResponse
            {
                Message = ""
            };
        }
    }
}