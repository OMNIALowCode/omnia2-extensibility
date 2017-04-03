/*
Developed by: NumbersBelieve
Function: Embeds a progress bar in the UI, that changes shape depending on a value in the model.
Parameters: An attribute named OrderStatus of type Value Pair, where its values are "0", "1", and "2".
*/

WebComponents.ProgressBar = function () {

	this.html = function () {
		return '<div class="progress mt-2"> 																																																\
					<div data-step="1" class="progress-bar progress-bar-animated progress-bar-striped hidden-xs-up" role="progressbar" style="width: 33%" aria-valuenow="33" aria-valuemin="0" aria-valuemax="100"></div> 					\
					<div data-step="2" class="progress-bar progress-bar-animated progress-bar-striped hidden-xs-up bg-info" role="progressbar" style="width: 33%" aria-valuenow="33" aria-valuemin="0" aria-valuemax="100"></div> 			\
					<div data-step="3" class="progress-bar progress-bar-animated progress-bar-striped hidden-xs-up bg-success" role="progressbar" style="width: 34%" aria-valuenow="34" aria-valuemin="0" aria-valuemax="100"></div>		\
				</div>																																																						\
				<p class="text-center lead"></p>																																																						\
				';
	};


	this.initialize = function (domElement) {
		calculateProgressBar(document.getElementById('OrderStatus'), domElement);

		document.getElementById('OrderStatus').addEventListener('change', function (event) {
			calculateProgressBar(event.target, domElement);
		});
	};

	function calculateProgressBar(selectElement, domElement) {
		var bars = domElement.querySelectorAll('div.progress-bar');

		// hide all
		for (var index = 0; index < bars.length; index++) {
			var element = bars[index];
			element.classList.add('hidden-xs-up');
		}

		var selectedValue = selectElement.value;
		var icon = null;
		// show only the current visible bars
		switch (selectedValue.toUpperCase()) {
			case "2":
				var bar = bars[2];
				bar && bar.classList.remove('hidden-xs-up');
				if (icon == null) icon = 'fa fa-truck';
				case "1":
				var bar = bars[1];
				bar && bar.classList.remove('hidden-xs-up');
				if (icon == null) icon = 'fa fa-archive';
			case "0":
				var bar = bars[0];
				bar && bar.classList.remove('hidden-xs-up');
				if (icon == null) icon = 'fa fa-cog';
				break;
		}

		domElement.querySelector('p').innerHTML = '<i class="' + icon + '"></i>&nbsp;' + selectElement.options[selectElement.selectedIndex].text;
	}
}
