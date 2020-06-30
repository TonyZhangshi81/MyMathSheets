var MathSheets = MathSheets || {};

MathSheets.FindTheLaw = MathSheets.FindTheLaw || (function () {
	// 打印設置
	printSetting = function () {
		$("input[id*='inputFtl']").each(function (index, element) {
			$(element).addClass('input-print');
			$(element).removeAttr('placeholder');
			$(element).removeAttr("disabled");
		});
	},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputFtl']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 答题验证(正确:true  错误:false)
		_FindTheLawCorrecting = function (pindex, pelement) {
			var inputAry = new Array();
			var right = true;
			// 标准答案列表
			var answerAry = ($(pelement).val() || "").split(';');
			// 验证输入值是否与答案一致
			$("input[id*='inputFtl" + pindex + "']").each(function (index, element) {
				inputAry.push($(element));
				answer = ($(element).val() == '') ? 0 : parseInt($(element).val());
				if (parseInt($.base64.atob(answerAry[index], true)) != answer) {
					right = false;
				}
			});
			// 如果不存在错误
			if (right) {
				// 对错图片显示和隐藏
				$('#imgOKFindTheLaw' + pindex).show();
				$('#imgNoFindTheLaw' + pindex).hide();
				// 移除圖片抖動特效
				$('#imgNoFindTheLaw' + pindex).removeClass("shake shake-slow");
				$.each(inputAry, function (index, inputObj) {
					// 控件只讀屬性設置
					inputObj.attr("disabled", "disabled");
					// 在錯題集中移除當前項目
					removeInputElementArray({ position: "mathSheetFindTheLaw", id: inputObj.attr("id") });
				});
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKFindTheLaw' + pindex).hide();
				$('#imgNoFindTheLaw' + pindex).show();
				$('#imgNoFindTheLaw' + pindex).animate({
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

		// 设定页面所有输入域为可用状态(找規律)
		ready = function () {
			$("input[id*='inputFtl']").each(function (index, element) {
				// 收集所有可輸入項目ID
				__allInputElementArray.push({ position: "mathSheetFindTheLaw", id: $(element).attr("id") });
				$(element).removeAttr("disabled");
			});
		},

		// 订正(找規律)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='hiddenFtl']").each(function (pindex, pelement) {
				// 答题验证
				if (!_FindTheLawCorrecting(pindex, pelement)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		},

		// 找規律交卷
		theirPapers = function () {
			$("input[id*='hiddenFtl']").each(function (pindex, pelement) {
				// 答题验证
				if (!_FindTheLawCorrecting(pindex, pelement)) {
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