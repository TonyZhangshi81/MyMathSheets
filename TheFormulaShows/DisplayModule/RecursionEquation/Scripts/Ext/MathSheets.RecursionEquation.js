var MathSheets = MathSheets || {};

MathSheets.RecursionEquation = MathSheets.RecursionEquation || (function () {
    // 打印設置
    printSetting = function () {
        $("div[id*='divAccordion']").each(function (index, element) {
            $(element).accordion({
                animate: false,
                multiple: true
            });

            $(element).accordion('select', 0);
            $(element).accordion('select', 1);
            $(element).accordion('select', 2);
        });

        $("input[id*='inputRe']").each(function (index, element) {
            $(element).addClass('input-print');
            $(element).removeAttr('placeholder');
            $(element).removeAttr("disabled");
        });
    },

        // 打印后頁面設定
        printAfterSetting = function () {
            $("div[id*='divAccordion']").each(function (index, element) {
                $(element).accordion('select', 0);
                $(element).accordion('unselect', 1);
                $(element).accordion('unselect', 2);

                $(element).accordion({
                    animate: true,
                    multiple: false
                })
            });

            $("input[id*='inputRe']").each(function (index, element) {
                $(element).removeClass('input-print');
                $(element).attr('placeholder', '??');
                $(element).attr("disabled", "disabled");
            });
        },

        // 計算輸入框的中計算式並返回結果
        calcInputContent = function (element) {
            if ($(element).val() == "") {
                return true;
            }

            var result;
            try {
                result = eval($(element).val());
            } catch (e) {
                console.log(e.name + ":" + e.message);
                // 計算式格式不正確（背景閃爍效果）
                _error($(element), 5);

                return false;
            }
            return true;
        },

        _normal = function (element, times) {
            $(element).css("background-color", "#FFF");
            if (times < 0) {
                return;
            }
            times = times - 1;
            setTimeout(function () { _error(element, times); }, 250);
        },

        _error = function (element, times) {
            $(element).css("background-color", "#F6CECE");
            times = times - 1;
            setTimeout(function () { _normal(element, times); }, 250);
        },

        // 答题验证(正确:true  错误:false)
        _RecursionEquationCorrecting = function (pIndex, answerElement) {
            var inputAry = new Array();
            // 用於結果驗證（解密後分割）
            var answer = (MathSheets.Common.base64Decode($(answerElement).val()) || '');

            var isRight = true;
            var isEmpty = 0;
            $.each($("input[id *= 'inputRe" + pIndex + "']"), function (index, element) {
                // 驗證遞等式計算結果
                var result = calcInputContent($(element));
                if ($(element).val() == "") {
                    // 填空項目為空(有2個或以上的空格未填寫內容,此題必錯)
                    isEmpty++;
                } else if (result == false) {
                    // 輸入內容格式不正確
                    isRight = false;
                } else if (result != answer) {
                    // 遞等式分步結果不正確
                    isRight = false;
                }
                if (isEmpty > 1) {
                    isRight = false;
                }

                // 輸入框集合
                inputAry.push($(element));
            });

            // 验证输入值是否与答案一致
            if (isRight) {
                // 动错题集中移除当前项目
                $.each(inputAry, function (index, inputObj) {
                    inputObj.attr("disabled", "disabled");
                });
                // 对错图片显示和隐藏
                $('#imgOKRecursionEquation' + pIndex).show();
                $('#imgNoRecursionEquation' + pIndex).hide();
                // 移除圖片抖動特效
                $('#imgNoRecursionEquation' + pIndex).removeClass("shake shake-slow");
                // 正确:true
                return true;
            } else {
                // 对错图片显示和隐藏
                $('#imgOKRecursionEquation' + pIndex).hide();
                $('#imgNoRecursionEquation' + pIndex).show();
                $('#imgNoRecursionEquation' + pIndex).animate({
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

        // 设定页面所有输入域为可用状态(巧算)
        ready = function () {
            $("input[id*='inputRe']").each(function (index, element) {
                // 特權輸入框採集
                __privilegeInputElementArray.push({ position: "mathSheetRecursionEquation", id: $(element).attr("id") });

                $(element).removeAttr("disabled");
            });
        },

        // 订正(巧算题)
        makeCorrections = function () {
            var fault = 0;
            $("input[id*='hiddenRe']").each(function (pIndex, answerElement) {
                var index = $(answerElement).attr("id").substring(8);
                // 答题验证
                if (!_RecursionEquationCorrecting(index, answerElement)) {
                    // 答题错误时,错误件数加一
                    fault++;
                }
            });
            return fault;
        },

        // 巧算交卷
        theirPapers = function () {
            $("input[id*='hiddenRe']").each(function (pIndex, answerElement) {
                var index = $(answerElement).attr("id").substring(8);
                // 答题验证
                if (!_RecursionEquationCorrecting(index, answerElement)) {
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
        theirPapers: theirPapers,
        calcInputContent: calcInputContent
    };
}());

$(function () {
    // 為每一個折疊面板註冊事件
    $("div[id*='divAccordion']").each(function (index, element) {
        $(element).accordion({
            heightStyle: "auto",
            height: 130,
            collapsible: true
        });
    });

    /** 測試版本
    var availableTags = ["18", "31", "2", "+", "-", "*", "/"];
    function split(val) {
        return val.split(/[ ]/);
    }
    function extractLast(term) {
        return split(term).pop();
    }

    $("#inputRe1011")
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB && $(this).data("ui-autocomplete").menu.active) {
                event.preventDefault();
            }
        })
        .autocomplete({
            source:
                function (request, response) {
                    response($.ui.autocomplete.filter(availableTags, extractLast(request.term)));
                },
            select:
                function (event, ui) {
                    var terms = split(this.value);
                    // 移除當前輸入值
                    //terms.pop();
                    // 添加備選項
                    terms.push(ui.item.value);
                    // 添加佔位符，在結尾添加空格
                    terms.push("");
                    this.value = terms.join(" ");
                    return false;
                }
        });
     */
});