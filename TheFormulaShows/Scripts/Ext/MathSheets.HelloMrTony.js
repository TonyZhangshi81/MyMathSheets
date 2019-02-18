
// 虛擬人物開關
var __switch = 'on';
// 回話序列
var __dialogueArray = new Array();
// 定時器執行后ID
var __timeId;
// 當前會話所在集合的位置
var __dialogueId = 0;
// 會話是否已經顯示(0沒有顯示，1顯示過，2等待)
var __messageIsShowed = 0;
// 是否無限循環執行
var __isCirculation = false;
// 循環事件執行后的回調函數定義對象
var __callbackFunc;
// 頁面虛擬人物實例
var $teacher;

var MathSheets = MathSheets || {};

MathSheets.HelloMrTony = MathSheets.HelloMrTony || (function () {

	var _tonyWidth = 0;
	// 會話顯示的時間點
	var _smarktime = 0;
	// 會話隱藏的時間點
	var _emarktime = 0;
	// 等待間隔時間(5秒)
	var _offset = 5000;

	// 回話顯示處理如下
	// 1.當前會話保持顯示5秒鐘
	// 2.5秒后隱藏當前會話
	// 3.按照上述處理過程逐個處理會話列表中的內容
	// 4.關閉虛擬人物時停止上述處理但保留會話狀態
	// 5.會話輪詢處理分為循環播放和非循環播放（在調用時設定該播放參數）
	_dialogue = function () {
		// 如果虛擬人物被關閉則暫停會話（不清除之前的回話狀態）
		if (__switch == 'off') {
			// 停止計時
			clearTimeout(__timeId);
			return;
		}

		// 判斷是否循環播放
		if (!__isCirculation) {
			// 如果會話播放完畢則釋放當前會話列表的內容
			if (__dialogueId >= __dialogueArray.length) {
				__dialogueArray.length = 0;
			}
		} else {
			// 會話循環模式開啟
			if (__dialogueId >= __dialogueArray.length) {
				__dialogueId = 0;
			}
		}

		// 當前沒有回話則停止計時器
		if (__dialogueArray.length == 0) {
			// 在停止計時之前執行回調函數（如果有回調函數的情況則執行）
			if (typeof __callbackFunc != 'undefined' && __callbackFunc instanceof Function) {
				// 執行回調函數
				__callbackFunc();
			}
			// 停止計時
			clearTimeout(__timeId);
			return;
		}

		// 判斷是否已經顯示過會話，如果是（0沒有執行顯示）則激活當前會話并執行顯示
		if (__messageIsShowed == 0) {
			$teacher.attr('data-original-title', __dialogueArray[__dialogueId]);
			$teacher.tooltip('show');
			// 設置為已經執行過顯示（避免遞歸時再次執行上述顯示處理）
			__messageIsShowed = 1;
		}

		//console.log('s:' + (new Date().getTime() - _smarktime));
		// 保持上述顯示處理（延遲時間為默認的5秒鐘），5秒后執行後續處理
		if ((new Date().getTime() - _smarktime) < _offset) {
			__timeId = setTimeout(_dialogue, 1000);
			return;
		}

		// 清除開始執行的時間點
		_smarktime = 0;
		// 判斷是否已經顯示過會話，如果是（1已經執行過顯示）則隱藏當前會話
		if (__messageIsShowed == 1) {
			// 用於隱藏處理的時間點初始化設定
			_emarktime = new Date().getTime();
			// 隱藏會話
			$teacher.tooltip('hide');
			$teacher.attr('data-original-title', "");
			// 設置為等待執行狀態（避免遞歸時再次執行上述顯示或隱藏處理）
			__messageIsShowed = 2;
		}

		//console.log('e:' + (new Date().getTime() - _emarktime));
		// 保持上述隱藏處理（延遲時間為默認的5秒鐘），5秒后執行後續處理
		if ((new Date().getTime() - _emarktime) < _offset) {
			__timeId = setTimeout(_dialogue, 1000);
			return;
		}

		// 會話指針
		__dialogueId++;
		// 回復顯示初始狀態（0表示沒有執行過顯示處理）
		__messageIsShowed = 0;
		_smarktime = new Date().getTime();
		// 再次輪詢會話（情況1：會話結束 情況2：會話重新開始）
		__timeId = setTimeout(_dialogue, 1000);
	},


		// 開啟或者關閉虛擬人物
		showMrTony = function (onoff, isAnimate = true) {
			__switch = onoff;

			if (__switch == 'off') {
				$teacher.tooltip('hide');
				$teacher.attr('data-original-title', "");

				if (isAnimate) {
					// 關閉虛擬人物
					$teacher.animate({
						width: 0
					}, "slow", "swing", function () { $teacher.hide(); });
				}
			} else {
				// 如果虛擬人物的會話已經結束，則不顯示人物
				if (__dialogueArray.length == 0) {
					return;
				}
				// 人物顯示
				$teacher.show();
				if (isAnimate) {
					// 自動播放
					$teacher.animate({
						width: _tonyWidth
					}, "slow", "swing", autoPlay(5000));
				}
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

			// 執行間隔時間
			_offset = delay;
			// 記錄會話開始的時間
			_smarktime = new Date().getTime();
			__timeId = setTimeout(_dialogue, delay);
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
				__dialogueArray.push("<h5>如果打搅你了我可以去喝杯茶</h5>");
				return __dialogueArray;
			}, 0, false, false, null);
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
			autoPlay(5000);
		},

		// 設定會話列表
		setDialogueArray = function (dialogue) {
			__dialogueArray = [];
			__dialogueArray.push(dialogue);
		},

		// 幫助提示
		help = function () {
			// 參數初期化設置
			_initParameter(function () {
				return __dialogueArray;
			}, 0, false, false, null);
			// 自動播放
			autoPlay(3000);
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
			}, 500, "easeOutQuint", function () {
				// 參數初期化設置
				_initParameter(function () {
					__dialogueArray = [];
					__dialogueArray.push("<h5>真厉害全部答对了！<br/>给你一枚奖章...</h5>");
					__dialogueArray.push("<h5>可以玩游戏哦！<br/>我先走了,再见!</h5>");
					return __dialogueArray;
				}, 0, false, false, callbackFunc);
				// 自動播放
				autoPlay(3000);
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
		theirPapersComplete: theirPapersComplete,
		// 設定會話列表
		setDialogueArray: setDialogueArray,
		// 幫助提示
		help: help
	};
}());