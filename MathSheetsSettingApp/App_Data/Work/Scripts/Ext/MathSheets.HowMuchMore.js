// 圖片名稱 = {'Diamond','Fish','HappyFace','Like'}
var __imgHmmHelpArray;

var MathSheets = MathSheets || {};
MathSheets.HowMuchMore = MathSheets.HowMuchMore || (function () {

	// 打印設置
	printSetting = function () {

		// 頁面圖片按鍵轉空白邊框
		$("img[id*='imgHmmHelp").each(function (index, element) {
			$(element).replaceWith("<button id=\"btnHmm" + $(element).attr('id').substring(-2) + "\" type=\"button\" class=\"btn btn-default btn-block button-addBorder\"></button>");
		});
	},

	// 打印后頁面設定
	printAfterSetting = function () {

		// 頁面按鍵項目復原
		$("button[id*='btnHmm").each(function (index, element) {
			$(element).replaceWith("<img src=\"../Content/image/help.png\" id=\"imgHmmHelp" + $(element).attr('id').substring(-2) + "\" style=\"width: 30px; height: 30px; \" title=\"help\" />");
		});
	},

	// 圖片点击切换
	_imgHmmHelpClick = function (element, imgHmmHelp) {

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

	// 設定頁面所有輸入域為可用狀態(比多少)
	ready = function () {

		// 圖片名稱 = {'Diamond','Fish','HappyFace','Like'}
		__imgHmmHelpArray = ($('#hidImgHmmHelpArray').val() || "").split(',');
		// 圖形按鈕事件註冊
		$("img[id*='imgHmmHelp']").each(function (index, element) {
			var imgIndex = parseInt(index / 10);
			$(element).click(function () { _imgHmmHelpClick(element, __imgHmmHelpArray[imgIndex]); });
		});
	};

	return {
		printSetting: printSetting,
		printAfterSetting: printAfterSetting,
		ready: ready
	};
}());
