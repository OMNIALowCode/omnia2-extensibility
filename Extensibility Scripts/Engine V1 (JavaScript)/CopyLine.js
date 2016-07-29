/*
Developed by: NumbersBelieve
Function: This script is triggered from a transactional entity line in the details of an Interaction, on user request (for example, from a button). There, it creates a new line, with all the same attributes.
Parameters: [Events.TaskExecution] represents the transactional entity you want to copy. Replace it with your entity.
*/
var triggeredByIndex = Number(parameters[0].split('.')[1].split('[')[1].replace(']', ''));
var triggeredBy = entity.Events.TaskExecution[triggeredByIndex];

var newLine = {UpdateType: "CREATED"};
for(var i in triggeredBy){
	newLine[i] = triggeredBy[i];
	if(i === "Attributes"){
		newLine[i] = {};
		for(var a in triggeredBy[i]){
			newLine[i][a] = triggeredBy[i][a];
		}
	}
}
newLine.RowNumber++;

entity.Events.TaskExecution.splice(triggeredByIndex, 0, newLine);

response.send([{
	StatusCode: "OK",
	StatusMessage: '',
	Result: {
		UpdateEntityProperties: entity
	}
}]);