
var MathSheets = MathSheets || {};

MathSheets.SchoolClock = MathSheets.SchoolClock || (function () {

    // 打印設置
    printSetting = function () {

    },

        // 打印后頁面設定
        printAfterSetting = function () {
            
        },

        // 答题验证(正确:true  错误:false)
        _schoolClockCorrecting = function (index, element) {
            // 验证输入值是否与答案一致
            if ($(element).val() == $('#hiddenAc' + index).val()
                || (parseInt($('#hiddenAc' + index).val()) == -999 && $(element).val() != '')) {
                // 对错图片显示和隐藏
                $('#imgOKSchoolClock' + index).show();
                $('#imgNoSchoolClock' + index).hide();
                $(element).attr("disabled", "disabled");
                // 正确:true
                return true;
            } else {
                // 对错图片显示和隐藏
                $('#imgOKSchoolClock' + index).hide();
                $('#imgNoSchoolClock' + index).show();
                // 错误:false
                return false;
            }
        },

        // 设定页面所有输入域为可用状态(四则运算)
        ready = function () {
            var aryClocksAnswer = new Array();
            aryClocksAnswer = ($('#hidClocksAnswer').val() || '').split(';');


            $("svg[class='clock']").each(function (index, element) {
                var clock = Snap('#' + $(element).attr('id'));
                var hours = clock.rect(79, 35, 3, 55).attr({ fill: "#282828", transform: "r" + 10 * 30 + "," + 80 + "," + 80 });
                var minutes = clock.rect(79, 20, 3, 70).attr({ fill: "#535353", transform: "r" + 10 * 6 + "," + 80 + "," + 80 });
                var seconds = clock.rect(80, 10, 1, 80).attr({ fill: "#ff6400" });
                var middle = clock.circle(81, 80, 3).attr({ fill: "#535353" });

                updateTime(clock, hours, minutes, seconds, aryClocksAnswer[index]);
            });
        },

        // 订正(時鐘學習板)
        makeCorrections = function () {
            var fault = 0;
            $("input[id*='inputAc']").each(function (index, element) {
                // 答题验证
                if (!_schoolClockCorrecting(index, element)) {
                    // 答题错误时,错误件数加一
                    fault++;
                }
            });
            return fault;
        },

        // 四则运算交卷
        theirPapers = function () {
            $("input[id*='inputAc']").each(function (index, element) {
                // 答题验证
                if (!_schoolClockCorrecting(index, element)) {
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
        theirPapers: theirPapers
    };
}());
