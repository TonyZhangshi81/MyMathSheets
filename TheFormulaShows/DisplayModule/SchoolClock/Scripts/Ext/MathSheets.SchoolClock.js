var __aryClocksAnswer = new Array();

var MathSheets = MathSheets || {};

MathSheets.SchoolClock = MathSheets.SchoolClock || (function () {
    // 打印設置
    printSetting = function () {
        $("input[id*='inputClock']").each(function (index, element) {
            $(element).addClass('input-print');
            $(element).removeAttr('placeholder');
            $(element).removeAttr("disabled");
        });
    },

        // 打印后頁面設定
        printAfterSetting = function () {
            $("input[id*='inputClock']").each(function (index, element) {
                if ($(element).hasClass('hours')) {
                    $(element).attr('placeholder', '時');
                } else if ($(element).hasClass('minutes')) {
                    $(element).attr('placeholder', '分');
                } else {
                    $(element).attr('placeholder', '秒');
                }
                $(element).removeClass('input-print');
                $(element).attr("disabled", "disabled");
            });
        },

        // 答题验证(正确:true  错误:false)
        _schoolClockCorrecting = function (index, answer) {
            // 答案時分秒
            var hms = ($.base64.atob(answer, true) || '').split(':');
            // 時
            var hours = parseInt($("#inputClockH" + index).val());
            // 分
            var minutes = parseInt($("#inputClockM" + index).val());
            // 秒
            var seconds = parseInt($("#inputClockS" + index).val());

            // 验证输入值是否与答案一致
            if (parseInt(hms[0]) == hours && parseInt(hms[1]) == minutes && parseInt(hms[2]) == seconds) {
                // 动错题集中移除当前项目(時分秒)
                removeInputElementArray({ position: "mathSheetSchoolClock", id: ("inputClockH" + index) });
                removeInputElementArray({ position: "mathSheetSchoolClock", id: ("inputClockM" + index) });
                removeInputElementArray({ position: "mathSheetSchoolClock", id: ("inputClockS" + index) });
                // 对错图片显示和隐藏
                $('#imgOKSchoolClock' + index).show();
                $('#imgNoSchoolClock' + index).hide();
                // 移除圖片抖動特效
                $('#imgNoSchoolClock' + index).removeClass("shake shake-slow");

                $("#inputClockH" + index).attr("disabled", "disabled");
                $("#inputClockM" + index).attr("disabled", "disabled");
                $("#inputClockS" + index).attr("disabled", "disabled");
                // 正确:true
                return true;
            } else {
                // 对错图片显示和隐藏
                $('#imgOKSchoolClock' + index).hide();
                $('#imgNoSchoolClock' + index).show();
                $('#imgNoSchoolClock' + index).animate({
                    width: "40px",
                    height: "40px",
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

        // 设定页面所有输入域为可用状态(時鐘學習板)
        ready = function () {
            __aryClocksAnswer = ($('#hidClocksAnswer').val() || '').split(';');

            $("svg[class='clock']").each(function (index, element) {
                var clock = Snap('#' + $(element).attr('id'));
                var hours = clock.rect(79, 35, 3, 55).attr({ fill: "#282828", transform: "r" + 10 * 30 + "," + 80 + "," + 80 });
                var minutes = clock.rect(79, 20, 3, 70).attr({ fill: "#535353", transform: "r" + 10 * 6 + "," + 80 + "," + 80 });
                var seconds = clock.rect(80, 10, 1, 80).attr({ fill: "#ff6400" });
                var middle = clock.circle(81, 80, 3).attr({ fill: "#535353" });

                _updateTime(hours, minutes, seconds, $.base64.atob(__aryClocksAnswer[index], true));
            });

            $("input[id*='inputClock']").each(function (index, element) {
                // 收集所有可輸入項目ID
                __allInputElementArray.push({ position: "mathSheetSchoolClock", id: $(element).attr("id") });
                $(element).removeAttr("disabled");
            });
        },

        _updateTime = function (clockHours, clockMinutes, clockSeconds, dateTime) {
            var currentTime, hour, minute, second;

            if (dateTime == '') {
                // 取系統時間
                currentTime = new Date();
                second = currentTime.getSeconds();
                minute = currentTime.getMinutes();
                hour = currentTime.getHours();
            } else {
                // 指定時間
                var ary = (dateTime || '').split(':');
                second = parseInt(ary[2]);
                minute = parseInt(ary[1]);
                hour = parseInt(ary[0]);
            }

            // 顯示12小時制
            if (hour > 12) {
                hour = hour - 12;
            }

            // 6度表示一分鐘
            var minangle = minute * 6;
            // 30度表示一小時
            var hourangle = (hour + (minute / 60.0)) * 30;
            // 6度表示一秒鐘
            var secrangel = second * 6;

            clockHours.animate({ transform: "r" + hourangle + "," + 80 + "," + 80 }, 200, mina.elastic);
            clockMinutes.animate({ transform: "r" + minangle + "," + 80 + "," + 80 }, 200, mina.elastic);
            if (clockSeconds) {
                clockSeconds.animate({ transform: "r" + secrangel + "," + 80 + "," + 80 }, 500, mina.elastic);
            }
        },

        // 订正(時鐘學習板)
        makeCorrections = function () {
            var fault = 0;
            $.each(__aryClocksAnswer, function (index, answer) {
                // 答题验证
                if (!_schoolClockCorrecting(index, answer)) {
                    // 答题错误时,错误件数加一
                    fault++;
                }
            });
            return fault;
        },

        // 當光標落在文本輸入框中的時候發生的事件
        inputOnFocus = function (e) {
            var value = $(e).val();
            // 如果當前輸入框的內容是空
            if (value == "") {
                return;
            }
            // 刪除數字前的"0"
            $(e).val(parseInt(value));
        },

        // 當光標失去焦點的時候發生的事件
        inputOnBlur = function (e) {
            var value = $(e).val();
            // 如果當前輸入框的內容是空
            if (value == "") {
                return;
            }
            // 在數字前填充"0"
            $(e).val(value.PadLeft(2, '0'));
        },

        // 時鐘學習板交卷
        theirPapers = function () {
            $.each(__aryClocksAnswer, function (index, answer) {
                // 答题验证
                if (!_schoolClockCorrecting(index, answer)) {
                    // 答题错误时,错误件数加一
                    __isFault++;
                } else {
                    __isRight++;
                }
            });
        };

    return {
        printSetting: printSetting,
        printAfterSetting: printAfterSetting,
        ready: ready,
        makeCorrections: makeCorrections,
        inputOnFocus: inputOnFocus,
        inputOnBlur: inputOnBlur,
        theirPapers: theirPapers
    };
}());