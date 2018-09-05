
var MathSheets = MathSheets || {};

MathSheets.Arithmetic = MathSheets.Arithmetic || (function () {
	
	// 打印設置
	printSetting = function () {
		$("input[id*='inputAc']").each(function (index, element) {
			$(element).addClass('input-print');
			$(element).removeAttr('placeholder');
			$(element).removeAttr("disabled");
		});
	},
	
	// 答题验证(正确:true  错误:false)
	_arithmeticCorrecting = function (index, element) {
		// 验证输入值是否与答案一致(并且特殊情况下,答案值可以是任意值,此处以-999代替)
		if ($(element).val() == $('#hiddenAc' + index).val()
			|| (parseInt($('#hiddenAc' + index).val()) == -999 && $(element).val() != '')) {
			// 对错图片显示和隐藏
			$('#imgArithmeticOK' + index).show();
			$('#imgArithmeticNo' + index).hide();
			$(element).attr("disabled", "disabled");
			// 正确:true
			return true;
		} else {
			// 对错图片显示和隐藏
			$('#imgArithmeticOK' + index).hide();
			$('#imgArithmeticNo' + index).show();
			// 错误:false
			return false;
		}
	},

	// 设定页面所有输入域为可用状态(四则运算)
	ready = function () {
		$("input[id*='inputAc']").each(function (index, element) {
			$(element).removeAttr("disabled");
		});
	},

	// 订正(四则运算题)
	makeCorrections = function () {
		var fault = 0;
		$("input[id*='inputAc']").each(function (index, element) {
			// 答题验证
			if (!_arithmeticCorrecting(index, element)) {
				// 答题错误时,错误件数加一
				fault++;
			}
		});
		return fault;
	},

	// 四则运算交卷
	theirPapers = function () {
		$("input[id*='inputAc']").each(function (index, element) {
			// 答题验证
			if (!_arithmeticCorrecting(index, element)) {
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
