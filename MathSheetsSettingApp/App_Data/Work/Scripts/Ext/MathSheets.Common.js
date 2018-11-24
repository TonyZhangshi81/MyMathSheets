// 秒
var __second = 0;
// 分
var __minute = 0;
// 時
var __hour = 0;
// 用於停止計時器(停止計時:true)
var __isStop = false;
// 答對數量
var __isRight = 0;
// 答錯數量
var __isFault = 0;

var MathSheets = MathSheets || {};
MathSheets.Common = MathSheets.Common || (function () {

	// 取得ID(eg: #Control)
	_getId = function (id) {
		return '#' + id;
	},

		// 計時器-不足十位向前補0
		_checkTime = function (i) {
			if (i < 10) { i = "0" + i }
			return i
		},

		// 頁面關閉
		windowClose = function () {
			window.close();
		},

		// 設定頁面所有輸入域為可用狀態
		ready = function (btnTheirPapersId, btnReadyId, btnPrintId) {
			// 交卷按鈕顯示
			$(_getId(btnTheirPapersId)).show();
			// 準備按鈕隱藏
			$(_getId(btnReadyId)).hide();
			// 打印按鈕隱藏
			$(_getId(btnPrintId)).hide();
		},

		// 頁面答應處理
		pagePrint = function (printDivId) {
			var keepAttr = (3)["class", "id", "style"];
			var headElements = "<meta charset=\"utf-8\" />,<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />";
			var options = {
				mode: 'popup', popClose: false, extraCss: "", retainAttr: keepAttr, standard: 'html5', extraHead: headElements
			};
			$(_getId(printDivId)).printArea(options);
		},

		// 打印后恢復頁面最初狀態設置
		printAfterSetting = function (btnTheirPapersId, btnMakeCorrectionsId, btnOverId) {
			// 錯題圖標隱藏
			$("img[id*='imgOK']").each(function (index, element) {
				$(element).hide();
			});
			// 對題圖標隱藏
			$("img[id*='imgNo']").each(function (index, element) {
				$(element).hide();
			});

			// 交卷按鈕隱藏
			$(_getId(btnTheirPapersId)).hide();
			// 訂正按鈕隱藏
			$(_getId(btnMakeCorrectionsId)).hide();
			// 完成按鈕隱藏
			$(_getId(btnOverId)).hide();
		},

		// 顯示上一次答題結果
		lastTimeRestore = function (oldSpanId, rightSpanId, faultSpanId) {
			// 獲取內容
			var result = store.get('result');
			// 前次用時顯示
			$(_getId(oldSpanId)).text(result.time);
			// 前次答對數顯示
			$(_getId(rightSpanId)).text(result.right);
			// 前次答錯數顯示
			$(_getId(faultSpanId)).text(result.fault);
		},


		// 讀秒計時器(當準備按鈕按下后,答題開始,計時器開始計時)
		startTime = function (spanSSId, spanMMId, spanHHId) {
			// 停止計時
			if (__isStop) {
				return;
			}

			__second++;
			// 讀秒
			$(_getId(spanSSId)).text(_checkTime(__second));
			// 讀分
			__minute = parseInt($(_getId(spanMMId)).text());
			// 60秒后,分鐘加1,秒針歸0
			if (__second == 60) {
				__minute++;
				__second = 0;
				$(_getId(spanMMId)).text(_checkTime(__minute));
				$(_getId(spanSSId)).text(_checkTime(__second));
			}
			// 讀時
			__hour = parseInt($(_getId(spanHHId)).text());
			// 60分后,時針加1,分針歸0
			if (__minute == 60) {
				__hour++;
				__minute = 0;
				$(_getId(spanHHId)).text(_checkTime(__hour));
				$(_getId(spanMMId)).text(_checkTime(__minute));
			}
			// 59:59:59為計時器上限值,將強制停止計時器
			if (__second == 59 && __minute == 59 && __hour == 59) {
				__isStop = true;
			}
			// 定時執行(設定時間間隔為1秒)
			setTimeout("startTime('" + spanSSId + "', '" + spanMMId + "', '" + spanHHId + "')", 1000)
		},

		// 訂正按鈕處理
		makeCorrections = function (fault, btnMakeCorrectionsId, btnOverId) {
			// 是否存在錯題
			if (fault != 0) {
				// 存在:訂正按鈕顯示
				$(_getId(btnMakeCorrectionsId)).show();
			} else {
				// 不存在：訂正按鈕隱藏，完成按鈕顯示
				$(_getId(btnMakeCorrectionsId)).hide();
				$(_getId(btnOverId)).show();
			}
		},

		// 交卷按鈕處理
		theirPapers = function (rightSpanId, faultSpanId, btnMakeCorrectionsId, btnOverId, btnTheirPapersId) {
			// 顯示答對題數
			$(_getId(rightSpanId)).text(__isRight);
			// 顯示答錯題數
			$(_getId(faultSpanId)).text(__isFault);

			if (__isFault != 0) {
				$(_getId(btnMakeCorrectionsId)).show();
			} else {
				$(_getId(btnOverId)).show();
			}
			$(_getId(btnTheirPapersId)).hide();

			var score = Math.round(__isRight / (__isRight + __isFault) * 10);
			$('#hidPracticeScore').val(score);

			$('#more-rating').show();

			// 打星處理
			var ratingOptions = {
				selectors: {
					starsSelector: '.rating-stars',
					starSelector: '.rating-star',
					starActiveClass: 'is--active',
					starHoverClass: 'is--hover',
					starNoHoverClass: 'is--no-hover',
					targetFormElementSelector: '.rating-value'
				}
			};
			// 星星打分評級js插件初始化
			$(".rating-stars").ratingStars(ratingOptions);
			// 移除星星的各類事件
			$(".rating-star").each(function (index, element) {
				$(element).unbind('mouseenter');
				$(element).unbind('mouseleave');
				$(element).unbind('click touchstart');
			});

		},

		// 計時停止（答題結果設定）
		timeStop = function (spanSSId, spanMMId, spanHHId) {
			__isStop = true;

			// 存儲變量到result鍵（答題結果）
			var timeStr = $(_getId(spanHHId)).text() + '：' + $(_getId(spanMMId)).text() + '：' + $(_getId(spanSSId)).text();
			store.set('result', { time: timeStr, right: __isRight, fault: __isFault });
		},

		overShow = function (e) {
			$(e).css('background-color', 'crimson');
		},

		outHide = function (e) {
			$(e).css('background-color', 'darkred');
		},

		totopClick = function () {
			$('html,body').animate({
				scrollTop: 0
			})
		},

		// 按鍵屏蔽防止刷新頁面
		forbidKeyDown = function () {
			$(document).bind("keydown", function (e) {
				var e = window.event || e;
				// 屏蔽 Alt+ 方向鍵 ←
				// 屏蔽 Alt+ 方向鍵 →
				if ((e.altKey) && ((e.keyCode == 37) || (e.keyCode == 39))) {
					e.returnValue = false;
					return false;
				}

				// 屏蔽退格刪除鍵
				if (e.keyCode == 8) {
					if (document.activeElement.tagName.toLowerCase() == 'input'.toLowerCase()) {
						var typeName = document.activeElement.type.toLowerCase();
						if (typeName == 'text'.toLowerCase() || typeName == 'password'.toLowerCase()) {
							if (!document.activeElement.readOnly) {
								return true;
							}
						}
					} else if (document.activeElement.tagName.toLowerCase() == 'textarea'.toLowerCase()) {
						if (!document.activeElement.readOnly)
							return true;
					}
					return false;
				}

				// 屏蔽F5刷新建
				if (e.keyCode == 116) {
					return false;
				}

				// 屏蔽alt+R
				if ((e.ctrlKey) && (e.keyCode == 82)) {
					return false;
				}
			});
		};

	return {
		pagePrint: pagePrint,
		printAfterSetting: printAfterSetting,
		lastTimeRestore: lastTimeRestore,
		makeCorrections: makeCorrections,
		theirPapers: theirPapers,
		timeStop: timeStop,
		forbidKeyDown: forbidKeyDown,
		windowClose: windowClose,
		startTime: startTime,
		overShow: overShow,
		outHide: outHide,
		totopClick: totopClick,
		ready: ready
	};
}());

// 頁面啟動后加載
$(document).ready(function () {
	// 準備
	$('#btnReady').click(function () { btnReadyClick(); });
	// 提交答題
	$('#btnTheirPapers').click(function () { btnTheirPapersClick(); });
	// 訂正錯題
	$('#btnMakeCorrections').click(function () { btnMakeCorrectionsClick(); });
	// 完成
	$('#btnOver').click(function () { btnOverClick(); });
	// 打印
	$('#btnPrint').click(function () { btnPrintClick(); });

	// 按鍵屏蔽防止刷新頁面
	MathSheets.Common.forbidKeyDown();
	// 禁用右鍵點擊功能
	$(document).bind("contextmenu", function (e) {
		return false;
	});
	$('.totop').bind("mouseover", function () { MathSheets.Common.overShow(this); });
	$('.totop').bind("mouseout", function () { MathSheets.Common.outHide(this); });
	$('.totop').bind("click", function () { MathSheets.Common.totopClick(); });

	// 置頂導航功能
	$(window).scroll(function () {
		var nowTop = $(document).scrollTop();
		if (nowTop > 200) {
			$('.totop').show()
		} else {
			$('.totop').hide()
		}
	});

	// 還原上一次答題結果
	MathSheets.Common.lastTimeRestore('spanOld', 'spanOldOK', 'spanOldNo');
	// 計算式提示
	$(function () { $("[data-toggle='tooltip']").tooltip(); });
});

String.prototype.PadLeft = function (totalWidth, paddingChar) {
	if (paddingChar != null) {
		return this.PadHelper(totalWidth, paddingChar, false);
	} else {
		return this.PadHelper(totalWidth, ' ', false);
	}
}

String.prototype.PadRight = function (totalWidth, paddingChar) {
	if (paddingChar != null) {
		return this.PadHelper(totalWidth, paddingChar, true);
	} else {
		return this.PadHelper(totalWidth, ' ', true);
	}
}

String.prototype.PadHelper = function (totalWidth, paddingChar, isRightPadded) {
	if (this.length < totalWidth) {
		var paddingString = new String();
		for (i = 1; i <= (totalWidth - this.length); i++) {
			paddingString += paddingChar;
		}

		if (isRightPadded) {
			return (this + paddingString);
		} else {
			return (paddingString + this);
		}
	} else {
		return this;
	}
}

Array.prototype.indexOf = function (val) {
	for (var i = 0; i < this.length; i++) {
		if (this[i] == val) return i;
	}
	return -1;
}

Array.prototype.remove = function (val) {
	var index = this.indexOf(val);
	if (index > -1) {
		this.splice(index, 1);
	}
}