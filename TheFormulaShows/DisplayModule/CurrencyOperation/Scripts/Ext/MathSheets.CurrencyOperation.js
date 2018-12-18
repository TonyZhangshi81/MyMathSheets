
var MathSheets = MathSheets || {};

MathSheets.CurrencyOperation = MathSheets.CurrencyOperation || (function () {

	// 打印設置
	printSetting = function () {
		$("input[id*='inputCo']").each(function (index, element) {
			$(element).addClass('input-print');
			$(element).removeAttr('placeholder');
			$(element).removeAttr("disabled");
		});
	},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputCo']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 答题验证(正确:true  错误:false)
		_currencyOperationCorrecting = function (pIndex, answerElement) {
			var inputAry = new Array();
			var answers = ($(answerElement).val() || '').split(';');

			var isRight = true;
			$.each(answers, function (index, answer) {
				var element = $("input[id *= 'inputCo" + pIndex + "L" + index + "']");
				inputAry.push($(element));

				if (parseInt($(element).val()) != answer) {
					isRight = false;
				}
			});

			// 验证输入值是否与答案一致
			if (isRight) {
				// 动错题集中移除当前项目
				__allFaultInputElementArray.remove({ position: "mathSheetCurrencyOperation", id: $(inputAry[0]).attr("id") });
				// 对错图片显示和隐藏
				$('#imgOKCurrencyOperation' + pIndex).show();
				$('#imgNoCurrencyOperation' + pIndex).hide();
				// 移除圖片抖動特效
				$('#imgNoCurrencyOperation' + pIndex).removeClass("shake shake-slow");

				$.each(inputAry, function (index, inputObj) { inputObj.attr("disabled", "disabled"); });
				// 正确:true
				return true;
			} else {
				// 收集所有錯題項目ID
				__allFaultInputElementArray.push({ position: "mathSheetCurrencyOperation", id: $(inputAry[0]).attr("id") });
				// 对错图片显示和隐藏
				$('#imgOKCurrencyOperation' + pIndex).hide();
				$('#imgNoCurrencyOperation' + pIndex).show();
				$('#imgNoCurrencyOperation' + pIndex).animate({
					width: "40px",
					height: "40px",
					marginLeft: "0px",
					marginTop: "0px"
				}, 1000, function () {
					// 添加圖片抖動特效（只針對錯題）
					$(this).addClass("shake shake-slow");
				});
				// 错误:false
				return false;
			}
		},

		// 设定页面所有输入域为可用状态(貨幣運算)
		ready = function () {
			$("input[id*='inputCo']").each(function (index, element) {
				// 收集所有可輸入項目ID
				__allInputElementArray.push({ position: "mathSheetCurrencyOperation", id: $(element).attr("id") });
				$(element).removeAttr("disabled");
			});
		},

		// 订正(貨幣運算题)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='hidCo']").each(function (pIndex, pElement) {
				var index = pIndex.toString().PadLeft(2, '0');
				// 答题验证
				if (!_currencyOperationCorrecting(index, pElement)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		},

		// 貨幣運算交卷
		theirPapers = function () {
			$("input[id*='hidCo']").each(function (pIndex, pElement) {
				var index = pIndex.toString().PadLeft(2, '0');
				// 答题验证
				if (!_currencyOperationCorrecting(index, pElement)) {
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
