
// 虛擬人物開關
var __switch = 'on';
// 回話序列
var __dialogueArray = new Array();
// 定時器執行后ID
var __timeId;
// 當前會話所在集合的位置
var __dialogueId = 0;
// 會話是否已經顯示
var __messageIsShowed = false;
// 是否無限循環執行
var __isCirculation = false;
// 循環事件執行后的回調函數定義對象
var __callbackFunc;
// 頁面虛擬人物實例
var $teacher;

var MathSheets = MathSheets || {};

MathSheets.HelloMrTony = MathSheets.HelloMrTony || (function () {

	var _tonyWidth = 0;

	// 回話顯示dialogue
	_dialogue = function () {
		if (__switch == 'off') {
			// 停止計時
			clearTimeout(__timeId);
			return;
		}

		if (!__isCirculation) {
			if (__dialogueId >= __dialogueArray.length) {
				__dialogueArray.length = 0;
			}
		} else {
			if (__dialogueId >= __dialogueArray.length) {
				__dialogueId = 0;
			}
		}

		// 當前沒有回話（沒什麼好說的:!）
		if (__dialogueArray.length == 0) {
			// 循環事件完成之後
			if (typeof __callbackFunc != 'undefined' && __callbackFunc instanceof Function) {
				__callbackFunc();
			}
			// 停止計時
			clearTimeout(__timeId);
			return;
		}

		if (!__messageIsShowed) {
			$teacher.attr('data-original-title', __dialogueArray[__dialogueId]);
			$teacher.tooltip('show');
			__messageIsShowed = true;
			__timeId = setTimeout(_dialogue, 5000);
		} else {
			$teacher.tooltip('hide');
			$teacher.attr('data-original-title', "");
			__dialogueId++;
			__messageIsShowed = false;
			__timeId = setTimeout(_dialogue, 5000);
		}
	},

		// 開啟或者關閉虛擬人物
		showMrTony = function (onoff) {
			__switch = onoff;

			if (__switch == 'off') {
				$teacher.tooltip('hide');
				$teacher.attr('data-original-title', "");
				// 關閉虛擬人物
				$teacher.animate({
					width: 0
				}, "slow", "swing", function () { $teacher.hide(); });
			} else {
				// 如果虛擬人物的會話已經結束，則不顯示人物
				if (__dialogueArray.length == 0) {
					return;
				}
				// 人物顯示
				$teacher.show();
				// 自動播放
				$teacher.animate({
					width: _tonyWidth
				}, "slow", "swing", autoPlay(1000));
			}
		},

		// 全局參數初期化設置
		_initParameter = function (setDialogueArrayFunc, dialogueId, messageIsShowed, isCirculation, callbackFunc) {
			// 回話序列
			__dialogueArray = setDialogueArrayFunc();
			// 當前會話所在集合的位置
			__dialogueId = dialogueId;
			// 會話是否已經顯示
			__messageIsShowed = messageIsShowed;
			// 是否無限循環執行
			__isCirculation = isCirculation;
			// 循環事件執行后的回調函數定義對象
			__callbackFunc = callbackFunc;
		},

		// 循環定時播放
		autoPlay = function (delay) {
			// 虛擬人物被關閉，播放停止
			if (__switch == 'off') {
				if (__timeId != null) {
					// 停止計時
					clearTimeout(__timeId);
				}
				return;
			}

			__timeId = setTimeout(_dialogue, delay);
			//__timeId = setInterval(_dialogue, delay);
		},

		// 初期設定
		initialize = function () {
			$teacher = $("#divShakeHead");
			$teacher.tooltip({ html: true });
			_tonyWidth = parseInt($teacher.css('width'));

			// 參數初期化設置
			_initParameter(function () {
				__dialogueArray = [];
				__dialogueArray.push("<h3>I'm Tony. Hello! Eve.</h3>");
				__dialogueArray.push("<h5>有没有做好准备<br/>我们要准备做题咯</h5>");
				return __dialogueArray;
			}, 0, false, true, null);
			// 自動播放
			autoPlay(5000);
		},

		// 準備操作已經完成
		readyComplete = function () {
			// 寬度設置（避免題目被虛擬人物遮擋）
			$teacher.width(300);
			$teacher.children(":first").width(300);

			// 將當前的會話隱藏
			$teacher.tooltip('hide');
			// 參數初期化設置
			_initParameter(function () {
				__dialogueArray = [];
				__dialogueArray.push("<h5>练习已经开始了<br/>请认真看题哦</h5>");
				__dialogueArray.push("<h5>加油!<br/>我会帮助你的</h5>");
				return __dialogueArray;
			}, 0, false, true, null);
			// 自動播放
			autoPlay(5000);
		},

		// 交卷操作已經完成
		theirPapersComplete = function (score) {
			__dialogueArray = [];
			// 得分在8至10之間
			if (score >= 8 && score < 10) {
				__dialogueArray.push("<h5>再仔细一点<br/>你可以得到满分</h5>");
				__dialogueArray.push("<h5>看看你的错题<br/>快去订正吧</h5>");
				__dialogueArray.push("<h5>有不会做的题目吗?</h5>");
			} else if (score >= 5 && score < 8) {
				__dialogueArray.push("<h5>有不会做的题目吗?</h5>");
				__dialogueArray.push("<h5>看看你的错题<br/>快去订正吧</h5>");
				__dialogueArray.push("<h5>看来,你以后要多做练习</h5>");
			} else if (score < 5) {
				__dialogueArray.push("<h5>有不会做的题目吗?</h5>");
				__dialogueArray.push("<h5>看看你的错题<br/>快去订正吧</h5>");
				__dialogueArray.push("<h5>你有些退步了</h5>");
				__dialogueArray.push("<h5>你需要努力了</h5>");
				__dialogueArray.push("<h5>打开书本复习一下吧</h5>");
			}

			// 將當前的會話隱藏
			$teacher.tooltip('hide');
			// 參數初期化設置
			_initParameter(function () {
				return __dialogueArray;
			}, 0, false, true, null);
			// 自動播放
			autoPlay(1000);
		},

		// 恭喜你,滿分過關
		doCelebrate = function () {
			// 將當前的會話隱藏
			$teacher.tooltip('hide');
			// 定義循環事件執行后的回調函數(1.向右躲閃 2.整體隱藏)
			var callbackFunc = function () {
				$teacher.animate({
					width: 0
				}, "slow", "swing", function () { $teacher.hide(); })
			};

			// 循環事件執行并最後完成上述回調事件
			$teacher.animate({
				top: 150,
				right: 500
			}, 1000, "easeOutQuint", function () {
				// 參數初期化設置
				_initParameter(function () {
					__dialogueArray = [];
					__dialogueArray.push("<h5>真厉害全部答对了！<br/>给你一枚奖章...</h5>");
					__dialogueArray.push("<h5>可以玩游戏哦！<br/>我先走了,再见!</h5>");
					return __dialogueArray;
				}, 0, false, false, callbackFunc);
				// 自動播放
				autoPlay(2000);
			});
		};

	return {
		// 初期化設定
		initialize: initialize,
		// 恭喜答對滿分過關
		doCelebrate: doCelebrate,
		// 準備操作已經完成
		readyComplete: readyComplete,
		// 自動播放
		autoPlay: autoPlay,
		// 虛擬人物開關
		showMrTony: showMrTony,
		// 交卷操作已經完成
		theirPapersComplete: theirPapersComplete
	};
}());