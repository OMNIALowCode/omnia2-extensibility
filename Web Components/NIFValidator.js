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
		return '';
	};
	
	// Optional method: Called after the html is added to the page
	// The right place to add custom behaviours
	this.initialize = function (domElement) {
		var nifField = document.getElementById("NIF");
		var initialValue = nifField.value || '';
		
		var errorMessageSpan = document.createElement('span')
		errorMessageSpan.innerHTML = '<span class="form-control-feedback" id="NIF_Error">NIF is invalid!</span>';
		
		var successMessageSpan = document.createElement('span')
		successMessageSpan.innerHTML = '<span class="form-control-feedback" id="NIF_Error">NIF is valid</span>';
		
		var input = document.getElementById('NIF');
		
		input.onchange = function(event){
			var input = document.getElementById('NIF');
			var nif = input.value || '';
			
			if (isValidNIF(nif)){
				document.getElementById("NIF").parentNode.parentNode.classList.remove("has-danger");
				if(errorMessageSpan.parentNode !== null){
					document.getElementById("NIF").parentNode.parentNode.removeChild(errorMessageSpan);	
				}
				
				document.getElementById("NIF").parentNode.parentNode.appendChild(successMessageSpan);
				
				var nifField = document.getElementById("NIF");
				nifField.value = nif;
				nifField.dispatchEvent(new Event('change'));
			}
			else{					
				document.getElementById("NIF").parentNode.parentNode.classList.add("has-danger");
				if(successMessageSpan.parentNode !== null){
					document.getElementById("NIF").parentNode.parentNode.removeChild(successMessageSpan);	
				}
				document.getElementById("NIF").parentNode.parentNode.appendChild(errorMessageSpan);
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
