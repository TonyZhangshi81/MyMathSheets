
var MathSheets = MathSheets || {};

MathSheets.CurrencyLinkage = MathSheets.CurrencyLinkage || (function () {

	// DIV畫線點坐標列表
	var _arrCurrencyDivPoints = new Array();
	// DIV連線狀態列表
	var _arrCurrencyDrawLines = new Array();

	// 打印設置
	printSetting = function () {
		// 遍歷所有DIV并設置打印樣式
		$("div[class='divDrawLine-currency']").each(function (index, element) {
			$(element).addClass('divDrawLine-currency-print');
		});
	},

		// 打印后頁面設定
		printAfterSetting = function () {
			// 遍歷所有checkbox并設置為隱藏
			$("input[type='checkbox']").each(function (index, element) {
				$(element).hide();
			});
			// 遍歷所有DIV并還原打印前樣式
			$("div[class*='divDrawLine-currency']").each(function (index, element) {
				$(element).removeClass('divDrawLine-currency-print')
			});
		},

		// 答题验证(正确:true  错误:false)
		_currencyLinkageCorrecting = function () {
			var isRight = false;
			// 答案序列(divCl01S#divCl02E;divCl02S#divCl03E;divCl03S#divCl01E;.....)
			var answers = ($('#hidClAnswer').val() || '').split(';');
			$.each(answers, function (i, answer) {
				isRight = false;
				// 左邊算式答案
				var left = "#" + (answer || '').split('#')[0];
				// 右邊算式答案
				var right = "#" + (answer || '').split('#')[1];
				// 答題情況
				$.each(_arrCurrencyDrawLines, function (j, value) {
					// 如果答題中連線情況與答一致
					if (value.startPoint.name == left && value.endPoint.name == right) {
						isRight = true;
					}
				});
				if (!isRight) {
					return false;
				}
			});

			// 验证输入值是否与答案一致
			if (isRight) {
				// 对错图片显示和隐藏
				$('#imgOKCurrencyLinkage').show();
				$('#imgNoCurrencyLinkage').hide();
				// 移除圖片抖動特效
				$('#imgNoCurrencyLinkage').removeClass("shake shake-slow");
				// 移除DIV點擊事件(起始點和結束點)
				$.each(_arrCurrencyDivPoints, function (index, div) {
					$(div.name).unbind('click');
				});
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKCurrencyLinkage').hide();
				$('#imgNoCurrencyLinkage').show();
				$('#imgNoCurrencyLinkage').animate({
					width: "80px",
					height: "80px",
					marginLeft: "0px",
					marginTop: "0px"
				}, 1000, function () {
					// 添加圖片抖動特效（只針對錯題）
					$(this).addClass("shake shake-slow");
				});
				// 错误:false
				return false;
			}
		},

		// 设定页面所有输入域为可用状态(認識價格)
		ready = function () {
			// 畫線區域初期化
			_arrCurrencyAreaInit("#hidCurrencyInitSettings");
			// 畫線坐標點初期化
			_relativeLocationCurrencyPointInit("#hidCurrencyInitSettings");
			// DIV事件註冊(起始點和結束點)
			$.each(_arrCurrencyDivPoints, function (index, div) {
				// 遍歷每一個畫線坐標點
				(div.name.substring(div.name.length - 1) == 'S')
					? $(div.name).click(function () { MathSheets.CurrencyLinkage.btnDivStartClick(this); })
					: $(div.name).click(function () { MathSheets.CurrencyLinkage.btnDivEndClick(this); });
			});

		},

		// 畫線區域初期化
		_arrCurrencyAreaInit = function (hidCurrencyInitSettings) {
			// 畫線區域的參數取得(注意: 所有控件ID均帶有#)
			var settings = ($(hidCurrencyInitSettings).val() || "").split(',');
			// 第一排的左起第一個DIV ID
			var div1First = settings[0];
			// 第一排的右起第一個DIV ID
			var div1Last = settings[1];
			// 第二排的右起第一個DIV ID
			var divLastLast = settings[2];
			// 0:橫向 1:縱向
			var isCrosswise = (settings[3] == 0);
			// 畫線區域ID
			var svg = settings[4];
			// 初始化坐標點
			var width = 0, height = 0, left = 0, top = 0;

			// 如果是橫向排列
			if (isCrosswise) {
				// 畫線區域的寬度
				width = $(div1Last).position().left - $(div1First).position().left;
				// 畫線區域的高度
				height = $(divLastLast).position().top - $(div1Last).position().top - $(div1Last).outerHeight(true);
				// 畫線區域的相對位置
				left = $(div1First).position().left + $(div1First).outerWidth(true) / 2;
				top = $(div1First).position().top + $(div1First).outerHeight(true);
			}

			// 如果是縱向排列
			if (!isCrosswise) {
				// 畫線區域的寬度
				width = $(div1Last).position().left - $(div1First).position().left - $(div1First).outerWidth(true);
				// 畫線區域的高度
				height = $(divLastLast).position().top - $(div1Last).position().top;
				// 畫線區域的相對位置
				left = $(div1First).position().left + $(div1First).outerWidth(true);
				top = $(div1First).position().top + $(div1First).outerHeight(true) / 2;
			}
			// 畫線區域SVG屬性設定
			$(svg).css({ "left": left, "top": top });
			$(svg).attr("width", width);
			$(svg).attr("height", height);
		},

		// 畫線起始點,結束點坐標初期化
		_relativeLocationCurrencyPointInit = function (hidCurrencyInitSettings) {
			// 畫線區域的參數取得
			var settings = ($(hidCurrencyInitSettings).val() || "").split(',');

			// 0:橫向 1:縱向
			var isCrosswise = (settings[3] == 0);
			// 第一排的左起第一個DIV ID
			var div1First = settings[0];
			// 第一排的右起第一個DIV ID
			var div1Last = settings[1];
			// 第二排的(橫向:左起)第一個DIV ID
			var div2First = settings[2];
			// 畫線區域ID
			var svg = settings[4];
			// 遍歷所以DIV(連線對象)
			$("div[class='divDrawLine-currency']").each(function (index, element) {
				// 取得DIV對象的控件ID
				var id = "#" + $(element).attr("id");
				// 判斷是否為開始隊列還是結束隊列
				var isS = (id.substring(id.length - 1) == 'S');

				// DIV起始坐標點對象實例化
				var divPoint = new Object();
				// 名稱(DIV ID)
				divPoint.name = id;
				// 橫向排列的坐標算法
				if (isCrosswise) {
					if (isS) {
						// 橫坐標(畫線區域內的相對坐標)
						divPoint.x = $(id).offset().left - $(div1First).offset().left;
						// 縱坐標(畫線區域內的相對坐標)
						divPoint.y = 0
					} else {
						// 橫坐標(畫線區域內的相對坐標)
						divPoint.x = $(id).offset().left - $(div2First).offset().left;
						// 縱坐標(畫線區域內的相對坐標)
						divPoint.y = parseInt($(svg).attr("height"));
					}
				} else {
					// 縱向排列的坐標算法
					if (isS) {
						// 橫坐標(畫線區域內的相對坐標)
						divPoint.x = 0
						// 縱坐標(畫線區域內的相對坐標)
						divPoint.y = $(id).offset().top - $(div1First).offset().top;
					} else {
						// 橫坐標(畫線區域內的相對坐標)
						divPoint.x = parseInt($(svg).attr("width"));
						// 縱坐標(畫線區域內的相對坐標)
						divPoint.y = $(id).offset().top - $(div1Last).offset().top;
					}
				}
				_arrCurrencyDivPoints.push(divPoint);
			});
		},

		// 開始隊列DIV點擊事件
		btnDivStartClick = function (div) {
			// 設定當前DIV被選中狀態
			_chkCurrencyBoxClick($(div).find(':checkbox'));

			var selectedDivId = "#" + $(div).attr("id");
			// DIV選擇狀態還原
			var drawedLine = _reducingSelectState(selectedDivId);

			if ($("#hidCurrencySelectedS").val() == "") {
				// 記錄當前被選擇的DIV
				$("#hidCurrencySelectedS").val(selectedDivId);
			} else {
				// 開始隊列中同時被選擇兩項的情況
				if ($("#hidCurrencySelectedS").val() != selectedDivId) {
					// 將前一次的選擇狀態取消
					_chkCurrencyBoxClick($($("#hidCurrencySelectedS").val()).find(':checkbox'));
					// 記錄當前被選擇的DIV
					$("#hidCurrencySelectedS").val(selectedDivId);
				} else {
					// 將當前結束點選擇狀態取消
					_chkCurrencyBoxClick($($("#hidCurrencySelectedE").val()).find(':checkbox'));
					// 取消當前選擇狀態
					$("#hidCurrencySelectedS").val("");
					$("#hidCurrencySelectedE").val("");
				}
			}
			// 畫線處理
			_currencyDrawLineToSvg(drawedLine);
		},

		// DIV選擇狀態還原
		_reducingSelectState = function (selectedDivId) {
			var line = null;
			var lineName = "";
			$.each(_arrCurrencyDrawLines, function (index, value) {
				if (value.startPoint.name == selectedDivId || value.endPoint.name == selectedDivId) {
					// 取得當前線型
					line = value;
					lineName = value.name;
					// DIV選擇狀態還原
					$("#hidCurrencySelectedS").val(value.startPoint.name);
					$("#hidCurrencySelectedE").val(value.endPoint.name);
				}
			});
			if (line != null) {
				// 從已畫線隊列中移除
				_arrCurrencyDrawLines.remove(line);
			}
			return lineName;
		},

		// 結束隊列DIV的點擊事件
		btnDivEndClick = function (div) {
			// 設定當前DIV被選中狀態
			_chkCurrencyBoxClick($(div).find(':checkbox'));

			var selectedDivId = "#" + $(div).attr("id");
			// DIV選擇狀態還原
			var drawedLine = _reducingSelectState(selectedDivId);

			if ($("#hidCurrencySelectedE").val() == "") {
				// 記錄當前被選擇的DIV
				$("#hidCurrencySelectedE").val(selectedDivId);
			} else {
				// 結束隊列中同時被選擇兩項的情況
				if ($("#hidCurrencySelectedE").val() != selectedDivId) {
					// 將前一次的選擇狀態取消
					_chkCurrencyBoxClick($($("#hidCurrencySelectedE").val()).find(':checkbox'));
					// 記錄當前被選擇的DIV
					$("#hidCurrencySelectedE").val(selectedDivId);
				} else {
					// 將當前起始點選擇狀態取消
					_chkCurrencyBoxClick($($("#hidCurrencySelectedS").val()).find(':checkbox'));
					// 取消當前選擇狀態
					$("#hidCurrencySelectedE").val("");
					$("#hidCurrencySelectedS").val("");
				}
			}
			// 畫線處理
			_currencyDrawLineToSvg(drawedLine);
		},

		_currencyDrawLineToSvg = function (drawedLine) {
			// 起始點狀態取得(被選擇的DIV ID)
			var s = $("#hidCurrencySelectedS").val();
			// 結束點狀態取得(被選擇的DIV ID)
			var e = $("#hidCurrencySelectedE").val();

			var x1 = 0, y1 = 0, x2 = 0, y2 = 0;
			// 線型或者已畫線型
			var line = drawedLine;
			if (s != '' && e != '') {
				// 當前畫線狀態對象
				var drawLine = new Object();
				// 遍歷每一個畫線坐標點
				$.each(_arrCurrencyDivPoints, function (index, value) {
					// 找到當前被選擇的起始點坐標
					if (value.name == s) {
						x1 = value.x;
						y1 = value.y;
						// 取得對應的線型對象
						line = "#" + $(s).find('input[type=hidden]').val();

						// 起始點對象
						drawLine.startPoint = value;
						drawLine.name = line;
					}
					// 找到當前被選擇的結束點坐標
					if (value.name == e) {
						x2 = value.x;
						y2 = value.y;

						// 結束點對象
						drawLine.endPoint = value;
					}
				});
				_arrCurrencyDrawLines.push(drawLine);
				// 畫線處理
				$(line).attr({ "x1": x1, "y1": y1, "x2": x2, "y2": y2 });

				// 起始點狀態清除
				$("#hidCurrencySelectedS").val("");
				// 結束點狀態清除
				$("#hidCurrencySelectedE").val("");
			} else {
				// 畫線清除
				$(line).attr({ "x1": x1, "y1": y1, "x2": x2, "y2": y2 });
				// 找出落單的選擇快
				_findCurrencySingleDiv();
			}
		},

		// 找出落單的選擇快
		_findCurrencySingleDiv = function () {
			// 遍歷每一個畫線坐標點
			$.each(_arrCurrencyDivPoints, function (index, div) {
				// 是否處於選擇狀態
				if ($(div.name).find(':checkbox').is(':checked')) {
					var isSingle = true;
					$.each(_arrCurrencyDrawLines, function (index, value) {
						var selectedDivId = div.name;
						// 判斷是否已經畫線
						if (value.startPoint.name == selectedDivId || value.endPoint.name == selectedDivId) {
							isSingle = false;
						}
					});
					// 如果是落單對象
					if (isSingle) {
						// 判斷是否為開始隊列還是結束隊列
						(div.name.substring(div.name.length - 1) == 'S') ? $("#hidCurrencySelectedS").val(div.name) : $("#hidCurrencySelectedE").val(div.name);
						return false;
					}
				}
			});
		},

		// 設定當前DIV被選中狀態
		_chkCurrencyBoxClick = function (chk) {
			var parent = $(chk).parent();
			if ($(chk).is(':checked')) {
				$(chk).prop('checked', false);
				parent.css('background-color', '#9fcfff');
			} else {
				$(chk).prop('checked', true);
				parent.css('background-color', '#00ff90');
			}
		},

		// 订正(認識價格题)
		makeCorrections = function () {
			var fault = 0;
			// 答题验证
			if (!_currencyLinkageCorrecting()) {
				// 答题错误时,错误件数加一
				fault++;
			}
			return fault;
		},

		// 認識價格交卷
		theirPapers = function () {
			// 答题验证
			if (!_currencyLinkageCorrecting()) {
				// 答题错误时,错误件数加一
				__isFault++;
			} else {
				__isRight++;
			}
		};

	return {
		printSetting: printSetting,
		printAfterSetting: printAfterSetting,
		ready: ready,
		makeCorrections: makeCorrections,
		theirPapers: theirPapers,
		btnDivStartClick: btnDivStartClick,
		btnDivEndClick: btnDivEndClick
	};
}());
