var MathSheets = MathSheets || {};

MathSheets.CombinatorialEquation = MathSheets.CombinatorialEquation || (function () {
    // 运算符点击切换(>、<、=)
    _imgCeClick = function (element) {
        if ($(element).attr("title") == 'help') {
            $(element).attr("title", 'plus');
            $(element).attr("src", '../Content/image/Plus.png');
            return;
        } else if ($(element).attr("title") == 'plus') {
            $(element).attr("title", 'subtraction');
            $(element).attr("src", '../Content/image/Subtraction.png');
            return;
        } else if ($(element).attr("title") == 'subtraction') {
            $(element).attr("title", 'multiple');
            $(element).attr("src", '../Content/image/Multiple.png');
            return;
        } else if ($(element).attr("title") == 'multiple') {
            $(element).attr("title", 'division');
            $(element).attr("src", '../Content/image/Division.png');
            return;
        } else if ($(element).attr("title") == 'division') {
            $(element).attr("title", 'plus');
            $(element).attr("src", '../Content/image/Plus.png');
            return;
        }
    },

        _toOperationString = function (title) {
            switch (title) {
                case "plus":
                    return "+";
                case "subtraction":
                    return "−";
                case "multiple":
                    return "×";
                case "division":
                    return "÷";
            }
            return "";
        },

        // 答题验证(正确:true  错误:false)
        _combinatorialCorrecting = function (index, element) {
            // 答題結果集合
            var answers = ($(element).val() || "").split(';');

            for (i = 0; i <= 3; i++) {
                var l = $('#inputCe' + index + 'L' + i).val();
                var s = _toOperationString($('#imgCe' + index + 'S' + i).attr("title"));
                var r = $('#inputCe' + index + 'R' + i).val();
                var a = $('#inputCe' + index + 'A' + i).val();
                var answer = l + s + r + "=" + a;
                //console.log(answer);
                answers = $.grep(answers, function (value) {
                    var decode = $.base64.atob(value, true);
                    //console.log(decode);
                    return (decode != answer);
                });
            }

            // 验证输入值是否与答案一致
            if (answers.length == 0) {
                $("input[id*='inputCe" + index + "']").each(function () {
                    // 动错题集中移除当前项目
                    removeInputElementArray({ position: "mathSheetCombinatorialEquation", id: $(this).attr('id') });
                    $(this).attr("disabled", "disabled");
                });
                // 对错图片显示和隐藏
                $('#imgOKCombinatorial' + index).show();
                $('#imgNoCombinatorial' + index).hide();
                // 移除圖片抖動特效
                $('#imgNoCombinatorial' + index).removeClass("shake shake-slow");
                // 正确:true
                return true;
            } else {
                // 对错图片显示和隐藏
                $('#imgOKCombinatorial' + index).hide();
                $('#imgNoCombinatorial' + index).show();
                $('#imgNoCombinatorial' + index).animate({
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

        // 打印設置
        printSetting = function () {
            // 頁面輸入項目
            $("input[id*='inputCe']").each(function (index, element) {
                $(element).addClass('input-print');
                $(element).removeAttr('placeholder');
                $(element).removeAttr("disabled");
            });
            // 頁面按鍵項目
            $("input[id*='hiddenCe']").each(function (parentIndex, parentElement) {
                $("img[id*='imgCe" + parentIndex + "S']").each(function (index, element) {
                    $(element).replaceWith("<button id=\"btnCe" + parentIndex + "S" + index + "\" type=\"button\" class=\"btn btn-default btn-circle button-addBorder\"></button>");
                });
            });
        },

        // 打印后頁面設定
        printAfterSetting = function () {
            // 頁面輸入項目
            $("input[id*='inputCe']").each(function (index, element) {
                $(element).removeClass('input-print');
                $(element).attr('placeholder', '??');
                $(element).attr("disabled", "disabled");
            });

            // 頁面按鍵項目
            $("input[id*='hiddenCe']").each(function (parentIndex, element) {
                $("button[id*='btnCe" + parentIndex + "S']").each(function (index, element) {
                    $(element).replaceWith("<img src=\"../Content/image/help.png\" id=\"imgCe" + parentIndex + "S" + index + "\" style=\"width: 30px; height: 30px; \" title=\"help\" />");
                });
            });
        },

        // 设定页面所有输入域为可用状态(算式組合)
        ready = function () {
            $("input[id*='inputCe']").each(function (index, element) {
                // 收集所有可輸入項目ID
                __allInputElementArray.push({ position: "mathSheetCombinatorialEquation", id: $(element).attr("id") });
                $(element).removeAttr("disabled");
            });

            $("img[id*='imgCe']").each(function (index, element) {
                $(element).click(function () { _imgCeClick(element); });
            });
        },

        // 订正(算式組合)
        makeCorrections = function () {
            var fault = 0;
            $("input[id*='hiddenCe']").each(function (index, element) {
                // 答题验证
                if (!_combinatorialCorrecting(index, element)) {
                    // 答题错误时,错误件数加一
                    fault++;
                }
            });
            return fault;
        },

        // 算式組合交卷
        theirPapers = function () {
            $("input[id*='hiddenCe']").each(function (index, element) {
                // 答题验证
                if (!_combinatorialCorrecting(index, element)) {
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