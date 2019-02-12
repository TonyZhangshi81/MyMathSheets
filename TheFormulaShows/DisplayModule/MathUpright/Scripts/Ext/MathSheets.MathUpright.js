
var MathSheets = MathSheets || {};

MathSheets.MathUpright = MathSheets.MathUpright || (function () {

	// 打印設置
	printSetting = function () {
		$("input[id*='inputMu']").each(function (index, element) {
			$(element).addClass('input-print');
			$(element).removeAttr('placeholder');
			$(element).removeAttr("disabled");
		});
	},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputMu']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 答题验证(正确:true  错误:false)
		_MathUprightCorrecting = function (index, element) {
			// 解密
			var answer = $.base64.atob($('#hiddenMu' + index).val(), true);
			// 验证输入值是否与答案一致(并且特殊情况下,答案值可以是任意值,此处以-999代替)
			if ($(element).val() == answer
				|| (parseInt(answer) == -999 && $(element).val() != '')) {
				// 动错题集中移除当前项目
				removeInputElementArray({ position: "mathSheetMathUpright", id: $(element).attr("id") });

				// 对错图片显示和隐藏
				$('#imgOKMathUpright' + index).show();
				$('#imgNoMathUpright' + index).hide();
				// 移除圖片抖動特效
				$('#imgNoMathUpright' + index).removeClass("shake shake-slow");
				$(element).attr("disabled", "disabled");
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKMathUpright' + index).hide();
				$('#imgNoMathUpright' + index).show();
				$('#imgNoMathUpright' + index).animate({
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

		// 设定页面所有输入域为可用状态(四则运算)
		ready = function () {
			$("input[id*='inputMu']").each(function (index, element) {
				// 收集所有可輸入項目ID
				__allInputElementArray.push({ position: "mathSheetMathUpright", id: $(element).attr("id") });

				$(element).removeAttr("disabled");
			});
		},

		// 订正(四则运算题)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='inputMu']").each(function (index, element) {
				// 答题验证
				if (!_MathUprightCorrecting(index.toString().PadLeft(2, '0'), element)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		},

		// 四则运算交卷
		theirPapers = function () {
			$("input[id*='inputMu']").each(function (index, element) {
				// 答题验证
				if (!_MathUprightCorrecting(index.toString().PadLeft(2, '0'), element)) {
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
