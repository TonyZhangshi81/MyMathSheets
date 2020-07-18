// 座位號數組
var __fruitsArray = new Array();
// 放置範圍上下限值(Left邊距上下限)
var __containerLeftUp = 0;
var __containerLeftDown = 0;
// 放置範圍上下限值(Top邊距上下限)
var __containerTopUp = 0;
var __containerTopDown = 0;
// 放置範圍上下限值的控件Id
var __fruitsArrayHiddenControlId = '';

var MathSheets = MathSheets || {};
MathSheets.FruitsLinkage = MathSheets.FruitsLinkage || (function () {
    // 座位號數組化
    _fruitsArrayInit = function () {
        // 座位號設置場所(eg:3,2,1,4,5,0,6 => [3,2,1,4,5,0,6])
        var list = ($('#' + __fruitsArrayHiddenControlId).val() || "").split(',');
        $.each(list, function (index, value) {
            __fruitsArray.push(value);
        });
    },

        // 拖動對象的合理坐標範圍初期化設置
        _upperAndLowerLimitInit = function (divFruitDragPrefix, divContainerPrefix, divDragWidth, divDragHeight) {
            // 遍歷所有容器座位編號并製作範圍上下限值
            $.each(__fruitsArray, function (index, value) {
                // 容器對象
                var element = $("div[id*='" + divContainerPrefix + value + "']");
                // 容器對象left上下限
                var leftup = $(element).position().left;
                var leftdown = leftup + ($(element).width() - divDragWidth);
                // 容器對象top上下限
                var topup = $(element).position().top;
                var topdown = $(element).height() + topup - divDragHeight;
                // 將上下限值放於divFruitDragPrefixXXInput控件中(eg:300,330,20,50,3[真確座位號->用於盤錯提示時能夠找到正確的座位號])
                $("#" + divFruitDragPrefix + index + "Input").val(leftup + "," + leftdown + "," + topup + "," + topdown + "," + value);

                // 拖動有效
                $("#" + divFruitDragPrefix + index).draggable();
            });
        },

        // 答题验证(正确:true  错误:false)
        _fruitsLinkageCorrecting = function (index, element) {
            var result = $("#" + $(element).attr('id') + "Result").val();
            var seat = parseInt((($("#" + $(element).attr('id') + "Input").val() || "").split(','))[4]);
            // 验证输入值是否与答案一致
            if (result == 'OK') {
                // 对错图片显示和隐藏
                $('#imgOKFruitsLinkage' + seat).show();
                $('#imgNoFruitsLinkage' + seat).hide();
                // 移除圖片抖動特效
                $('#imgNoFruitsLinkage' + seat).removeClass("shake shake-slow");
                //$(element).attr("disabled", "disabled");
                // 正确:true
                return true;
            } else {
                // 对错图片显示和隐藏
                $('#imgOKFruitsLinkage' + seat).hide();
                $('#imgNoFruitsLinkage' + seat).show();
                $('#imgNoFruitsLinkage' + seat).animate({
                    width: "20px",
                    height: "20px",
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

        // 设定页面所有输入域为可用状态(水果連連看)
        ready = function (divFruitDragPrefix, divContainerPrefix) {
            // 座位號數組化
            _fruitsArrayInit();

            // 可拖動對象的大小信息
            var divDrag = $('#' + divFruitDragPrefix + "0");
            var divDragWidth = $(divDrag).width();
            var divDragHeight = $(divDrag).height();

            // 拖動對象的合理坐標範圍初期化設置
            _upperAndLowerLimitInit(divFruitDragPrefix, divContainerPrefix, divDragWidth, divDragHeight);
        },

        // 當控件被拖動時觸發的事件
        onDrag = function (e) {
            // 當前控件對象的配置參數
            var data = e.data;
            if (data.left < 0) { data.left = 0 }
            if (data.top < 0) { data.top = 0 }
            if (data.left + $(data.target).outerWidth() > $(data.parent).width()) {
                data.left = $(data.parent).width() - $(data.target).outerWidth();
            }
            if (data.top + $(data.target).outerHeight() > $(data.parent).height()) {
                data.top = $(data.parent).height() - $(data.target).outerHeight();
            }
        },

        // 當控件拖動停止時觸發的事件
        onStopDrag = function (e) {
            // 當前控件對象的配置參數
            var data = e.data;
            // 放置範圍上下限值取得
            var array = ($("#" + $(this).attr('id') + "Input").val() || "").split(',');
            // 放置範圍上下限值(Left邊距上下限)
            __containerLeftUp = parseInt(array[0]);
            __containerLeftDown = parseInt(array[1]);
            // 放置範圍上下限值(Top邊距上下限)
            __containerTopUp = parseInt(array[2]);
            __containerTopDown = parseInt(array[3]);
            // 如果拖放對象的相對位置在容器放置範圍上下限內
            if (data.left >= __containerLeftUp && data.left <= __containerLeftDown && data.top >= __containerTopUp && data.top <= __containerTopDown) {
                $("#" + $(this).attr('id') + "Result").val('OK');
            } else {
                $("#" + $(this).attr('id') + "Result").val('ERROR');
            }
        },

        // 订正(水果連連看)
        makeCorrections = function () {
            var fault = 0;
            $("div[id*='divFruitDrag']").each(function (index, element) {
                // 答题验证
                if (!_fruitsLinkageCorrecting(index, element)) {
                    // 答题错误时,错误件数加一
                    fault++;
                }
            });
            // 错题集設定
            _setFaultInputElementArray(fault);
            return fault;
        },

        // 错题集設定
        _setFaultInputElementArray = function (fault) {
            // 有錯題的情況
            if (fault != 0) {
                // 错题集中移除当前项目
                removeInputElementArray({ position: "mathSheetFruitsLinkage", id: null });
            }
        },

        // 水果連連看交卷
        theirPapers = function () {
            var fault = 0;
            $("div[id*='divFruitDrag']").each(function (index, element) {
                // 答题验证
                if (!_fruitsLinkageCorrecting(index, element)) {
                    // 答题错误时,错误件数加一
                    __isFault++;
                    fault++;
                } else {
                    __isRight++;
                }
            });
            // 错题集設定
            _setFaultInputElementArray(fault);
        };

    return {
        onDrag: onDrag,
        ready: ready,
        onStopDrag: onStopDrag,
        makeCorrections: makeCorrections,
        theirPapers: theirPapers
    };
}());