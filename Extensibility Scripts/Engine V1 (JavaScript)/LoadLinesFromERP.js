/*
Developed by: NumbersBelieve
Function: This script is used to create in the platform a set of lines with data returned from the ERP.
Parameters: The string in query should be replaced with the query you wish to perform. entity.Events.EventCode should be replaced with the interaction/event or commitment you want to fill.
*/
var connector = new mymis.connector(requestID);

connector.onConnectionError = function(err) {
    response.status(500).send([{
        StatusCode: "ScriptException",
        StatusMessage: (err instanceof Error) ? err.toString() : JSON.stringify(err),
        Result: ""
    }]);
};

connector.start(function() {
    var context = mymis.helpers.getContextToCompany(contextData, entity.CompanyCode);

    var mymis_services = connector.initConnectorServices();

	var query = "SELECT * FROM TABLE";

    var scriptName = "";
    var scriptVersion = "";
    if (contextData.Files && contextData.Files.length > 0) {
        for (var x = 0; x < contextData.Files.length; x++) {
            if (contextData.Files[x].ExecutionType === "Remote") {
                scriptName = contextData.Files[x].Name;
                scriptVersion = contextData.Files[x].Version;
                break;
            }
        }
    }

    if (scriptName == "") {
        var statusMsg = ('Remote script file not found.');
        response.send([{
            StatusCode: "ScriptException",
            StatusMessage: statusMsg,
            Result: ""
        }]);
    } else {
        mymis_services.query(context, query, {
            ScriptName: scriptName,
            ScriptVersion: scriptVersion
        }, function(resp) {
            if (!mymis.helpers.isValidConnectorResponse(resp, response)) {
                connector.close();
            } else {
                if (resp && resp[0] && resp[0].StatusCode === 'OK' && resp[0].Result && resp[0].Result.Data) {
					var result = resp[0].Result.Data;
					
                    if (entity.Events && entity.Events.EventCode) {
						for (var i in entity.Events.EventCode) {
							entity.Events.EventCode[i].UpdateType = "DELETED";
						}
                    } else {
                        entity.Events.EventCode = [];
                    }

                    for (var i in result) {
                        entity.Events.EventCode.push({
                            "Quantity": 1,
							"Resource": result[i][0],
							"Amount": result[i][1]
                            "Attributes": {
                                "Attribute": result[i][2],
                            },
                            UpdateType: "CREATED",
                            RowNumber: Number(i) + 1
                        });
                    }

					entity.Attributes = {};

                    response.send([{
                        StatusCode: "OK",
                        StatusMessage: '',
                        Result: {
                            UpdateEntityProperties: entity
                        }
                    }]);

                    connector.close();
                } else {
                    response.send([{
                        StatusCode: resp[0].StatusCode,
                        StatusMessage: resp[0].StatusMessage,
                        Result: {}
                    }]);
                    connector.close();
                }
            }
        });
    }
});