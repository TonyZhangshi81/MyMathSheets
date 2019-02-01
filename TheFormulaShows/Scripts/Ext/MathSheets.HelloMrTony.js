
// 回話序列
var __dialogueArray = new Array();
var __timeId;
var __dialogueId = 0;
var __flag = 0;
var __unlimited = false;
var $teacher;

var MathSheets = MathSheets || {};
MathSheets.HelloMrTony = MathSheets.HelloMrTony || (function () {

	// 回話顯示dialogue
	_dialogue = function () {
		if (__unlimited) {
			if (__dialogueId >= __dialogueArray.length) {
				__dialogueArray.length = 0;
			}
		}

		// 當前沒有回話（沒什麼好說的:!）
		if (__dialogueArray.length == 0) {
			clearInterval(__timeId);
			return;
		}

		if (__flag == 0) {
			$teacher.attr('data-original-title', __dialogueArray[__dialogueId]);
			$teacher.tooltip('show');
			__flag = 1;
		} else {
			$teacher.tooltip('hide');
			$teacher.attr('data-original-title', "");
			__dialogueId++;
			__flag = 0;
		}
	},

		// 循環定時播放
		autoPlay = function (delay) {
			__timeId = setInterval(_dialogue, delay);
		},

		// 初期設定
		initialize = function () {
			$teacher = $("#divShakeHead");
			$teacher.delay({ show: 5000, hide: 5000 });
			$teacher.tooltip({ html: true });

			setTimeout(function () {
				$teacher.attr('data-original-title', "<h3>I'm Tony. Hello! Eve.</h3>");
				// 會話顯示
				$teacher.tooltip('show');
				setTimeout(function () {
					// 關閉會話
					$teacher.tooltip('toggle');
				}, 10000);
			}, 3000);
		},

		// 恭喜你,滿分過關
		doCelebrate = function () {
			//$teacher.tooltip('hide');
			//$teacher.css("top", "100px").css("right", "400px");

			__dialogueArray.push("<h5>真厉害全部答对了！<br/>给你一枚奖章...</h5>");
			__dialogueArray.push("<h5>还可以玩游戏哦！</h5>");

			$teacher.tooltip('hide');

			$teacher.animate({
				top: 200,
				right: 400
			}, 1000, "easeOutQuint", function () {
				__unlimited = true;
				autoPlay(3000);
			});
		};

	return {
		// 初期化設定
		initialize: initialize,
		// 恭喜答對滿分過關
		doCelebrate: doCelebrate,
		// 自動播放
		autoPlay: autoPlay
	};
}());