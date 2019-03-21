"use strict";

/**
 * @description 用於記錄答題正確的數量
 * @var {int} __isRight 答題正確的數量
 * @default 0
 */
var __isRight = 0;

/**
 * @description 用於記錄答題錯誤的數量
 * @var {int} __isFault 答題錯誤的數量
 * @default 0
 */
var __isFault = 0;

/**
 * @description 記錄頁面所有可輸入項目的控件ID集合，用於控制光標移動
 * @var {Array} __allInputElementArray 可輸入項目的控件ID集合
 */
var __allInputElementArray = new Array();

/**
 * @file 通用處理
 * @description 作為其他JS模塊的通用處理，提供統一的功能實現
 * 
 * @author TonyZhangshi <tonyzhangshi@163.com>
 * @version 0.1
 * @copyright Tony Zhangshi 2018
 */
var MathSheets = MathSheets || {};
MathSheets.Common = MathSheets.Common || (function () {

	/**
	 * @description 用於做題計時使用
     * @private
	 * @property {int} _second 單位：秒
	 * @default 0
	 */
	var _second = 0;

	/**
	 * @description 用於做題計時使用
     * @private
	 * @property {int} _minute 單位：分
	 * @default 0
	 */
	var _minute = 0;

	/**
	 * @description 用於做題計時使用
     * @private
	 * @property {int} _hour 單位：時
	 * @default 0
	 */
	var _hour = 0;

	/**
	 * @description 用於停止計時器
     * @private
	 * @property {int} _isStop 是否停止計時
	 * @example true：停止
	 * @default false
	 */
	var _isStop = false;

	/**
	 * @description 獲取對應模塊距離頂端的距離（開始和結束）
     * @private
	 * @property {Array} _itemTops 獲取對應模塊距離頂端的距離
	 */
	var _itemTops = new Array();

	/**
	 * @description 導航欄列表控件對象集合
     * @private
	 * @property {int} _navlist 導航欄列表控件對象集合
	 * @default null
	 */
	var _navlist = null;

	/**
	 * @description 當前Active狀態的輸入框索引號
     * @private
	 * @property {int} _sequence 當前Active狀態的輸入框索引號
	 * @default 0
	 * @example 配合使用: __allInputElementArray[_sequence] 取得當前可輸入項目的控件ID
	 */
	var _sequence = 0;

	/**
	 * @description 頁面總高度
     * @private
	 * @property {int} _scrollHeight 頁面總高度
	 */
	var _scrollHeight;

	/**
	 * @description 窗體顯示的高度
     * @private
	 * @property {int} _windowHeight 窗體顯示的高度
	 */
	var _windowHeight;

	/**
	 * @description 用於記錄遊戲序號，確保答題全對後只能玩指定序號的遊戲
     * @private
	 * @property {int} _gameId 遊戲序號（1：貪吃蛇 2：打方塊）
	 * @default null
	 */
	var _gameId;

    /**
     * @description 取得控件ID的特定形式
     * @private
     * @param id {String} 控件ID
     * @return {string} 控件ID的特定形式
     * @example 'control01' return '#control01'
     */
	function _getId(id) {
		return '#' + id;
	}

    /**
     * @description 計時器format顯示（不足十位向前補0）
     * @private
     * @param i {int} 時間數值
     * @return {string} 補0後數值顯示
     * @example '2' return '02'
     */
	function _checkTime(i) {
		if (i < 10) { i = "0" + i }
		return i
	}

    /**
     * @description 錯題項目獲得光標并選中
     * @private
     */
	function _faultInputElement() {
		if (__allInputElementArray.length != 0) {
			setTimeout(function () {
				_setLocation(__allInputElementArray[0]);
				_sequence = 0;
				//__allInputElementArray.length = 0;
			}, 1000);
		}
	}

    /**
     * @description 頁面滾動條移動至指定位置（用於錯題輸入框select）
     * @private
     * @param inputItem {object} 錯題對象
     */
	function _setLocation(inputItem) {
		// 設置滾動高度
		$('html,body').animate({
			scrollTop: $(_getId(inputItem.position)).offset().top
		}, 500, "easeOutQuint", function () {
			// 高亮選中當前輸入框
			if (inputItem.id != null) {
				$(_getId(inputItem.id)).focus().select();
			}
		});
	}

    /**
     * @description 顯示獎章
     * @private
     */
	function _setAward() {

		// 如果答題滿分的話則顯示獎章
		if (__isFault != 0) {
			return;
		}

		// 置頂處理
		toTop();

		// 恭喜答對滿分過關
		setTimeout(MathSheets.HelloMrTony.doCelebrate(), 1000);

		// 精彩瞬間應該先等上3秒鐘 :-!
		setTimeout(function () {
			// 隨機設定獎章
			var path = String.format("../Content/image/honor/award{0}.png", _getRandomInt(11));
			$(".imgAward").attr('src', path);
			// 顯示獎章 :-)
			$(".imgAward").fadeIn(2000, function () {
				$(this).animate({ width: "290px", height: "290px" }, "slow", null, function () {
					$(".honorArea").animate({ "top": "-30px", }, 300)
						.animate({ "top": "-15px", }, 100)
						.animate({ "top": "-28px", }, 100)
						.animate({ "top": "-15px", }, 100)
						.animate({ "top": "-25px", }, 50)
						.animate({ "top": "-15px", }, 50)
						.animate({ "top": "-22px", }, 50)
						.animate({ "top": "-15px", }, 10)
						.animate({ "top": "-18px", }, 10)
						.animate({ "top": "-15px", }, 10)
				});
			});
		}, 3000);
	}

    /**
     * @description 隨機數取得
     * @private
     * @param max {int} 隨機數上限值
	 * @returns {int} 隨機數
     */
	function _getRandomInt(max) {
		// 從最小值0開始計數（隨機數中不包含max值）
		return Math.floor(Math.random() * Math.floor(max));
	}

	/**
     * @description link信息設定處理（用於主題切換）
     * @private
     * @param style {string} 樣式庫名
     */
	function _linkSetting(style) {
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
	}

	/**
     * @description 取得下一個輸入框的索引號
     * @private
     * @param currentSelectedId {string} 當前輸入項目的控件ID
     * @param keyCode {string} 按鍵Code
	 * @returns {int} 輸入框的索引號
     */
	function _getNextInputFocusSequence(currentSelectedId, keyCode) {
		// 當前focus項目取得
		var f = $("input:focus");
		if (f.length == 0) {
			return (keyCode == 39) ? -1 : 1;
		}
		// Active輸入域的id
		var selectedId = f.attr("id");
		// 當前輸入框是active狀態
		if (selectedId == currentSelectedId) {
			return _sequence;
		}
		// 遍歷全部輸入域
		$.each(__allInputElementArray, function (index, e) {
			if (selectedId == e.id) {
				// 重新定位Active輸入域的索引號
				_sequence = index;
				return false;
			}
		});
		return _sequence;
	}

	/**
     * @description 打開新窗口
     * @private
     * @param url {string} 新窗口顯示的URL
     * @param name {string} 新窗口的標題
     * @param width {string} 新窗口的大小
     * @param height {string} 新窗口的高度
     */
	function _openWindow(url, name, width, height) {
		// 獲取窗口的垂直位置
		var top = (window.screen.availHeight - 30 - height) / 2;
		// 獲取窗口的垂水平位置;
		var left = (window.screen.availWidth - 10 - width) / 2;
		window.open(url, name, 'height=' + height + ',,innerHeight=' + height + ',width=' + width + ',innerWidth=' + width + ',top=' + top + ',left=' + left + ',toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no');
	}

    /**
     * @description 頁面關閉處理
     * @method windowClose
     */
	windowClose = function () {
		// window.close();
		//open(location, '_self').close();
		window.location.href = "about:blank";
		window.close();
	},

        /**
         * @description 設定頁面所有輸入域為和按鍵的可用狀態
         * @method ready
         * @param btnTheirPapersId {string} 交卷按鈕的控件ID
         * @param btnReadyId {string} 準備按鈕的控件ID
         * @param btnPrintId {string} 打印按鈕的控件ID
         */
		ready = function (btnTheirPapersId, btnReadyId, btnPrintId) {
			// 交卷按鈕顯示
			$(_getId(btnTheirPapersId)).show();
			// 準備按鈕隱藏
			$(_getId(btnReadyId)).hide();
			// 打印按鈕隱藏
			$(_getId(btnPrintId)).hide();

			setTimeout(function () {
				// 頁面第一個輸入域設置Active
				$('#divPrintContent').find(':text').first().focus();
			}, 1000);

			// 虛擬人物開始說話了
			MathSheets.HelloMrTony.readyComplete();
		},

        /**
         * @description 頁面打印處理
         * @method pagePrint
         * @param printDivId {String} 打印區域的控件ID
         */
		pagePrint = function (printDivId) {
			var keepAttr = (3)["class", "id", "style"];
			var headElements = "<meta charset=\"utf-8\" />,<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />";
			var options = {
				mode: 'popup', popClose: false, extraCss: "", retainAttr: keepAttr, standard: 'html5', extraHead: headElements
			};
			$(_getId(printDivId)).printArea(options);
		},

        /**
         * @description 打印后恢復頁面最初狀態設置
         * @method printAfterSetting
         * @param btnTheirPapersId {String} 交卷按鈕的控件ID
         * @param btnMakeCorrectionsId {String} 訂正按鈕的控件ID
         * @param btnOverId {String} 完成按鈕的控件ID
         */
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

        /**
         * @description 顯示前一次做題的答題情況
         * @method lastTimeRestore
         * @param oldSpanId {String} 顯示用時的控件ID
         * @param rightSpanId {String} 顯示答題正確數量的控件ID
         * @param faultSpanId {String} 顯示答題錯誤數量的控件ID
         */
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

        /**
         * @description 讀秒計時器(當準備按鈕按下后,答題開始,計時器開始計時)
         * @method startTime
         * @param spanSSId {String} 顯示秒的控件ID
         * @param spanMMId {String} 顯示分的控件ID
         * @param spanHHId {String} 顯示時的控件ID
         */
		startTime = function (spanSSId, spanMMId, spanHHId) {
			// 停止計時
			if (_isStop) {
				return;
			}

			_second++;
			// 讀秒
			$(_getId(spanSSId)).text(_checkTime(_second));
			// 讀分
			_minute = parseInt($(_getId(spanMMId)).text());
			// 60秒后,分鐘加1,秒針歸0
			if (_second == 60) {
				_minute++;
				_second = 0;
				$(_getId(spanMMId)).text(_checkTime(_minute));
				$(_getId(spanSSId)).text(_checkTime(_second));
			}
			// 讀時
			_hour = parseInt($(_getId(spanHHId)).text());
			// 60分后,時針加1,分針歸0
			if (_minute == 60) {
				_hour++;
				_minute = 0;
				$(_getId(spanHHId)).text(_checkTime(_hour));
				$(_getId(spanMMId)).text(_checkTime(_minute));
			}
			// 23:59:59為計時器上限值,將強制停止計時器
			if (_second == 59 && _minute == 59 && _hour == 23) {
				_isStop = true;
			}
			// 定時執行(設定時間間隔為1秒)
			setTimeout("startTime('" + spanSSId + "', '" + spanMMId + "', '" + spanHHId + "')", 1000)
		},

        /**
         * @description 訂正按鈕處理（批改錯題）
         * @method makeCorrections
         * @param fault {int} 錯題數量
         * @param btnMakeCorrectionsId {String} 訂正按鈕的控件ID
         * @param btnOverId {String} 完成按鈕的控件ID
         */
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

        /**
         * @description 交卷按鈕處理（批改錯題）
         * @method theirPapers
         * @param rightSpanId {String} 顯示答題正確數量的控件ID
         * @param faultSpanId {String} 顯示答題錯誤數量的控件ID
         * @param btnMakeCorrectionsId {String} 訂正按鈕的控件ID
         * @param btnOverId {String} 完成按鈕的控件ID
         * @param btnTheirPapersId {String} 交卷按鈕的控件ID
         */
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
			_setAward();

			// 交卷操作已經完成
			MathSheets.HelloMrTony.theirPapersComplete(score);
		},

        /**
         * @description 計時停止(並顯示答題情況)
         * @method timeStop
         * @param spanSSId {String} 顯示秒的控件ID
         * @param spanMMId {String} 顯示分的控件ID
         * @param spanHHId {String} 顯示時的控件ID
         */
		timeStop = function (spanSSId, spanMMId, spanHHId) {
			_isStop = true;

			// 存儲變量到result鍵（答題結果）
			var timeStr = $(_getId(spanHHId)).text() + '：' + $(_getId(spanMMId)).text() + '：' + $(_getId(spanSSId)).text();
			store.set('result', { time: timeStr, right: __isRight, fault: __isFault });
		},

        /**
         * @description 鼠標移入頁面頂端導航區域時，浮動菜單顯示（4秒后自動隱藏）
         * @method overNavbarShow
         */
		overNavbarShow = function () {
			$(".box").slideDown(500, function () {
				var realTimeClData = setInterval(function () {
					$(".box").slideUp(500);
					clearInterval(realTimeClData);
				}, 4000);
			});
		},

        /**
         * @description 鼠標移出浮動菜單區域時，浮動菜單關閉
         * @method outNavbarHide
         */
		outNavbarHide = function () {
			$(".box").slideUp(500, function () {
			});
		},

        /**
         * @description 導航置頂處理
         * @method toTop
         */
		toTop = function () {
			// 移除滾動條事件（由導航鍵事件控制本身的隱藏）
			//$(window).unbind('scroll');
			// 置頂動畫處理
			$('html,body').animate({
				scrollTop: 0
			}, 2500, "easeOutQuint", function () {
				// 向上箭頭隱藏
				$(".imgHelper-up").hide();
				setTimeout(function () {
					$('.totop').hide(400);
					// 待導航鍵隱藏后回復窗體滾動條事件
					//$(window).bind("scroll", function () { windowScroll(); });
					//windowScroll();
				}, 500);
			});
		},

        /**
         * @description 移動至畫面最底端
         * @method toBottom
         */
		toBottom = function () {
			// 置頂動畫處理
			$('html,body').animate({
				scrollTop: $('.div-company').offset().top
			}, 1500, "easeOutQuint", function () { $('.totop').show(400); });
			// 向下箭頭隱藏
			$(".imgHelper-down").hide();
		},

        /**
         * @description 主題選擇事件
         * @method styleSelect
         * @param e {object} 被點選的li元素對象
         */
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

        /**
         * @description 頁面主題初期化設置
         * @method styleInitialize
         */
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

			// 是否顯示虛擬人物
			var isShowTony = store.get('isTony');
			if (isShowTony == 'show') {
				$('.imgClown').attr('src', '../Content/image/clown-color.png');
				MathSheets.HelloMrTony.showMrTony('on');
			} else {
				$('.imgClown').attr('src', '../Content/image/clown.png');
				MathSheets.HelloMrTony.showMrTony('off');
			}
		},

        /**
         * @description 右側導航欄的初期化設置
         * @method sidebarInitialize
         */
		sidebarInitialize = function () {
			_itemTops.length = 0;

			// 存放各模塊開始位置
			var begins = [];
			// 默認第一個導航欄為被選中狀態
			var html = '<li><img class="light"/><a href="#divContainer" class="active">主題</a></li>';
			begins[0] = 20;
			$("h4[id*='mathSheet']").each(function (index) {
				html += '<li><img class="light"/><a href="#' + $(this).attr('id') + '">' + $($(this).children('span')).text() + '</a></li>';
				// 獲取對應模塊距離頂端的距離
				begins[index + 1] = $(this).offset().top;
				//debug
				//console.log($(this).attr('id') + begins[index + 1]);
			});

			$.each(begins, function (index, begin) {
				var sheet = new Object();
				// 模塊索引號
				sheet.index = index;
				// 模塊開始位置
				sheet.begin = begin;
				// 判斷當前模塊是不是在頁面的末尾（當頁面只有1個題型模塊時也是相同處理）
				if (index == begins.length - 1) {
					// 當前模塊在頁面的末尾
					sheet.end = $(document).height() - 250;
				} else {
					// 結束位置為當前模塊的後一個模塊的開始位置
					sheet.end = begins[index + 1];
				}
				//debug
				//console.log(sheet.index + "|" + sheet.begin + "|" + sheet.end);
				_itemTops.push(sheet);
			});

			// 根據模塊動態作成導航欄
			$("ul[class='nav nav-ext']").prepend(html);
			// 獲取導航欄對象列表
			_navlist = $('.bs-docs-sidebar ul li a');

            /*
             * 題型數量小於4時不顯示右側導航欄
            if ($("div[id='divSidebar']").find("li").length < 4) {
                $("div[id='divSidebar']").hide();
            }
            */
		},

        /**
         * @description 窗體滾動條事件
         * @method sidebarInitialize
         */
		windowScroll = function () {
			// 滾動條距離窗體頂端的距離
			var scrollTop = $(window).scrollTop();

			//debug
			//console.log(scrollTop + "|" + (scrollTop + _windowHeight) + "|" + _windowHeight);

			// 可視範圍查詢
			var searchList = $.Enumerable.From(_itemTops)
				.Where(function (v) {
					return scrollTop <= v.begin && scrollTop + _windowHeight > v.end
				}).ToArray();
			// 優先考慮可視範圍內的模塊設置高亮、其後再考慮接近窗體頂端的模塊
			if (searchList.length > 0) {
				$.each(_navlist, function (index, nav) {
					if (index == searchList[0].index) {
						$(nav).addClass('active');
					} else {
						$(nav).removeClass('active');
					}
				});
			} else {
				// 遍歷所有模塊距離頂端的距離
				$.each(_itemTops, function (index, sheet) {
					if (scrollTop >= sheet.begin && scrollTop < sheet.end) {
						_navlist.eq(index).addClass('active');
					} else {
						_navlist.eq(index).removeClass('active');
					}
				});
			}

			if (scrollTop > 200) {
				// 置頂導航鍵顯示
				$('.totop').show()
			} else {
				// 置頂導航鍵隱藏
				$('.totop').hide();
			}
		},

        /**
         * @description 頁面向下滾動
         * @method scrollAutoDown
         */
		scrollAutoDown = function () {
			// 滾動條高度
			var scrollTop = $(window).scrollTop();
			// 頁面總高度
			//var scrollHeight = $(document).height();
			// 窗體顯示的高度
			//var windowHeight = $(window).height();
			// 如果滾動條已經到達頁面底部，則關閉當前自動移動
			if (scrollTop + _windowHeight == _scrollHeight) {
				// 向下箭頭隱藏
				$(".imgHelper-down").hide();
				return;
			}
			// 向下移動
			window.scrollTo(0, window.pageYOffset + 10);
		},

        /**
         * @description 頁面向上滾動
         * @method scrollAutoUp
         */
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

        /**
         * @description 頁面滾動輔助按鍵顯示控制處理
         * @method scrollAutoHelper
         * @param clientY {int} 滾動條的顯示位置高度
         */
		scrollAutoHelper = function (clientY) {
			// 窗體顯示的高度
			//var windowHeight = $(window).height();
			// 指定區域內顯示顯示
			if (clientY > (_windowHeight * 0.7)) {
				// 滾動條高度
				var scrollTop = $(window).scrollTop();
				// 頁面總高度
				//var scrollHeight = $(document).height();
				// 如果滾動條已經到達頁面底部，則關閉當前自動移動
				if (scrollTop + _windowHeight >= _scrollHeight - 1) {
					// 向下箭頭隱藏
					$(".imgHelper-down").hide();
					return;
				}
				// 向下箭頭顯示
				$(".imgHelper-down").show();
				// 向上箭頭隱藏
				$(".imgHelper-up").hide();
			} else if (clientY < 220) {
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

        /**
         * @description 答題后將正確的項目從列表中移除
         * @method removeInputElementArray
         */
		removeInputElementArray = function (objValue) {
			$.each(__allInputElementArray, function (index, item) {
				if (item.position == objValue.position && item.id == objValue.id) {
					__allInputElementArray.splice(index, 1);
					return false;
				}
			});
		},

        /**
         * @description 點擊獎牌可以玩遊戲
         * @method imgAwardClick
         */
		imgAwardClick = function () {
			// 避免遊戲外洩（一次做題玩盡所有遊戲！）
			if (_gameId == null) {
				_gameId = _getRandomInt(2);
			}
			switch (_gameId) {
				case 0:
					// 貪吃蛇遊戲
					_openWindow("../Games/snake/snake-game.html", "贪吃蛇", 440, 470);
					break;
				case 1:
					// 打方塊遊戲
					_openWindow("../Games/break the bricks/index.html", "打方块", 810, 605);
					break;
			}
		},

        /**
         * @description 是否啟用屏幕虛擬人物托尼
         * @method useClown
         */
		useClown = function () {
			if ($('.imgClown').attr('src') == '../Content/image/clown-color.png') {
				$('.imgClown').attr('src', '../Content/image/clown.png');
				MathSheets.HelloMrTony.showMrTony('off');
				store.set('isTony', 'hide');
			} else {
				$('.imgClown').attr('src', '../Content/image/clown-color.png');
				MathSheets.HelloMrTony.showMrTony('on');
				store.set('isTony', 'show');
			}
		},

        /**
         * @description 按鍵屏蔽防止刷新頁面
         * @method forbidKeyDown
         */
		forbidKeyDown = function () {
			$(document).bind("keydown", function (e) {
				var e = window.event || e;

				// 判斷鍵盤是按下了左方向鍵
				if (e.keyCode == 39) {
					if (__allInputElementArray.length == 0) {
						return false;
					}

					_sequence = _getNextInputFocusSequence(__allInputElementArray[_sequence].id, e.keyCode);
					if (_sequence == __allInputElementArray.length - 1) {
						_sequence = -1;
					}
					_sequence++;
					$(_getId(__allInputElementArray[_sequence].id)).focus().select();

					return false;
				}
				// 判斷鍵盤是按下了左方向鍵
				if (e.keyCode == 37) {
					if (__allInputElementArray.length == 0) {
						return false;
					}

					_sequence = _getNextInputFocusSequence(__allInputElementArray[_sequence].id, e.keyCode);
					if (_sequence == 0) {
						_sequence = __allInputElementArray.length;
					}
					_sequence--;
					$(_getId(__allInputElementArray[_sequence].id)).focus().select();

					return false;
				}
				// 判斷鍵盤是按下回車鍵（重新定位Active輸入域的索引號用於左移右移）
				if (e.keyCode == 13) {
					// 當前focus項目取得
					var f = $("input:focus");
					if (f.length == 0) {
						return false;
					}
					// Active輸入域的id
					var selectedId = f.attr("id");
					// 遍歷全部輸入域
					$.each(__allInputElementArray, function (index, e) {
						if (selectedId == e.id) {
							// 重新定位Active輸入域的索引號
							_sequence = index;
							return false;
						}
					});
				}

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
		imgAwardClick: imgAwardClick,
		ready: ready,
		windowScroll: windowScroll,
		useClown: useClown,
		removeInputElementArray: removeInputElementArray
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

	// 窗體顯示的高度
	_windowHeight = window.innerHeight;//$(window).height();
	// 頁面總高度
	_scrollHeight = $(document).height();
	// 當頁面超長時顯示輔助滾輪
	if (_scrollHeight > _windowHeight) {
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

	// 點擊置頂導航鍵
	$('.totop').bind("click", function () { MathSheets.Common.toTop(); });

	// 右側導航欄的初期化設置
	MathSheets.Common.sidebarInitialize();
	// 窗體滾動條事件
	$(window).bind("scroll", function () { MathSheets.Common.windowScroll(); });

	// 鼠標移入頁面頂端導航區域時，浮動菜單顯示
	$('.imgNavbar').bind("mouseenter", function () { MathSheets.Common.overNavbarShow(); });
	// 鼠標移出浮動菜單區域時，浮動菜單關閉
	$('#close').bind("click", function () { MathSheets.Common.outNavbarHide(); });
	// 主題選擇事件
	$('.switcher li').bind("click", function () { MathSheets.Common.styleSelect(this); });
	// 獎牌點擊事件
	$('.imgAward').bind("click", function () { MathSheets.Common.imgAwardClick(); });

	// 是否啟用托尼
	$('.imgClown').click(function () { MathSheets.Common.useClown(); });

	// 計算式提示
	$(function () { $("[data-toggle='tooltip']").tooltip(); });
	MathSheets.HelloMrTony.initialize();

	// 頁面主題初期化設置
	MathSheets.Common.styleInitialize();
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
