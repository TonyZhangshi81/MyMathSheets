
var __tony;

var MathSheets = MathSheets || {};
MathSheets.HelloMrTony = MathSheets.HelloMrTony || (function () {

	// 指定并找到老師
	teacher = function () {
		if (__tony == null) {
			__tony = $("#divShakeHead");
		}
		return __tony;
	}

	// 恭喜你,滿分過關
	doCelebrate = function () {
		// 頭像居中設置
		teacher.css("top", "200px").css("right", "400px");
		// 刪除默認會話
		teacher.attr("title", "");
	};
	
	return {
		teacher: teacher,
		doCelebrate: doCelebrate
};
}());