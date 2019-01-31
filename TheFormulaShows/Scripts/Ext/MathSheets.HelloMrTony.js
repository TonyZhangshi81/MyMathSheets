
var MathSheets = MathSheets || {};
MathSheets.HelloMrTony = MathSheets.HelloMrTony || (function () {

	// 恭喜你,滿分過關
	doCelebrate = function () {
		var $teacher = $("#divShakeHead");
		// 頭像居中設置
		$teacher.css("top", "100px").css("right", "400px");
		// 撤銷默認會話
		$teacher.tooltip('destroy');
	};

	return {
		// 恭喜答對滿分過關
		doCelebrate: doCelebrate
	};
}());