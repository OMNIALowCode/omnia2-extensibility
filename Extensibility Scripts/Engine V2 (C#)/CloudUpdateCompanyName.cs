using System;
using System.Collections.Generic;
using MyMis.Scripting.Core.Contracts;
using System.Linq;
using MyMis.Scripting.ConnectorClient;

/*
Developed by: NumbersBelieve
Function: Calls the connector to get some data from the Primavera ERP, processes that data and updates the interaction. Must be used together with ERPUpdateCompanyName.cs.
Parameters: A process called PurchasesManagement, with an interaction called PurchaseOrder which has a commitment of name GoodsPurchaseRequest. This commitment needs to have ERPCode, ERPName and Name Text-type attributes.
*/
namespace myMIS
{
    public class Script
    {
        public ScriptResponse Execute(ContextData context, Entity document, Dictionary<string, object> parameters)
        {
            try
            {


                /* **************************************** */
                /* **************************************** */
                /*          ADD YOUR CODE HERE              */

                document.MarkToApplyChanges();
                var externalSystem = context.ExternalSystems.FirstOrDefault().Value;

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
				
                string str = externalSystem.Parameters["0"].ToString();
                int first = str.IndexOf("Commitments.GoodsPurchaseRequest[") + "Commitments.GoodsPurchaseRequest[".Length;
                int last = str.LastIndexOf("].ERPCode");
                int commIndex = int.Parse(str.Substring(first, last - first));
				
                var connectorResult = connectorResponse.Result.FirstOrDefault().Result;
                DadosFornecedores fornec = new DadosFornecedores((string)connectorResult.Data[0, 0], (string)connectorResult.Data[0, 1]);
                
                var nome = fornec.Nome;
                document.Commitments.GoodsPurchaseRequest[commIndex].Attributes.ERPName = nome;
                if (fornec.Pais == "PT")
                {
                    nome = fornec.Nome += (" (Local)");
                }
                if (fornec.Pais != "PT")
                {
                    nome = fornec.Nome += (" (Estrangeiro)");
                }

                document.Commitments.GoodsPurchaseRequest[commIndex].Attributes.Name = nome;
                return new ScriptResponse();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            /* **************************************** */
            /* **************************************** */
        }
    }
    public class DadosFornecedores
    {
        public DadosFornecedores(string Nome, string Pais)
        {
            this.Pais = Pais;
            this.Nome = Nome;
        }

        public string Pais { get; set; }
        public string Nome { get; set; }
    }
}