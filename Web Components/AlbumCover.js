/*
Developed by: NumbersBelieve
Function: In a list, display an image from a URL that is in one of the list's columns.
Parameters: N/A
*/

WebComponents.AlbumCover = function () {

	this.html = function () {
		return '<img style="height: 80px;" class="mx-auto d-block" src="" />';
	};


	this.initialize = function (domElement) {
		var imgElement = domElement.getElementsByTagName('img')[0];
		imgElement.src = domElement.parentElement.parentElement.querySelectorAll('td')[3].innerText;
	};
}
