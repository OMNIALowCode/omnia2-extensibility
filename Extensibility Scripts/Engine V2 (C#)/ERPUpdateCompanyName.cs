//#R Interop.ErpBS900.dll
//#R Interop.StdBE900.dll
//#R Interop.IGcpBS900.dll
//#R Interop.GcpBE900.dll

using System;
using Interop.ErpBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using Interop.IGcpBS900;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using MyMis.Scripting.Core.Contracts;

/*
Developed by: NumbersBelieve
Function: Obtains some data from the Primavera ERP to be used for a Cloud-based script. Must be used together with CloudUpdateCompanyName.cs.
Parameters: A process called PurchasesManagement, with an interaction called PurchaseOrder which has a commitment of name GoodsPurchaseRequest. This commitment needs to have ERPCode, ERPName and Name Text-type attributes.
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
            ErpBS bsERP = new ErpBS();
            var externalSystem = context.ExternalSystems.FirstOrDefault().Value;
            try
            {
                if (!externalSystem.Parameters.ContainsKey("TipoPlataforma"))
                {
                    throw new Exception("TipoPlataforma invalido");
                }

                EnumTipoPlataforma tipoPlataforma;
                if (!Enum.TryParse<EnumTipoPlataforma>((string)externalSystem.Parameters["TipoPlataforma"], out tipoPlataforma))
                {
                    throw new Exception("TipoPlataforma invalido");
                }

                try
                {
                    bsERP.AbreEmpresaTrabalho(tipoPlataforma, (string)externalSystem.Parameters["Company"], (string)externalSystem.Parameters["Username"], (string)externalSystem.Parameters["Password"]);
                }
                catch (Exception e)
                {
                    throw new Exception("Erro a abrir a empresa no ERP: " + e.Message);
                }
                
                string str = externalSystem.Parameters["0"].ToString();
                int first = str.IndexOf("Commitments.GoodsPurchaseRequest[") + "Commitments.GoodsPurchaseRequest[".Length;
                int last = str.LastIndexOf("].ERPCode");
                int commIndex = int.Parse(str.Substring(first, last - first));

                StdBELista queryResults = bsERP.Consulta($"SELECT Nome,Pais FROM Fornecedores WHERE Fornecedor='{document.Commitments.GoodsPurchaseRequest[commIndex].Attributes.ERPCode}'");

                int numLinhas = queryResults.NumLinhas();
                int numColunas = queryResults.NumColunas();
                
                string[] headers = new string[numColunas];
                for (short i = 0; i < numColunas; i++)
                {
                    headers[i] = queryResults.Nome(i);
                }

                object[,] data = new object[numLinhas, numColunas];
                for (short i = 0; i < numLinhas; i++)
                {
                    for (short j = 0; j < numColunas; j++)
                    {
                        var nome = headers[j];
                        data[i, j] = queryResults.Valor(nome);
                    }
                    queryResults.Seguinte();
                }

                QueryResult response = new QueryResult()
                {
                    Headers = headers,
                    Data = data,
                    NumberOfRecords = numLinhas
                };
                
                bsERP.FechaEmpresaTrabalho();

                return new ScriptResponse
                {
                    Result = response
                };

            }
            catch (Exception ex)
            {
                bsERP.FechaEmpresaTrabalho();

                throw ex;

            }
        }
    }
}