﻿var MathSheets = MathSheets || {};

MathSheets.CleverCalculation = MathSheets.CleverCalculation || (function () {
    // 打印設置
    printSetting = function () {
        $("input[id*='inputClc']").each(function (index, element) {
            $(element).addClass('input-print');
            $(element).removeAttr('placeholder');
            $(element).removeAttr("disabled");
        });
    },

        // 打印后頁面設定
        printAfterSetting = function () {
            $("input[id*='inputClc']").each(function (index, element) {
                $(element).removeClass('input-print');
                $(element).attr('placeholder', '??');
                $(element).attr("disabled", "disabled");
            });
        },

        // 答题验证(正确:true  错误:false)
        _cleverCalculationCorrecting = function (pIndex, answerElement) {

            var inputAry = new Array();
            // 用於結果驗證（解密後分割）
            var answers = (MathSheets.Common.base64Decode($(answerElement).val(), true) || '').split(';');

            var isRight = true;
            $.each($("input[id *= 'inputClc" + pIndex + "']"), function (index, element) {
                // 輸入框集合
                inputAry.push($(element));
                // 結果驗證（相等的值從結果序列中移除）
                answers.remove(parseInt($(element).val()));
            });

            // 結果序列中存在元素（存在錯誤）
            if (answers.length != 0) {
                isRight = false;
            }

            // 验证输入值是否与答案一致
            if (isRight) {
                // 动错题集中移除当前项目
                $.each(inputAry, function (index, inputObj) {
                    removeInputElementArray({ position: "mathSheetCleverCalculation", id: inputObj.attr("id") });
                    inputObj.attr("disabled", "disabled");
                });

                // 对错图片显示和隐藏
                $('#imgOKCleverCalculation' + pIndex).show();
                $('#imgNoCleverCalculation' + pIndex).hide();
                // 移除圖片抖動特效
                $('#imgNoCleverCalculation' + pIndex).removeClass("shake shake-slow");

                // 正确:true
                return true;
            } else {
                // 对错图片显示和隐藏
                $('#imgOKCleverCalculation' + pIndex).hide();
                $('#imgNoCleverCalculation' + pIndex).show();
                $('#imgNoCleverCalculation' + pIndex).animate({
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
            $("input[id*='inputClc']").each(function (index, element) {
                // 收集所有可輸入項目ID
                __allInputElementArray.push({ position: "mathSheetCleverCalculation", id: $(element).attr("id") });

                $(element).removeAttr("disabled");
            });
        },

        // 订正(巧算题)
        makeCorrections = function () {
            var fault = 0;
            $("input[id*='hiddenClc']").each(function (pIndex, answerElement) {
                var index = pIndex.toString().PadLeft(2, '0');
                // 答题验证
                if (!_cleverCalculationCorrecting(index, answerElement)) {
                    // 答题错误时,错误件数加一
                    fault++;
                }
            });
            return fault;
        },

        // 巧算交卷
        theirPapers = function () {
            $("input[id*='hiddenClc']").each(function (pIndex, answerElement) {
                var index = pIndex.toString().PadLeft(2, '0');
                // 答题验证
                if (!_cleverCalculationCorrecting(index, answerElement)) {
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