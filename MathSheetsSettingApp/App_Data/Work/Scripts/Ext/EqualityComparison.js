// 打印設置
function equalityComparisonPrintSetting() {
	$("img[id*='imgEc']").each(function (index, element) {
		$(element).replaceWith("<button type=\"button\" class=\"btn btn-default btn-circle button-addBorder\"></button>");
	});
}

// 设定页面所有输入域为可用状态(算式比大小)
function equalityComparisonReady() {
	$("img[id*='imgEc']").each(function (index, element) {
		$(element).click(function () { imgEqualityClick(element); });
	});
}

function imgEqualityClick(element) {
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
}

function equalityComparisonTheirPapers() {
	$("img[id*='imgEc']").each(function (index, element) {
		// 答题验证
		if (!equalityComparisonCorrecting(index, element)) {
			// 答题错误时,错误件数加一
			isFault++;
		} else {
			isRight++;
		}
	});
}

// 答题验证(正确:true  错误:false)
function equalityComparisonCorrecting(index, element) {
	// 验证输入值是否与答案一致(并且特殊情况下,答案值可以是任意值,此处以-999代替)
	if ($(element).attr("title") == $('#hiddenEc' + index).val()) {
		// 对错图片显示和隐藏
		$('#imgEqualityOK' + index).show();
		$('#imgEqualityNo' + index).hide();
		$(element).attr("disabled", "disabled");
		// 正确:true
		return true;
	} else {
		// 对错图片显示和隐藏
		$('#imgEqualityOK' + index).hide();
		$('#imgEqualityNo' + index).show();
		// 错误:false
		return false;
	}
}

// 订正(运算比大小)
function equalityComparisonMakeCorrections() {
	var fault = 0;
	$("img[id*='imgEc']").each(function (index, element) {
		// 答题验证
		if (!equalityComparisonCorrecting(index, element)) {
			// 答题错误时,错误件数加一
			fault++;
		}
	});
	return fault;
}
