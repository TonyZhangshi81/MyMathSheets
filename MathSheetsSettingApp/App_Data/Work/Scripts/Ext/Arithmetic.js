// 设定页面所有输入域为可用状态(四则运算)
function arithmeticReady() {
	$("input[id*='inputAc']").each(function (index, element) {
		$(element).removeAttr("disabled");
	});
}

// 订正(四则运算题)
function arithmeticMakeCorrections() {
	var fault = 0;
	$("input[id*='inputAc']").each(function (index, element) {
		// 答题验证
		if (!arithmeticCorrecting(index, element)) {
			// 答题错误时,错误件数加一
			fault++;
		}
	});
	return fault;
}

// 答题验证(正确:true  错误:false)
function arithmeticCorrecting(index, element) {
	// 验证输入值是否与答案一致(并且特殊情况下,答案值可以是任意值,此处以-999代替)
	if ($(element).val() == $('#hiddenAc' + index).val()
		|| (parseInt($('#hiddenAc' + index).val()) == -999 && $(element).val() != '')) {
		// 对错图片显示和隐藏
		$('#imgArithmeticOK' + index).show();
		$('#imgArithmeticNo' + index).hide();
		$(element).attr("disabled", "disabled");
		// 正确:true
		return true;
	} else {
		// 对错图片显示和隐藏
		$('#imgArithmeticOK' + index).hide();
		$('#imgArithmeticNo' + index).show();
		// 错误:false
		return false;
	}
}

// 四则运算交卷
function arithmeticTheirPapers() {
	$("input[id*='inputAc']").each(function (index, element) {
		// 答题验证
		if (!arithmeticCorrecting(index, element)) {
			// 答题错误时,错误件数加一
			isFault++;
		} else {
			isRight++;
		}
	});
}
