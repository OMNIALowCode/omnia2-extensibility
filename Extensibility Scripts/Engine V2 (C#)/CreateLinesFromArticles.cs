using MyMis.Scripting.Core.Contracts;
using System.Collections.Generic;


/*
Developed by: NumbersBelieve
Function: Obtains a series of articles from an attribute in the header, and creates a line for each of them.
Parameters: An interaction with a header attribute named InsertItem, of type Other Entity, based off an External Entity, and with cardinality N.
  The interaction should have an event in the Details, named PurchaseLine.
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

            document.MarkToApplyChanges();

            string[] Artigos = document.Attributes.InsertItem.Split(',');
            foreach (string Item in Artigos)
            {
                document.Events.PurchaseLine.Add( new PurchaseLine() );

				        document.Events.PurchaseLine.Last().Attributes.ItemCode = Item;
            }


            /* **************************************** */
            /* **************************************** */

            return new ScriptResponse
            {
                Message = ""
            };
        }
    }
}
