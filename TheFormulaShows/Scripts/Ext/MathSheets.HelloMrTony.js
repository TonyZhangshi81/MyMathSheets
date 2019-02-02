﻿
// 回話序列
var __dialogueArray = new Array();
// 定時器執行后ID
var __timeId;
// 當前會話所在集合的位置
var __dialogueId = 0;
// 會話是否已經顯示
var __isShowed = false;
// 是否無限循環執行
var __isCirculation = false;
// 循環事件執行后的回調函數定義對象
var __callbackFunc;
// 頁面虛擬人物實例
var $teacher;

var MathSheets = MathSheets || {};

MathSheets.HelloMrTony = MathSheets.HelloMrTony || (function () {

	// 回話顯示dialogue
	_dialogue = function () {
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
			// 停止計時
			clearInterval(__timeId);
			// 循環事件完成之後
			if (typeof __callbackFunc != 'undefined' && __callbackFunc instanceof Function) {
				__callbackFunc();
			}
			return;
		}

		if (!__isShowed) {
			$teacher.attr('data-original-title', __dialogueArray[__dialogueId]);
			$teacher.tooltip('show');
			__isShowed = true;
		} else {
			$teacher.tooltip('hide');
			$teacher.attr('data-original-title', "");
			__dialogueId++;
			__isShowed = false;
		}
	},

		// 全局參數初期化設置
		_initParameter = function (setDialogueArrayFunc, dialogueId, isShowed, isCirculation, callbackFunc) {
			__dialogueArray = setDialogueArrayFunc();
			__dialogueId = dialogueId;
			__isShowed = isShowed;
			__isCirculation = isCirculation;
			__callbackFunc = callbackFunc;
		},

		// 循環定時播放
		autoPlay = function (delay) {
			__timeId = setInterval(_dialogue, delay);
		},

		// 初期設定
		initialize = function () {
			$teacher = $("#divShakeHead");
			$teacher.tooltip({ html: true });

			// 參數初期化設置
			_initParameter(function () {
				__dialogueArray = [];
				__dialogueArray.push("<h3>I'm Tony. Hello! Eve.</h3>");
				__dialogueArray.push("<h5>有没有做好准备<br/>我们要准备做题咯</h5>");
				return __dialogueArray;
			}, 0, false, true, null);
			// 自動播放
			autoPlay(3000);
		},

		// 準備操作已經完成
		readyComplete = function () {
			// 沒有初期化的情況下
			if ($teacher == null) {
				return;
			}

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
			autoPlay(4000);
		},

		// 恭喜你,滿分過關
		doCelebrate = function () {
			// 沒有初期化的情況下
			if ($teacher == null) {
				return;
			}

			// 將當前的會話隱藏
			$teacher.tooltip('hide');
			// 定義循環事件執行后的回調函數(1.向右躲閃 2.整體隱藏)
			__callbackFunc = function () {
				$teacher.animate({
					width: 0
				}, "slow", "swing", function () { $teacher.hide(); })
			};

			// 循環事件執行并最後完成上述回調事件
			$teacher.animate({
				top: 150,
				right: 400
			}, 1000, "easeOutQuint", function () {
				// 參數初期化設置
				_initParameter(function () {
					__dialogueArray = [];
					__dialogueArray.push("<h5>真厉害全部答对了！<br/>给你一枚奖章...</h5>");
					__dialogueArray.push("<h5>可以玩游戏哦！<br/>我先走了,再见!</h5>");
					return __dialogueArray;
				}, 0, false, false, __callbackFunc);
				// 自動播放
				autoPlay(4000);
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
		autoPlay: autoPlay
	};
}());