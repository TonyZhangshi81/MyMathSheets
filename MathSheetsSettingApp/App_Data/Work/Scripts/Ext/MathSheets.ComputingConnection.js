
var MathSheets = MathSheets || {};

MathSheets.ComputingConnection = MathSheets.ComputingConnection || (function () {
	
	// 答题验证(正确:true  错误:false)
	_computingConnectionCorrecting = function (pIndex, element) {
		var isErr = false;
		var inputCcArray = new Array();
		$("input[id*='inputCc" + pIndex + "']").each(function (index, element) {
			// 验证输入值是否与答案一致
			if ($(element).val() != $('#hiddenCc' + pIndex + index).val()) {
				isErr = true;
			}
			inputCcArray.push($(element));
		});

		// 不存在错误的情况下
		if (!isErr) {
			// 对错图片显示和隐藏
			$('#imgComputingConnectionOK' + pIndex).show();
			$('#imgComputingConnectionNo' + pIndex).hide();

			inputCcArray.forEach(function (element, index) {
				$(element).attr("disabled", "disabled");
			});
			// 正确:true
			return true;
		} else {
			// 对错图片显示和隐藏
			$('#imgComputingConnectionOK' + pIndex).hide();
			$('#imgComputingConnectionNo' + pIndex).show();
			// 错误:false
			return false;
		}
	},
	
	// 打印設置
	printSetting = function () {
		$("input[id*='inputCc']").each(function (index, element) {
			$(element).removeAttr('placeholder');
			$(element).removeAttr("disabled");
		});
	},

	// 设定页面所有输入域为可用状态(等式接龙)
	ready = function () {
		$("input[id*='inputCc']").each(function (index, element) {
			$(element).removeAttr("disabled");
		});
	},

	// 订正(等式接龙)
	makeCorrections = function () {
		var fault = 0;
		$("input[id*='hiddenAllCc']").each(function (pIndex, element) {
			// 层数获取
			var allCc = parseInt($(element).val());
			// 答题验证
			if (!_computingConnectionCorrecting(pIndex, element)) {
				// 答题错误时,错误件数加一
				fault++;
			}
		});
		return fault;
	},

	// 等式接龙交卷
	theirPapers = function () {
		$("input[id*='hiddenAllCc']").each(function (pIndex, element) {
			// 答题验证
			if (!_computingConnectionCorrecting(pIndex, element)) {
				// 答题错误时,错误件数加一
				__isFault++;
			} else {
				__isRight++;
			}
		});
	};
	
return {
		printSetting : printSetting,
		ready : ready,
		makeCorrections : makeCorrections,
		theirPapers : theirPapers
	};
}());