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

        // 答题验证(正确:true  错误:false)
        _RecursionEquationCorrecting = function (pIndex, answerElement) {
            var inputAry = new Array();
            // 用於結果驗證（解密後分割）
            var answer = ($.base64.atob($(answerElement).val(), true) || '');

            var isRight = true;
            $.each($("input[id *= 'inputRe" + pIndex + "']"), function (index, element) {
                // 驗證遞等式計算結果
                var result = eval($(element).val());
                if (result != answer) {
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

            // 為每一個折疊面板註冊事件
            $("div[id*='divAccordion']").each(function (index, element) {
                $(element).accordion({
                    // 展開面板時替換為編輯圖標
                    onSelect: function (title, index) {
                        var pp = $(element).accordion('getSelected');
                        $(pp).prev().children("div").eq(1).attr("class", "panel-icon icon-edit");
                    },
                    // 閉合面板時替換為初始圖標
                    onUnselect: function (title, index) {
                        var pp = $(element).accordion('getPanel', index);
                        $(pp).prev().children("div").eq(1).attr("class", "panel-icon icon-tip");
                    }
                });
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
        theirPapers: theirPapers
    };
}());