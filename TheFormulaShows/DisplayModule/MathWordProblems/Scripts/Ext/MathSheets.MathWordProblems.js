
var MathSheets = MathSheets || {};

MathSheets.MathWordProblems = MathSheets.MathWordProblems || (function () {

	// 运算符点击切换(>、<、=)
	_imgProblemsClick = function (element) {
		if ($(element).attr("title") == 'help') {
			$(element).attr("title", 'plus');
			$(element).attr("src", '../Content/image/Plus.png');
			return;
		} else if ($(element).attr("title") == 'plus') {
			$(element).attr("title", 'subtraction');
			$(element).attr("src", '../Content/image/Subtraction.png');
			return;
		} else if ($(element).attr("title") == 'subtraction') {
			$(element).attr("title", 'multiple');
			$(element).attr("src", '../Content/image/Multiple.png');
			return;
		} else if ($(element).attr("title") == 'multiple') {
			$(element).attr("title", 'division');
			$(element).attr("src", '../Content/image/Division.png');
			return;
		} else if ($(element).attr("title") == 'division') {
			$(element).attr("title", 'plus');
			$(element).attr("src", '../Content/image/Plus.png');
			return;
		}
	},

		// 运算符转换(+-*/)
		_signToString = function (title) {
			switch (title) {
				case "plus":
					return "+";
				case "subtraction":
					return "-";
				case "multiple":
					return "*";
				case "division":
					return "/";
				default:
					return "";
			}
		},

		// 計算式各項目的非空檢查
		_checkInputIsEmpty = function (inputArray) {
			var inputL = $(inputArray[0]).val();
			if (inputL == '') {
				return true;
			}

			var inputR = $(inputArray[1]).val();
			if (inputR == '') {
				return true;
			}

			var title = $(inputArray[2]).attr("title");
			if (title == 'help') {
				return true;
			}

			var inputA = $(inputArray[3]).val();
			if (inputA == '') {
				return true;
			}

			return false;
		},

		// 答案比對
		_isExist = function (index, result) {
			var hidValue = $('#hiddenMwp' + index).val();
			var answers = (hidValue || '').split(',');
			for (var i = 0; i < answers.length; i++) {
				if (answers[i] == result)
					return true;
			}
			return false;
		},

		// 答题验证(正确:true  错误:false)
		_mathWordProblemsCorrecting = function (index, element) {
			var inputArray = new Array();
			inputArray.push($('#inputMwp' + index + '0'));
			inputArray.push($('#inputMwp' + index + '1'));
			inputArray.push($('#imgMwp' + index));
			inputArray.push($('#inputMwp' + index + '2'));

			var result = '';
			// 計算式各項目的非空檢查
			if (!_checkInputIsEmpty(inputArray)) {
				// 等式健全的情況下，統計結果
				result = $(inputArray[0]).val() + _signToString($(inputArray[2]).attr("title")) + $(inputArray[1]).val() + "=" + $(inputArray[3]).val();
			}

			// 验证输入值是否与答案一致
			if (_isExist(index, result)) {
				// 对错图片显示和隐藏
				$('#imgOKProblems' + index).show();
				$('#imgNoProblems' + index).hide();

				inputArray.forEach(function (element, index) {
					$(element).attr("disabled", "disabled");
				});
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKProblems' + index).hide();
				$('#imgNoProblems' + index).show();
				// 错误:false
				return false;
			}
		},

		// 打印設置
		printSetting = function () {
			$("img[id*='imgMwp']").each(function (index, element) {
				$(element).replaceWith("<button id=\"btnMwp" + index + "\" type=\"button\" class=\"btn btn-default btn-circle button-addBorder\"></button>");
			});

			$("input[id*='inputMwp']").each(function (index, element) {
				$(element).addClass('input-print');
				$(element).removeAttr('placeholder');
				$(element).removeAttr("disabled");
			});
		},

		// 打印后頁面設定
		printAfterSetting = function () {
			$("button[id*='btnMwp']").each(function (index, element) {
				$(element).replaceWith("<img src=\"../Content/image/help.png\" id=\"imgMwp" + index + "\" style=\"width: 30px; height: 30px; \" title=\"help\" />");
			});

			$("input[id*='inputMwp']").each(function (index, element) {
				$(element).removeClass('input-print');
				$(element).attr('placeholder', '??');
				$(element).attr("disabled", "disabled");
			});
		},

		// 设定页面所有输入域为可用状态(算式应用题)
		ready = function () {
			$("input[id*='inputMwp']").each(function (index, element) {
				$(element).removeAttr("disabled");
			});
			$("img[id*='imgMwp']").each(function (index, element) {
				$(element).click(function () { _imgProblemsClick(element); });
			});
		},

		// 交卷
		theirPapers = function () {
			$("input[id*='hiddenMwp']").each(function (index, element) {
				// 答题验证
				if (!_mathWordProblemsCorrecting(index, element)) {
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
			$("input[id*='hiddenMwp']").each(function (index, element) {
				// 答题验证
				if (!_mathWordProblemsCorrecting(index, element)) {
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
