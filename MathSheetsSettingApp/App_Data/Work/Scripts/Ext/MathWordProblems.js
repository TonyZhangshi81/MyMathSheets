// 打印設置
function mathWordProblemsPrintSetting() {
	$("img[id*='imgMwp']").each(function (index, element) {
		$(element).replaceWith("<button type=\"button\" class=\"btn btn-default btn-circle\"><i class=\"glyphicon glyphicon-ok\"></i></button>");
	});

	$("input[id*='inputMwp']").each(function (index, element) {
		$(element).removeAttr('placeholder');
	});
}

// 设定页面所有输入域为可用状态(算式应用题)
function mathWordProblemsReady() {
	$("input[id*='inputMwp']").each(function (index, element) {
		$(element).removeAttr("disabled");
	});
	$("img[id*='imgMwp']").each(function (index, element) {
		$(element).click(function () { imgProblemsClick(element); });
	});
}

// 运算符点击切换
function imgProblemsClick(element) {
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
}

// 交卷
function mathWordProblemsTheirPapers() {
	$("input[id*='hiddenMwp']").each(function (index, element) {
		// 答题验证
		if (!mathWordProblemsCorrecting(index, element)) {
			// 答题错误时,错误件数加一
			isFault++;
		} else {
			isRight++;
		}
	});
}

// 运算符转换(+-*/)
function signToString(title) {
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
}

// 答题验证(正确:true  错误:false)
function mathWordProblemsCorrecting(index, element) {
	var inputArray = new Array();

	inputArray.push($('#inputMwp' + index + '0'));
	var inputL = $(inputArray[0]).val();
	if (inputL == '') {
		return false;
	}

	inputArray.push($('#inputMwp' + index + '1'));
	var inputR = $(inputArray[1]).val();
	if (inputR == '') {
		return false;
	}

	inputArray.push($('#imgMwp' + index));
	var title = $(inputArray[2]).attr("title");
	if (title == 'help') {
		return false;
	}

	inputArray.push($('#inputMwp' + index + '2'));
	var inputA = $(inputArray[3]).val();
	if (inputA == '') {
		return false;
	}

	var result = inputL + signToString(title) + inputR + "=" + inputA;

	// 验证输入值是否与答案一致
	if (isExist(index, result)) {
		// 对错图片显示和隐藏
		$('#imgProblemsOK' + index).show();
		$('#imgProblemsNo' + index).hide();

		inputArray.forEach(function (element, index) {
			$(element).attr("disabled", "disabled");
		});
		// 正确:true
		return true;
	} else {
		// 对错图片显示和隐藏
		$('#imgProblemsOK' + index).hide();
		$('#imgProblemsNo' + index).show();
		// 错误:false
		return false;
	}
}

function isExist(index, result) {
	var values = $('#hiddenMwp' + index).val().split(',');
	for (var i = 0; i < values.length; i++) {
		if (values[i] == result)
			return true;
	}
	return false;
}

// 订正(算式应用题)
function mathWordProblemsMakeCorrections() {
	var fault = 0;
	$("input[id*='hiddenMwp']").each(function (index, element) {
		// 答题验证
		if (!mathWordProblemsCorrecting(index, element)) {
			// 答题错误时,错误件数加一
			fault++;
		}
	});
	return fault;
}