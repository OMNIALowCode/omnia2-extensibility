/*
Developed by: NumbersBelieve
Function: NIF validation and display in a row.
Parameters:
	The interaction or entity must have an NIF attribute, of text type.
	The NIF field should be hidden and required.
*/

// Define a new JS class
WebComponents.NIFValidator = function(){
	
	// Required method: Used to return the Component's HTML
	this.html = function(){
		return ' 																				\
						<input type="text" class="form-control" id="NIF_Value"/>				\
						<span class="form-control-feedback" id="NIF_Error"></span> 				\
		';
	};
	
	// Optional method: Called after the html is added to the page
	// The right place to add custom behaviours
	this.initialize = function (domElement) {
		var nifField = document.getElementById("NIF");
		var initialValue = nifField.value || '';
		
		var input = document.getElementById('NIF_Value');
			
		input.onchange = function(event){
			var input = document.getElementById('NIF_Value');
			var nif = input.value || '';
			
			if (isValidNIF(nif)){
				document.getElementById("NIF_Value").parentNode.parentNode.classList.remove("has-danger");
				document.getElementById("NIF_Error").innerHTML = "";
				
				var nifField = document.getElementById("NIF");
				nifField.value = nif;
				nifField.dispatchEvent(new Event('change'));
			}
			else{					
				document.getElementById("NIF_Value").parentNode.parentNode.classList.add("has-danger");
				document.getElementById("NIF_Error").innerHTML = "NIF is invalid!";
				
				var nifField = document.getElementById("NIF");
				nifField.value = ""; //do not allow saving
				nifField.dispatchEvent(new Event('change'));
			}
		};
	};
	
	//NIF validation function adapted from http://rtenreirosa.blogspot.pt/2012/06/validar-nif-com-javascript.html
	
	function isValidNIF(nif) {
		if(nif != null && nif.length == 9){
			c = nif.charAt(0);
			if(c == '1' || c == '2' || c == '5' || c == '6' || c == '8' || c == '9'){
				checkDigit = c * 9;
				for(i = 2; i <= 8; i++){
						checkDigit += nif.charAt(i-1) * (10-i);
				}
				checkDigit = 11 - (checkDigit % 11);
				if(checkDigit >= 10){
						checkDigit = 0;
				}
				if(checkDigit == nif.charAt(8)){
						return true;
				}
			}
		}
		return false;

	};
}