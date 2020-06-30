var MathSheets = MathSheets || {};

MathSheets.EqualityLinkage = MathSheets.EqualityLinkage || (function () {
    // DIV畫線點坐標列表
    var _arrDivPoints = new Array();
    // DIV連線狀態列表
    var _arrDrawLines = new Array();

    // 打印設置
    printSetting = function () {
        // 遍歷所有DIV并設置打印樣式
        $("div[class='divDrawLine']").each(function (index, element) {
            $(element).addClass('divDrawLine-print');
        });
    },

        // 打印后頁面設定
        printAfterSetting = function () {
            // 遍歷所有checkbox并設置為隱藏
            $("input[type='checkbox']").each(function (index, element) {
                $(element).hide();
            });
            // 遍歷所有DIV并還原打印前樣式
            $("div[class*='divDrawLine']").each(function (index, element) {
                $(element).removeClass('divDrawLine-print')
            });
        },

        // 答题验证(正确:true  错误:false)
        _equalityLinkageCorrecting = function () {
            var isRight = false;
            // 答案序列(div01S#div02E;div02S#div03E;div03S#div01E;.....)
            var answers = ($('#hidElAnswer').val() || '').split(';');
            $.each(answers, function (i, answer) {
                isRight = false;
                // 左邊算式答案
                var left = "#" + (answer || '').split('#')[0];
                // 右邊算式答案
                var right = "#" + (answer || '').split('#')[1];
                // 答題情況
                $.each(_arrDrawLines, function (j, value) {
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
				// 动错题集中移除当前项目
				removeInputElementArray({ position: "mathSheetEqualityLinkage", id: null });
                // 对错图片显示和隐藏
                $('#imgOKEqualityLinkage').show();
				$('#imgNoEqualityLinkage').hide();
				// 移除圖片抖動特效
				$('#imgNoEqualityLinkage').removeClass("shake shake-slow");
                // 移除DIV點擊事件(起始點和結束點)
                $.each(_arrDivPoints, function (index, div) {
                    $(div.name).unbind('click');
                });
                // 正确:true
                return true;
            } else {
                // 对错图片显示和隐藏
                $('#imgOKEqualityLinkage').hide();
				$('#imgNoEqualityLinkage').show();
				$('#imgNoEqualityLinkage').animate({
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

        // 设定页面所有输入域为可用状态(算式連一連)
        ready = function () {
            // 畫線區域初期化
            _svaAreaInit("#hidInitSettings");
            // 畫線坐標點初期化
            _relativeLocationPointInit("#hidInitSettings");
            // DIV事件註冊(起始點和結束點)
            $.each(_arrDivPoints, function (index, div) {
                // 遍歷每一個畫線坐標點
                (div.name.substring(div.name.length - 1) == 'S')
                    ? $(div.name).click(function () { _btnDivStartClick(this); })
                    : $(div.name).click(function () { _btnDivEndClick(this); });
            });
        },

        // 畫線區域初期化
        _svaAreaInit = function (hidInitSettings) {
            // 畫線區域的參數取得(注意: 所有控件ID均帶有#)
            var settings = ($(hidInitSettings).val() || "").split(',');
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
        _relativeLocationPointInit = function (hidInitSettings) {
            // 畫線區域的參數取得
            var settings = ($(hidInitSettings).val() || "").split(',');

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
            $("div[class='divDrawLine']").each(function (index, element) {
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
                _arrDivPoints.push(divPoint);
            });
        },

        // 開始隊列DIV點擊事件
        _btnDivStartClick = function (div) {
            // 設定當前DIV被選中狀態
            _chkBoxClick($(div).find(':checkbox'));

            var selectedDivId = "#" + $(div).attr("id");
            // DIV選擇狀態還原
            var drawedLine = _reducingSelectState(selectedDivId);

            if ($("#hidSelectedS").val() == "") {
                // 記錄當前被選擇的DIV
                $("#hidSelectedS").val(selectedDivId);
            } else {
                // 開始隊列中同時被選擇兩項的情況
                if ($("#hidSelectedS").val() != selectedDivId) {
                    // 將前一次的選擇狀態取消
                    _chkBoxClick($($("#hidSelectedS").val()).find(':checkbox'));
                    // 記錄當前被選擇的DIV
                    $("#hidSelectedS").val(selectedDivId);
                } else {
                    // 將當前結束點選擇狀態取消
                    _chkBoxClick($($("#hidSelectedE").val()).find(':checkbox'));
                    // 取消當前選擇狀態
                    $("#hidSelectedS").val("");
                    $("#hidSelectedE").val("");
                }
            }
            // 畫線處理
            _drawLineToSvg(drawedLine);
        },

        // DIV選擇狀態還原
        _reducingSelectState = function (selectedDivId) {
            var line = null;
            var lineName = "";
            $.each(_arrDrawLines, function (index, value) {
                if (value.startPoint.name == selectedDivId || value.endPoint.name == selectedDivId) {
                    // 取得當前線型
                    line = value;
                    lineName = value.name;
                    // DIV選擇狀態還原
                    $("#hidSelectedS").val(value.startPoint.name);
                    $("#hidSelectedE").val(value.endPoint.name);
                }
            });
            if (line != null) {
                // 從已畫線隊列中移除
                _arrDrawLines.remove(line);
            }
            return lineName;
        },

        // 結束隊列DIV的點擊事件
        _btnDivEndClick = function (div) {
            // 設定當前DIV被選中狀態
            _chkBoxClick($(div).find(':checkbox'));

            var selectedDivId = "#" + $(div).attr("id");
            // DIV選擇狀態還原
            var drawedLine = _reducingSelectState(selectedDivId);

            if ($("#hidSelectedE").val() == "") {
                // 記錄當前被選擇的DIV
                $("#hidSelectedE").val(selectedDivId);
            } else {
                // 結束隊列中同時被選擇兩項的情況
                if ($("#hidSelectedE").val() != selectedDivId) {
                    // 將前一次的選擇狀態取消
                    _chkBoxClick($($("#hidSelectedE").val()).find(':checkbox'));
                    // 記錄當前被選擇的DIV
                    $("#hidSelectedE").val(selectedDivId);
                } else {
                    // 將當前起始點選擇狀態取消
                    _chkBoxClick($($("#hidSelectedS").val()).find(':checkbox'));
                    // 取消當前選擇狀態
                    $("#hidSelectedE").val("");
                    $("#hidSelectedS").val("");
                }
            }
            // 畫線處理
            _drawLineToSvg(drawedLine);
        },

        _drawLineToSvg = function (drawedLine) {
            // 起始點狀態取得(被選擇的DIV ID)
            var s = $("#hidSelectedS").val();
            // 結束點狀態取得(被選擇的DIV ID)
            var e = $("#hidSelectedE").val();

            var x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            // 線型或者已畫線型
            var line = drawedLine;
            if (s != '' && e != '') {
                // 當前畫線狀態對象
                var drawLine = new Object();
                // 遍歷每一個畫線坐標點
                $.each(_arrDivPoints, function (index, value) {
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
                _arrDrawLines.push(drawLine);
                // 畫線處理
                $(line).attr({ "x1": x1, "y1": y1, "x2": x2, "y2": y2 });

                // 起始點狀態清除
                $("#hidSelectedS").val("");
                // 結束點狀態清除
                $("#hidSelectedE").val("");
            } else {
                // 畫線清除
                $(line).attr({ "x1": x1, "y1": y1, "x2": x2, "y2": y2 });
                // 找出落單的選擇快
                _findSingleDiv();
            }
        },

        // 找出落單的選擇快
        _findSingleDiv = function () {
            // 遍歷每一個畫線坐標點
            $.each(_arrDivPoints, function (index, div) {
                // 是否處於選擇狀態
                if ($(div.name).find(':checkbox').is(':checked')) {
                    var isSingle = true;
                    $.each(_arrDrawLines, function (index, value) {
                        var selectedDivId = div.name;
                        // 判斷是否已經畫線
                        if (value.startPoint.name == selectedDivId || value.endPoint.name == selectedDivId) {
                            isSingle = false;
                        }
                    });
                    // 如果是落單對象
                    if (isSingle) {
                        // 判斷是否為開始隊列還是結束隊列
                        (div.name.substring(div.name.length - 1) == 'S') ? $("#hidSelectedS").val(div.name) : $("#hidSelectedE").val(div.name);
                        return false;
                    }
                }
            });
        },

        // 設定當前DIV被選中狀態
        _chkBoxClick = function (chk) {
            var parent = $(chk).parent();
            if ($(chk).is(':checked')) {
                $(chk).prop('checked', false);
				parent.css('background-color', 'navajowhite');
            } else {
                $(chk).prop('checked', true);
                parent.css('background-color', '#00ff90');
            }
        },

        // 订正(算式連一連题)
        makeCorrections = function () {
            var fault = 0;
            // 答题验证
            if (!_equalityLinkageCorrecting()) {
                // 答题错误时,错误件数加一
                fault++;
            }
            return fault;
        },

        // 算式連一連交卷
        theirPapers = function () {
            // 答题验证
            if (!_equalityLinkageCorrecting()) {
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
        theirPapers: theirPapers
    };
}());