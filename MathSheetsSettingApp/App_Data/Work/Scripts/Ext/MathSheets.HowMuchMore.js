
var MathSheets = MathSheets || {};
MathSheets.HowMuchMore = MathSheets.HowMuchMore || (function () {

	// 打印設置
	printSetting = function () {

		$("input[id*='hidHmmAnswer']").each(function (parentIndex, pElement) {
			// 頁面圖片按鍵轉空白邊框
			$("img[id*='imgHmmHelp").each(function (index, element) {
				$(element).replaceWith("<button id=\"btnHmm" + parentIndex + index + "\" type=\"button\" class=\"btn btn-default btn-square button-addBorder\"></button>");
			});
		});
	},

	// 打印后頁面設定
	printAfterSetting = function () {

		$("input[id*='hidHmmAnswer']").each(function (parentIndex, pElement) {
			// 頁面按鍵項目復原
			$("button[id*='btnHmm").each(function (index, element) {
				$(element).replaceWith("<img src=\"../Content/image/help.png\" id=\"imgHmmHelp" + parentIndex + index + "\" style=\"width: 30px; height: 30px; \" title=\"help\" />");
			});
		});
	},

	// 圖片点击切换
	_imgHmmHelpClick = function (element, imgHmmHelp) {

		if ($(element).attr("disabled") == 'disabled') {
			return;
		}
		if ($(element).attr("title") == 'help') {
			$(element).attr("title", 'go');
			$(element).attr("src", '../Content/image/more/' + imgHmmHelp + '.png');
			return;
		} else if ($(element).attr("title") == 'go') {
			$(element).attr("title", 'help');
			$(element).attr("src", '../Content/image/help.png');
			return;
		}
	},

	// 答题验证(正确:true  错误:false)
	_howMuchMoreCorrecting = function (pIndex, pElement) {

		var isTrue = 0;
		// 頁面圖片按鍵轉空白邊框
		$("img[id*='imgHmmHelp" + pIndex).each(function (index, element) {
			if ("help" != $(element).attr('title')){
				isTrue++;
			}
		});
		var answer = parseInt($(pElement).val());
		// 验证输入值是否与答案一致
		if (answer == isTrue){
			// 对错图片显示和隐藏
			$('#imgOKHmm' + pIndex).show();
			$('#imgNoHmm' + pIndex).hide();
			$("img[id*='imgHmmHelp" + pIndex).each(function (index, element) {
				$(element).attr("disabled", "disabled");
			});
			// 正确:true
			return true;
		}else{
			// 对错图片显示和隐藏
			$('#imgOKHmm' + pIndex).hide();
			$('#imgNoHmm' + pIndex).show();
			// 错误:false
			return false;
		}
	},

	// 订正(比多少)
	makeCorrections = function () {
		var fault = 0;
		$("input[id*='hidHmmAnswer']").each(function (index, element) {
			// 答题验证
			if (!_howMuchMoreCorrecting(index, element)) {
				// 答题错误时,错误件数加一
				fault++;
			}
		});
		return fault;
	},

	// 比多少交卷
	theirPapers = function () {
		$("input[id*='hidHmmAnswer']").each(function (index, element) {
			// 答题验证
			if (!_howMuchMoreCorrecting(index, element)) {
				// 答题错误时,错误件数加一
				__isFault++;
			} else {
				__isRight++;
			}
		});
	},

	// 設定頁面所有輸入域為可用狀態(比多少)
	ready = function () {

		// 圖片名稱 = {'Diamond','Fish','HappyFace','Like'}
		var imgHmmHelpArray = ($('#hidImgHmmHelpArray').val() || "").split(',');
		$("input[id*='hidHmmAnswer']").each(function (parentIndex, pElement) {
			// 圖形按鈕事件註冊
			$("img[id*='imgHmmHelp" + parentIndex + "']").each(function (index, element) {
				$(element).click(function () { _imgHmmHelpClick(this, imgHmmHelpArray[parentIndex]); });
			});
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
