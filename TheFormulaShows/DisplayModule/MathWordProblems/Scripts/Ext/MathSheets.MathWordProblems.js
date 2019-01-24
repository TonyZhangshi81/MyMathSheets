
var MathSheets = MathSheets || {};

MathSheets.MathWordProblems = MathSheets.MathWordProblems || (function () {

	// 答题验证(正确:true  错误:false)
	_mathWordProblemsCorrecting = function (strIndex, answerElement) {
		var isRight = false;
		// 計算式
		var inputAnswer = $("#inputMwp" + strIndex + "0").val();
		// 計算式答案
		var answers = ($(answerElement).val() || '').split(';');
		// 計算式比對
		$.each(answers, function (index, answer) {
			if (inputAnswer == $.base64.atob(answer, true)) {
				isRight = true;
				return false;
			}
		});
		// 單位
		var hidUnit = $("#hiddenMwpUnit" + strIndex).val();
		if (hidUnit != '') {
			if ($.base64.atob(hidUnit, true) != $("#inputMwp" + strIndex + "1").val())
				isRight = false;
		}

		// 驗證輸入值是否與答案一致
		if (isRight) {
			// 在錯題集中移除當前項目
			removeInputElementArray({ position: "mathSheetMathWordProblems", id: ('inputMwp' + strIndex + '0') });
			removeInputElementArray({ position: "mathSheetMathWordProblems", id: ('inputMwp' + strIndex + '1') });

			// 對錯圖片顯示和影藏
			$('#imgOKProblems' + strIndex).show();
			$('#imgNoProblems' + strIndex).hide();
			// 移除圖片抖動特效
			$('#imgNoProblems' + strIndex).removeClass("shake shake-slow");
			$("#inputMwp" + strIndex + "0").attr("disabled", "disabled");
			$("#inputMwp" + strIndex + "1").attr("disabled", "disabled");
			// 正确:true
			return true;
		} else {
			// 对错图片显示和隐藏
			$('#imgOKProblems' + strIndex).hide();
			$('#imgNoProblems' + strIndex).show();
			$('#imgNoProblems' + strIndex).animate({
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

		// 打印設置
		printSetting = function () {
			$("input[id*='inputMwp']").each(function (index, element) {
				$(element).addClass('input-print');
				$(element).removeAttr('placeholder');
				$(element).removeAttr("disabled");
			});
		},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputMwp']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 设定页面所有输入域为可用状态(算式应用题)
		ready = function () {
			$("input[id*='inputMwp']").each(function (index, element) {
				// 收集所有可輸入項目ID
				__allInputElementArray.push({ position: "mathSheetMathWordProblems", id: $(element).attr("id") });
				$(element).removeAttr("disabled");
			});
		},

		// 交卷
		theirPapers = function () {
			$("input[id*='hiddenMwpAnswer']").each(function (pIndex, pElement) {
				var strIndex = pIndex.toString().PadLeft(2, '0');
				// 答题验证
				if (!_mathWordProblemsCorrecting(strIndex, pElement)) {
					// 答题错误时,错误件数加一
					__isFault++;
				} else {
					__isRight++;
				}
			});
		},

		// 订正(算式应用题)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='hiddenMwpAnswer']").each(function (pIndex, pElement) {
				var strIndex = pIndex.toString().PadLeft(2, '0');
				// 答题验证
				if (!_mathWordProblemsCorrecting(strIndex, pElement)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		};

	return {
		printSetting: printSetting,
		printAfterSetting: printAfterSetting,
		ready: ready,
		makeCorrections: makeCorrections,
		theirPapers: theirPapers
	};
}());
