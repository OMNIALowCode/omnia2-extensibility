/*
Developed by: NumbersBelieve
Function: This script is triggered from anywhere in the UI, and it redirects you to the creation of an interaction, with optional parameters for that creation. The same logic can also be adapted for any other operation other than creating an interaction.
Parameters: [fields] contains attributes we want to pass to the interaction creation, in a FieldName:value,FieldName2:value2 syntax. In the URL [Location:], ProcessTypeCode and InteractionTypeCode should be changed to those you want to use. The relative URL may need to be changed, depending on where the script is triggered.
*/

var fields = 'Project:' + entity.Projeto;

response.send([{
	StatusCode: 'OK',
	StatusMessage: '',
	Result: {
		Actions : 
		{
			'URL': {
				Location: '../ProcessInteraction/Create/?ProcessTypeCode=TaskManagement&InteractionTypeCode=Timesheet&View=Default&Attributes=' + encodeURIComponent(fields),
				Target: 'CURRENT'
			}
		}
	}
}]);