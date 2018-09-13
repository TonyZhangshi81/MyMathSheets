﻿
var MathSheets = MathSheets || {};

MathSheets.FindNearestNumber = MathSheets.FindNearestNumber || (function () {

	// 打印設置
	printSetting = function () {
		$("input[id*='inputFnn']").each(function (index, element) {
			$(element).addClass('input-print');
			$(element).removeAttr('placeholder');
			$(element).removeAttr("disabled");
		});
	},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputFnn']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 答题验证(正确:true  错误:false)
		_findNearestNumberCorrecting = function (index, element) {
			// 验证输入值是否与答案一致(并且特殊情况下,答案值可以是任意值,此处以-999代替)
			if ($(element).val() == $('#hiddenFnn' + index).val()
				|| (parseInt($('#hiddenFnn' + index).val()) == -999 && $(element).val() != '')) {
				// 对错图片显示和隐藏
				$('#imgOKFindNearestNumber' + index).show();
				$('#imgNoFindNearestNumber' + index).hide();
				$(element).attr("disabled", "disabled");
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKFindNearestNumber' + index).hide();
				$('#imgNoFindNearestNumber' + index).show();
				// 错误:false
				return false;
			}
		},

		// 设定页面所有输入域为可用状态(找到最近的數字)
		ready = function () {
			$("input[id*='inputFnn']").each(function (index, element) {
				$(element).removeAttr("disabled");
			});
		},

		// 订正(找到最近的數字)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='inputFnn']").each(function (index, element) {
				// 答题验证
				if (!_findNearestNumberCorrecting(index, element)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		},

		// 找到最近的數字交卷
		theirPapers = function () {
			$("input[id*='inputFnn']").each(function (index, element) {
				// 答题验证
				if (!_findNearestNumberCorrecting(index, element)) {
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