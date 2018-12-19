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
// 所有可輸入項目的集合
var __allInputElementArray = new Array();
// 錯題集
var __allFaultInputElementArray = new Array();

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
			// window.close();
			//open(location, '_self').close();
			window.location.href = "about:blank";
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
			if (typeof result !== typeof undefined && result !== false) {
				// 前次用時顯示
				$(_getId(oldSpanId)).text(result.time);
				// 前次答對數顯示
				$(_getId(rightSpanId)).text(result.right);
				// 前次答錯數顯示
				$(_getId(faultSpanId)).text(result.fault);
			}
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
			// 23:59:59為計時器上限值,將強制停止計時器
			if (__second == 59 && __minute == 59 && __hour == 23) {
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
				// 錯題項目獲得光標并選中
				_faultInputElement();
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

			// 答題正確率統計(評星級)
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
			// 錯題項目獲得光標并選中
			_faultInputElement();
			// 如果答題滿分的話則顯示獎章
			_setAward(score);
		},

		// 錯題項目獲得光標并選中
		_faultInputElement = function () {
			if (__allFaultInputElementArray.length != 0) {
				setTimeout(function () {
					_setLocation(__allFaultInputElementArray[0]);
					__allFaultInputElementArray.length = 0;
				}, 1000);
			}
		},

		// 頁面滾動條移動至指定位置
		_setLocation = function (faultItem) {
			// 設置滾動高度
			$('html,body').animate({
				scrollTop: $(_getId(faultItem.position)).offset().top
			}, 1500, "easeOutQuint", function () {
				// 高亮選中當前輸入框
				if (faultItem.id != null) {
					$(_getId(faultItem.id)).select();
				}
			});
		},

		// 顯示獎章
		_setAward = function (score) {
			// 如果答題滿分的話則顯示獎章
			if (score != 10) {
				return;
			}

			// 置頂處理
			toTop();
			// 精彩瞬間應該先等上4秒鐘 :-!
			setTimeout(function () {
				// 隨機設定獎章
				var path = String.format("../Content/image/honor/award{0}.png", _getRandom(4));
				$(".imgAward").attr('src', path);
				// 顯示獎章 :-)
				$(".imgAward").fadeIn(5000);
			}, 2000);
		},

		// 隨機數取得
		_getRandom = function (n) {
			return Math.floor(Math.random() * n + 1);
		},

		// 計時停止（答題結果設定）
		timeStop = function (spanSSId, spanMMId, spanHHId) {
			__isStop = true;

			// 存儲變量到result鍵（答題結果）
			var timeStr = $(_getId(spanHHId)).text() + '：' + $(_getId(spanMMId)).text() + '：' + $(_getId(spanSSId)).text();
			store.set('result', { time: timeStr, right: __isRight, fault: __isFault });
		},

		// 鼠標移入置頂導航鍵
		overShow = function (e) {
			$(e).css('background-color', 'crimson');
		},

		// 鼠標移出置頂導航鍵
		outHide = function (e) {
			$(e).css('background-color', 'darkred');
		},

		// 鼠標移入頁面頂端導航區域時，浮動菜單顯示
		overNavbarShow = function () {
			$(".box").slideDown(500, function () {
			});
		},

		// 鼠標移出浮動菜單區域時，浮動菜單關閉
		outNavbarHide = function () {
			$(".box").slideUp(500, function () {
			});
		},

		// 置頂導航
		toTop = function () {
			// 移除滾動條事件（由導航鍵事件控制本身的隱藏）
			$(window).unbind('scroll');
			// 置頂動畫處理
			$('html,body').animate({
				scrollTop: 0
			}, 1500, "easeOutQuint", function () {
				// 向上箭頭隱藏
				$(".imgHelper-up").hide();
				setTimeout(function () {
					$('.totop').hide(400);
					// 待導航鍵隱藏后回復窗體滾動條事件
					$(window).bind("scroll", function () { windowScroll(); });
				}, 500);
			});
		},

		// 移動至畫面最底端
		toBottom = function () {
			// 置頂動畫處理
			$('html,body').animate({
				scrollTop: $('.div-company').offset().top
			}, 1500, "easeOutQuint", function () { $('.totop').show(400); });
			// 向下箭頭隱藏
			$(".imgHelper-down").hide();
		},

		// 窗體滾動條事件
		windowScroll = function () {
			var nowTop = $(document).scrollTop();
			if (nowTop > 200) {
				// 置頂導航鍵顯示
				$('.totop').show()
			} else {
				// 置頂導航鍵隱藏
				$('.totop').hide();
			}
		},

		// link信息設定處理
		_linkSetting = function (style) {
			// 查詢指定的樣式文件并設置為可用
			$("link[title='" + style + "']").removeAttr("disabled");
			// 遍歷其他樣式文件并設置為不可使用
			$("link[title!='" + style + "']").each(function () {
				var attr = $(this).attr('title');
				// 設定對象僅限於有效的可替換對象（即有title的樣式庫）
				if (typeof attr !== typeof undefined && attr !== false) {
					// 設定其為無效樣式
					$(this).attr("disabled", "disabled");
				}
			});
		},

		// 主題選擇事件
		styleSelect = function (e) {
			// 當前被點選的li元素對象
			var style = $(e).attr("id");
			// link信息設定處理
			_linkSetting(style);

			// 將當前選擇寫入cookie并設定30天有效 <- 暫無對應方法，因為chrome只在http請求下啟用cookie有效
			// $.cookie("mystyle", style, { expires: 30, path: '/' });
			// 使用本地儲存實現style的緩存(TODO: 可以考慮對時效性的功能擴展)
			store.set('mystyle', style);
		},

		// 頁面主題初期化設置
		styleInitialize = function () {
			// var cookie_style = $.cookie().mystyle;	 <- 暫無對應方法，因為chrome只在http請求下啟用cookie有效
			// 使用本地儲存實現style的緩存(TODO: 可以考慮對時效性的功能擴展)
			var cookie_style = store.get('mystyle');
			if (typeof cookie_style !== typeof undefined && cookie_style !== false) {
				// link信息設定處理
				_linkSetting(cookie_style);
			} else {
				$("link[title='default']").removeAttr("disabled");
			}
		},

		// 右側導航欄的初期化設置
		sidebarInitialize = function () {
			var html = '<li><a href="#divContainer">主題</a></li>';
			$("h4[id*='mathSheet']").each(function () {
				html += '<li><a href="#' + $(this).attr('id') + '">' + $($(this).children('span')).text() + '</a></li>';
			});

			$("ul[class='nav nav-ext']").prepend(html);
			/*
			if ($("div[id='divSidebar']").find("li").length < 4) {
				$("div[id='divSidebar']").hide();
			}
			*/
		},

		// 頁面向下滾動
		scrollAutoDown = function () {
			// 滾動條高度
			var scrollTop = $(window).scrollTop();
			// 頁面總高度
			var scrollHeight = $(document).height();
			// 窗體顯示的高度
			var windowHeight = $(window).height();
			// 如果滾動條已經到達頁面底部，則關閉當前自動移動
			if (scrollTop + windowHeight == scrollHeight) {
				// 向下箭頭隱藏
				$(".imgHelper-down").hide();
				return;
			}
			// 向下移動
			window.scrollTo(0, window.pageYOffset + 10);
		},

		// 頁面向上滾動
		scrollAutoUp = function () {
			// 滾動條高度
			var scrollTop = $(window).scrollTop();
			// 如果滾動條已經到達頁面頂部，則關閉當前自動移動
			if (scrollTop == 0) {
				// 向上箭頭隱藏
				$(".imgHelper-up").hide();
				return;
			}
			// 向上移動
			window.scrollTo(0, window.pageYOffset - 10);
		},

		// 頁面滾動輔助按鍵顯示控制處理
		scrollAutoHelper = function (clientY) {
			// 窗體顯示的高度
			var windowHeight = $(window).height();
			// 指定區域內顯示顯示
			if (clientY > (windowHeight * 0.8 - 120)) {
				// 滾動條高度
				var scrollTop = $(window).scrollTop();
				// 頁面總高度
				var scrollHeight = $(document).height();
				// 如果滾動條已經到達頁面底部，則關閉當前自動移動
				if (scrollTop + windowHeight >= scrollHeight - 1) {
					// 向下箭頭隱藏
					$(".imgHelper-down").hide();
					return;
				}
				// 向下箭頭顯示
				$(".imgHelper-down").show();
				// 向上箭頭隱藏
				$(".imgHelper-up").hide();
			} else if (clientY < (windowHeight * 0.2 + 120)) {
				// 滾動條高度
				var scrollTop = $(window).scrollTop();
				// 如果滾動條已經到達頁面頂部，則關閉當前自動移動
				if (scrollTop == 0) {
					// 向上箭頭隱藏
					$(".imgHelper-up").hide();
					return;
				}
				// 向上箭頭顯示
				$(".imgHelper-up").show();
				// 向下箭頭隱藏
				$(".imgHelper-down").hide();
			} else {
				// 向下箭頭隱藏
				$(".imgHelper-down").hide();
				// 向上箭頭隱藏
				$(".imgHelper-up").hide();
			}
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
		overNavbarShow: overNavbarShow,
		outNavbarHide: outNavbarHide,
		toTop: toTop,
		toBottom: toBottom,
		styleSelect: styleSelect,
		styleInitialize: styleInitialize,
		sidebarInitialize: sidebarInitialize,
		scrollAutoUp: scrollAutoUp,
		scrollAutoDown: scrollAutoDown,
		scrollAutoHelper: scrollAutoHelper,
		ready: ready,
		windowScroll: windowScroll
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

	// 還原上一次答題結果
	MathSheets.Common.lastTimeRestore('spanOld', 'spanOldOK', 'spanOldNo');

	// 按鍵屏蔽防止刷新頁面
	MathSheets.Common.forbidKeyDown();
	// 禁用右鍵點擊功能
	$(document).bind("contextmenu", function (e) { return false; });

	// 當頁面超長時顯示輔助滾輪
	if ($(document).height() > 2000) {
		/* 
		 * 與 mouseover 事件不同，只有在鼠標指針穿過被選中的元素時，才會觸發 mouseenter 事件。
		 * 如果鼠標指針穿過任何子元素，同樣會觸發 mouseover 事件。 
		 */
		// 答題區域鼠標移動事件（用於顯示頁面自動滾動輔助按鍵）
		$('#divContainer').bind('mouseover', function (e) {
			MathSheets.Common.scrollAutoHelper(e.clientY);
		});
	}

	var backId;
	// 頁面向上滾動
	$('.imgHelper-up').bind('mouseenter', function () {
		// TODO 使用tween.js緩動算法改善動畫效果
		backId = setInterval(MathSheets.Common.scrollAutoUp, 100);
	}).bind('mouseleave', function () {
		clearInterval(backId);
	}).bind('click', function () {
		MathSheets.Common.toTop();
	});

	// 頁面向下滾動
	$('.imgHelper-down').bind('mouseenter', function () {
		// TODO 使用tween.js緩動算法改善動畫效果
		backId = setInterval(MathSheets.Common.scrollAutoDown, 100);
	}).bind('mouseleave', function () {
		clearInterval(backId);
	}).bind('click', function () {
		MathSheets.Common.toBottom();
	});

	// 鼠標移入置頂導航鍵(高亮效果)
	$('.totop').bind("mouseenter", function () { MathSheets.Common.overShow(this); });
	// 鼠標移出置頂導航鍵
	$('.totop').bind("mouseleave", function () { MathSheets.Common.outHide(this); });
	// 點擊置頂導航鍵
	$('.totop').bind("click", function () { MathSheets.Common.toTop(); });
	// 窗體滾動條事件
	$(window).bind("scroll", function () { MathSheets.Common.windowScroll(); });
	// 鼠標移入頁面頂端導航區域時，浮動菜單顯示
	$('.imgNavbar').bind("mouseenter", function () { MathSheets.Common.overNavbarShow(); });
	// 鼠標移出浮動菜單區域時，浮動菜單關閉
	$('#close').bind("click", function () { MathSheets.Common.outNavbarHide(); });
	// 主題選擇事件
	$('.switcher li').bind("click", function () { MathSheets.Common.styleSelect(this); });

	// 計算式提示
	$(function () { $("[data-toggle='tooltip']").tooltip(); });
	// 頁面主題初期化設置
	MathSheets.Common.styleInitialize();
	// 右側導航欄的初期化設置
	MathSheets.Common.sidebarInitialize();
});


/* 
 * 以下為javascript屬性、方法擴展 
 */
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

String.format = function () {
	if (arguments.length == 0) {
		return "";
	}
	var str = arguments[0];
	for (var i = 1; i < arguments.length; i++) {
		var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
		str = str.replace(re, arguments[i]);
	}
	return str;
}