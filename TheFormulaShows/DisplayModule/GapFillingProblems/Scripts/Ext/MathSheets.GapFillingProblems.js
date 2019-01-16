
var MathSheets = MathSheets || {};

MathSheets.GapFillingProblems = MathSheets.GapFillingProblems || (function () {

	// 答题验证(正确:true  错误:false)
	_gapFillingProblemsCorrecting = function (pIndex, pElement) {
		var inputAry = new Array();
		var strId = pIndex.toString().PadLeft(2, '0');
		// 答案數組
		var answerAry = ($(pElement).val() || '').split(',');
		var isOK = true;
		$("input[id*='inputGfp" + strId + "']").each(function (index, element) {
			inputAry.push($(element));
			if ($(element).val() != answerAry[index]) {
				isOK = false;
			}
		});

		// 驗證輸入值是否與答案一致
		if (isOK) {
			// 在錯題集中移除當前項目并設置不可使用
			inputAry.forEach(function (element, index) {
				removeInputElementArray({ position: "mathSheetGapFillingProblems", id: $(element).attr("id") });
				$(element).attr("disabled", "disabled");
			});
			
			// 對錯圖片顯示和影藏
			$('#imgOKGapFillingProblems' + strId).show();
			$('#imgNoGapFillingProblems' + strId).hide();
			// 移除圖片抖動特效
			$('#imgNoGapFillingProblems' + strId).removeClass("shake shake-slow");
			// 正确:true
			return true;
		} else {
			// 对错图片显示和隐藏
			$('#imgOKGapFillingProblems' + strId).hide();
			$('#imgNoGapFillingProblems' + strId).show();
			$('#imgNoGapFillingProblems' + strId).animate({
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
			$("input[id*='inputGfp']").each(function (index, element) {
				$(element).addClass('input-print');
				$(element).removeAttr('placeholder');
				$(element).removeAttr("disabled");
			});
		},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("input[id*='inputGfp']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 设定页面所有输入域为可用状态(基礎填空題)
		ready = function () {
			$("input[id*='inputGfp']").each(function (index, element) {
				// 收集所有可輸入項目ID
				__allInputElementArray.push({ position: "mathSheetGapFillingProblems", id: $(element).attr("id") });
				$(element).removeAttr("disabled");
			});
		},

		// 交卷
		theirPapers = function () {
			$("input[id*='hiddenGfpAnswer']").each(function (pIndex, pElement) {
				// 答题验证
				if (!_gapFillingProblemsCorrecting(pIndex, pElement)) {
					// 答题错误时,错误件数加一
					__isFault++;
				} else {
					__isRight++;
				}
			});
		},

		// 订正(基礎填空題)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='hiddenGfpAnswer']").each(function (pIndex, pElement) {
				// 答题验证
				if (!_gapFillingProblemsCorrecting(pIndex, pElement)) {
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
