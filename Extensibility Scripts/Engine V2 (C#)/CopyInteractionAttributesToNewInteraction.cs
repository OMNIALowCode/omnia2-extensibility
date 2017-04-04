//#R System.Web.dll
using MyMis.Scripting.Core.Contracts;
using MyMis.Scripting.PlatformApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
Developed by: NumbersBelieve
Function: From a list of an interaction, obtains the document of an instance of that interaction and opens the creation form for a new one with fields copied.
Parameters: 
	A PurchasesManagement process with a PurchaseOrder interaction. 
	An external system with an omniaEmail and omniaPassword attributes, containing the information of a login on the desired tenant that has permissions to list the PurchaseOrder interaction.
*/
namespace myMIS
{
    public class Script
    {
        public ScriptResponse Execute(ContextData context, Entity document, Dictionary<string, object> parameters)
        {
            var externalSystem = context.ExternalSystems.FirstOrDefault().Value;

            ApiClient api = new ApiClient(context.Api.Endpoint, context.Tenant);
            api.Authenticate(externalSystem.Parameters["omniaEmail"].ToString(), externalSystem.Parameters["omniaPassword"].ToString());

            var documentToCopy = api.ProcessInteraction.GetByID("PurchasesManagement", "PurchaseOrder", Convert.ToInt64(document["ID"]));
            var fields = $"CompanyCode:{documentToCopy.CompanyCode}";

            var response = new ScriptResponse();
            response.Actions = new Dictionary<string, object>();
            
            var urlAction = new Dictionary<string, string>();
            urlAction.Add("Location", $"../../ProcessInteraction/Create/?ProcessTypeCode=PurchasesManagement&InteractionTypeCode=PurchaseOrder&Attributes={HttpUtility.UrlEncode(HttpUtility.UrlEncode(fields))}");
            urlAction.Add("Target", "CURRENT");

            response.Actions.Add("URL", urlAction);

            return response;
        }
    }

    public class Entity : Dictionary<string, object>
    {

    }
}