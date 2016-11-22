using MyMis.Scripting.Core.Contracts;
using MyMis.Scripting.PlatformApi;
using System.Collections.Generic;
using MyMis.Scripting.PlatformApi.Models;
using System;

/*
Developed by: NumbersBelieve
Function: Queries the platform's API to obtain data on a commitment to fill a commitment in a different interaction.
Parameters: A process called PurchasesManagement, with an interaction called PurchaseOrder which has a commitment of name GoodsPurchaseRequest, with the parameters shown in the queryModel object below.
An external system with ApiUrl, ApiUsername and ApiPassword parameters configured with a valid platform login.
A separate interaction, with a commitment that has an OriginalLineID attribute.
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

            document.MarkToApplyChanges();
            ApiClient api = new ApiClient((string)externalSystem.Parameters["ApiUrl"], context.Tenant.ToString());
            api.Authenticate((string)externalSystem.Parameters["ApiUsername"], (string)externalSystem.Parameters["ApiPassword"]);

            QueryDefinitionModel queryModel = new QueryDefinitionModel();
            queryModel.MisEntityKind = EntityKind.Commitment;
            queryModel.Code = "GoodsPurchaseRequest";

            queryModel.Select = "ID,Amount,Quantity,Resource:Resource.Code,#DeliveryDate,Agent:ProviderAgent.Code";
            queryModel.Where = "Interaction:Interaction.ApprovalStageCode='Completed'&Interaction:Interaction.CompanyCode='" + document.CompanyCode + "'&InteractionNumber=" + document.Attributes.PurchaseOrderSerieNumber.Split('/')[1] + "&InteractionNumberSerie='" + document.Attributes.PurchaseOrderSerieNumber.Split('/')[0] + "'";
            queryModel.QueryContext = QueryContext.DataAccess;
            queryModel.ProcessTypeCode = "PurchasesManagement";
            queryModel.InteractionTypeCode = "PurchaseOrder";
            queryModel.IncludeInactives = false;

            var queryResult = api.Query.Execute(queryModel);

            if (queryResult.NumberOfRecords > 0) {
                int lineNo = 0;
                foreach (var line in queryResult.Data) {
                    
                    Commitment newLine = new Commitment();

                    double quantity, amount;
                    int originalLineID;
                    DateTime scheduledDate;
                    if(Double.TryParse(line["Quantity"].ToString(), out quantity))
                        newLine.Quantity = quantity;
                    if (Double.TryParse(line["Amount"].ToString(), out amount))
                        newLine.Amount = amount;
                    
                    if(line.ContainsKey("DeliveryDate") && line["#DeliveryDate"]!= null && DateTime.TryParse(line["#DeliveryDate"].ToString(), out scheduledDate))
                        newLine.ScheduledDate = scheduledDate;

                    if (Int32.TryParse(line["ID"].ToString(), out originalLineID))
                        newLine.Attributes.OriginalLineID = originalLineID;

                    newLine.Resource = line["Resource:Resource.Code"].ToString();
                    newLine.ProviderAgent = line["Agent:ProviderAgent.Code"].ToString();
                    newLine.ReceiverAgent = document.CompanyCode;
                    newLine.DateOccurred = document.DateCreated;

                    document.Commitments.Commitment.Add(newLine);
                    lineNo++;
                }
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
