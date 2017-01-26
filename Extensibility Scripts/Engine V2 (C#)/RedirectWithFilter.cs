using MyMis.Scripting.Core.Contracts;
using System;
using System.Collections.Generic;

/*
Developed by: NumbersBelieve
Function: Script that is executed in the context of a list, and redirects the user to a list from another Entity
Parameters: List based off a non-external entity, with only Code and Name .
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

            
			string filter = "Filter=ProjectCode$PROJ_1"; 
			// The following replace is needed to encode the '=' symbol
			filter = filter.Replace("=", "%3D");
			
            var url = $"../Index/?EntityTypeCode=SqlTask&MisEntityKind=UserDefinedEntity&IsExternal=true&Parameters=" + filter;
			
			Dictionary<string, object> actions = new Dictionary<string, object>();
            
            var urlAction = new Dictionary<string, string>();
            urlAction.Add("Location", url);

            //Target is set as current so that the new page is opened in the same tab. If not indicated, the URL will be opened on a new browser tab
            urlAction.Add("Target", "CURRENT");


            actions.Add("URL", urlAction);
            

            /* **************************************** */
            /* **************************************** */

            return new ScriptResponse
            {
                Actions = actions
            };
        }
    }

    //List columns are represented as a dictionary
    public class Entity : Dictionary<string, object>
    {
    }

}