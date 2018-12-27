
var MathSheets = MathSheets || {};

MathSheets.TimeCalculation = MathSheets.TimeCalculation || (function () {

	// 打印設置
	printSetting = function () {
		$("input[id*='inputTc']").addClass('input-print').removeAttr('placeholder').removeAttr("disabled");
	},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputTc']").removeClass('input-print').attr('placeholder', '??').attr("disabled", "disabled");
		},

		// 答题验证(正确:true  错误:false)
		_timeCalculationCorrecting = function (pIndex, pElement) {
			var fault = true;
			// 答案
			var answers = ($(pElement).val() || "").split(':');
			// 填空項目
			var $inputs = $("input[id*='inputTc" + pIndex.toString().PadLeft(2, '0') + "']");
			$inputs.each(function (index, element) {
				// 验证输入值是否与答案一致
				if (parseInt($(element).val()) != parseInt(answers[index])) {
					fault = false;
				}
			});

			// 验证输入值是否与答案一致
			if (fault) {
				// 动错题集中移除当前项目
				__allFaultInputElementArray.remove({ position: "mathSheetTimeCalculation", id: $inputs.eq(0).attr("id") });

				// 对错图片显示和隐藏
				$('#imgOKTimeCalculation' + pIndex).show();
				$('#imgNoTimeCalculation' + pIndex).hide();
				// 移除圖片抖動特效
				$('#imgNoTimeCalculation' + pIndex).removeClass("shake shake-slow");
				$inputs.attr("disabled", "disabled");
				// 正确:true
				return true;
			} else {
				// 收集所有錯題項目ID
				__allFaultInputElementArray.push({ position: "mathSheetTimeCalculation", id: $inputs.eq(0).attr("id") });

				// 对错图片显示和隐藏
				$('#imgOKTimeCalculation' + pIndex).hide();
				$('#imgNoTimeCalculation' + pIndex).show();
				$('#imgNoTimeCalculation' + pIndex).animate({
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

		// 设定页面所有输入域为可用状态(時間運算)
		ready = function () {
			$("input[id*='inputTc']").each(function (index, element) {
				// 收集所有可輸入項目ID
				__allInputElementArray.push({ position: "mathSheetTimeCalculation", id: $(element).attr("id") });

				$(element).removeAttr("disabled");
				// 亮燈
				$("span[class='span-tc-lightoff']").removeClass("span-tc-lightoff").addClass("span-tc-lighton");
			});
		},

		// 订正(時間運算题)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='hiddenTc']").each(function (pIndex, pElement) {
				// 答题验证
				if (!_timeCalculationCorrecting(pIndex, pElement)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		},

		// 當光標落在文本輸入框中的時候發生的事件
		inputOnFocus = function (e) {
			var value = $(e).val();
			// 如果當前輸入框的內容是空
			if (value == "") {
				return;
			}
			// 刪除數字前的"0"
			$(e).val(parseInt(value));
		},

		// 當光標失去焦點的時候發生的事件
		inputOnBlur = function (e) {
			var value = $(e).val();
			// 如果當前輸入框的內容是空
			if (value == "") {
				return;
			}
			// 在數字前填充"0"
			$(e).val(value.PadLeft(2, '0'));
		},

		// 時間運算交卷
		theirPapers = function () {
			$("input[id*='hiddenTc']").each(function (pIndex, pElement) {
				// 答题验证
				if (!_timeCalculationCorrecting(pIndex, pElement)) {
					// 答题错误时,错误件数加一
					__isFault++;
				} else {
					__isRight++;
				}
			});
			// 關燈
			$("span[class='span-tc-lighton']").removeClass("span-tc-lighton").addClass("span-tc-lightoff");
		};

	return {
		printSetting: printSetting,
		printAfterSetting: printAfterSetting,
		ready: ready,
		makeCorrections: makeCorrections,
		inputOnFocus: inputOnFocus,
		inputOnBlur: inputOnBlur,
		theirPapers: theirPapers
	};
}());
