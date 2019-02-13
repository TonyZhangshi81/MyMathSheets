
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

		// 答題驗證(正確:true  錯誤:false)
		_MathUprightCorrecting = function (pIndex, pElement) {
			// 解密
			//var answer = $.base64.atob($('#hiddenMu' + index).val(), true);
			var inputAry = new Array();
			var strId = pIndex.toString().PadLeft(2, '0');
			// 答案數組
			var answerAry = ($(pElement).val() || '').split(';');
			var isOK = true;
			$("input[id*='inputMu" + strId + "']").each(function (index, element) {
				inputAry.push($(element));
				var answer = $.base64.atob(answerAry[index], true);
				if ($(element).val() != answer) {
					isOK = false;
				}
			});

			// 驗證輸入值是否與答案一致
			if (isOK) {
				// 在錯題集中移除當前項目并設置不可使用
				inputAry.forEach(function (element, index) {
					removeInputElementArray({ position: "mathSheetMathUpright", id: $(element).attr("id") });
					$(element).attr("disabled", "disabled");
				});

				// 對錯圖示顯示和隱藏
				$('#imgOKMathUpright' + index).show();
				$('#imgNoMathUpright' + index).hide();
				// 移除圖片抖動特效
				$('#imgNoMathUpright' + index).removeClass("shake shake-slow");
				$(element).attr("disabled", "disabled");
				// 正確:true
				return true;
			} else {
				// 對錯圖示顯示和隱藏
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
				// 錯誤:false
				return false;
			}
		},

		// 設定頁面所有輸入域可用狀態(豎式計算)
		ready = function () {
			$("input[id*='inputMu']").each(function (index, element) {
				// 收集所有可輸入項目ID
				__allInputElementArray.push({ position: "mathSheetMathUpright", id: $(element).attr("id") });

				$(element).removeAttr("disabled");
			});
		},

		// 訂正(豎式計算題)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='hiddenMuAnswer']").each(function (pIndex, pElement) {
				// 答題驗證
				if (!_MathUprightCorrecting(pIndex, pElement)) {
					// 答題錯誤時，錯題件數加一
					fault++;
				}
			});
			return fault;
		},

		// 豎式計算交卷
		theirPapers = function () {
			$("input[id*='inputMuAnswer']").each(function (pIndex, pElement) {
				// 答題驗證
				if (!_MathUprightCorrecting(pIndex, pElement)) {
					// 答題錯誤時，錯題件數加一
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
