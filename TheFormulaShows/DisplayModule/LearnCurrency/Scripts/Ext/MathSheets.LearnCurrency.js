var MathSheets = MathSheets || {};

MathSheets.LearnCurrency = MathSheets.LearnCurrency || (function () {

	// 打印設置
	printSetting = function () {
		$("input[id*='inputLc']").each(function (index, element) {
			$(element).addClass('input-print');
			$(element).removeAttr('placeholder');
			$(element).removeAttr("disabled");
		});
	},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputLc']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 答题验证(正确:true  错误:false)
		_learnCurrencyCorrecting = function (pindex, pelement) {
			var inputAry = new Array();
			// 答案數組
			var answerAry = ($(pelement).val() || '').split(',');
			var isOK = true;
			$("input[id*='inputLc" + pindex + "S']").each(function (index, element) {
				inputAry.push($(element));
				if (parseInt($(element).val()) != parseInt(answerAry[index])) {
					isOK = false;
				}
			});

			// 验证输入值是否与答案一致
			if (isOK) {
				// 对错图片显示和隐藏
				$('#imgOKLearnCurrency' + pindex).show();
				$('#imgNoLearnCurrency' + pindex).hide();
				$.each(inputAry, function (index, inputObj) { inputObj.attr("disabled", "disabled"); });
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKLearnCurrency' + pindex).hide();
				$('#imgNoLearnCurrency' + pindex).show();
				// 错误:false
				return false;
			}
		},

		// 设定页面所有输入域为可用状态(認識貨幣)
		ready = function () {
			$("input[id*='inputLc']").each(function (index, element) {
				$(element).removeAttr("disabled");
			});
		},

		// 订正(認識貨幣題)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='hidLcAnswer']").each(function (pindex, pelement) {
				// 答题验证
				if (!_learnCurrencyCorrecting(pindex, pelement)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		},

		// 認識貨幣交卷
		theirPapers = function () {
			$("input[id*='hidLcAnswer']").each(function (pindex, pelement) {
				// 答题验证
				if (!_learnCurrencyCorrecting(pindex, pelement)) {
					// 答题错误时,错误件数加一
					__isFault++;
				} else {
					__isRight++;
				}
			});
		};

	return {
		printSetting: printSetting,
		printAfterSetting: printAfterSetting,
		ready: ready,
		makeCorrections: makeCorrections,
		theirPapers: theirPapers
	};
}());
