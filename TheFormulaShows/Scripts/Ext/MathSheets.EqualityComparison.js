
var MathSheets = MathSheets || {};

MathSheets.EqualityComparison = MathSheets.EqualityComparison || (function () {

	// 關係運算符的切換
	_imgEqualityClick = function (element) {
		if ($(element).attr("title") == 'help') {
			$(element).attr("title", 'less');
			$(element).attr("src", '../Content/image/char-less.png');
			return;
		} else if ($(element).attr("title") == 'less') {
			$(element).attr("title", 'more');
			$(element).attr("src", '../Content/image/char-more.png');
			return;
		} else if ($(element).attr("title") == 'more') {
			$(element).attr("title", 'calculator');
			$(element).attr("src", '../Content/image/calculator.png');
			return;
		} else if ($(element).attr("title") == 'calculator') {
			$(element).attr("title", 'less');
			$(element).attr("src", '../Content/image/char-less.png');
			return;
		}
	},

		// 答题验证(正确:true  错误:false)
		_equalityComparisonCorrecting = function (index, element) {
			// 验证输入值是否与答案一致(并且特殊情况下,答案值可以是任意值,此处以-999代替)
			if ($(element).attr("title") == $('#hiddenEc' + index).val()) {
				// 对错图片显示和隐藏
				$('#imgOKEquality' + index).show();
				$('#imgNoEquality' + index).hide();
				$(element).attr("disabled", "disabled");
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKEquality' + index).hide();
				$('#imgNoEquality' + index).show();
				// 错误:false
				return false;
			}
		},

		// 打印設置
		printSetting = function () {
			$("img[id*='imgEc']").each(function (index, element) {
				$(element).replaceWith("<button id=\"btnEc" + index + "\" type=\"button\" class=\"btn btn-default btn-circle button-addBorder\"></button>");
			});
		},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("button[id*='btnEc']").each(function (index, element) {
				$(element).replaceWith("<img src=\"../Content/image/help.png\" id=\"imgEc" + index + "\" style=\"width: 30px; height: 30px; \" title=\"help\" />");
			});
		},

		// 设定页面所有输入域为可用状态(算式比大小)
		ready = function () {
			$("img[id*='imgEc']").each(function (index, element) {
				$(element).click(function () { _imgEqualityClick(element); });
			});
		},

		theirPapers = function () {
			$("img[id*='imgEc']").each(function (index, element) {
				// 答题验证
				if (!_equalityComparisonCorrecting(index, element)) {
					// 答题错误时,错误件数加一
					__isFault++;
				} else {
					__isRight++;
				}
			});
		},

		// 订正(运算比大小)
		makeCorrections = function () {
			var fault = 0;
			$("img[id*='imgEc']").each(function (index, element) {
				// 答题验证
				if (!_equalityComparisonCorrecting(index, element)) {
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
