//#R System.Web.dll
using MyMis.Scripting.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Web;

/*
Developed by: NumbersBelieve
Function: Redirects to the page for creating an entity of type Person, passing it a parameter from the current entity.
Parameters: An entity or interaction with a text field PersonCode, with an action to execute this script (for example, a Button); A Person entity of type Agent. 
*/
namespace myMIS
{
    public class Script
    {
        public ScriptResponse Execute(ContextData context, Entity document, Dictionary<string, object> parameters)
        {
            var fields = $"Code:{document.PersonCode}";

            var response = new ScriptResponse();
            response.Actions = new Dictionary<string, object>();

            var urlAction = new Dictionary<string, string>();
            urlAction.Add("Location", $"../../Entity/Create/?EntityTypeCode=Person&MisEntityKind=Agent&View=Default&Attributes={HttpUtility.UrlEncode(HttpUtility.UrlEncode(fields))}");
            urlAction.Add("Target", "CURRENT");

            response.Actions.Add("URL", urlAction);

            return response;
        }
    }

    public class Entity
    {
        public string PersonCode { get; set; }

    }





}