
/**
 * @file 頁面虛擬人物會話處理模塊
 * @description 完成簡單的會話處理
 * @author TonyZhangshi <tonyzhangshi@163.com>
 * @version 0.1
 * @copyright Tony Zhangshi 2018
 */
var MathSheets = MathSheets || {};
MathSheets.HelloMrTony = MathSheets.HelloMrTony || (function () {

    /**
     * @description 默認會話資源信息
     * @constant {string} RESOURCES
     * @private
     */
    const RESOURCES = (function () {
        var I0001 = '看看你的错题<br/>快去订正吧',
            I0002 = '有不会做的题目吗?',
            I0003 = '打开书本复习一下吧',
            I0004 = '练习已经开始了<br/>请认真看题哦',
            I0005 = '加油!<br/>我会帮助你的',
            I0006 = '如果打搅你了我可以去喝杯茶',
            I0007 = "I'm Tony.Hello! Eve.",
            I0008 = '有没有做好准备<br/>我们要准备做题咯',
            I0009 = '真厉害全部答对了！<br/>给你一枚奖章...',
            I0010 = '可以玩游戏哦！<br/>我先走了,再见!'
        return {
            I0001: I0001,
            I0002: I0002,
            I0003: I0003,
            I0004: I0004,
            I0005: I0005,
            I0006: I0006,
            I0007: I0007,
            I0008: I0008,
            I0009: I0009,
            I0010: I0010
        };
    }());

    /**
     * @description 等待間隔時間
     * @private
     * @property {int} _offSet
     * @default 5000（5秒）
     */
    var _offSet = 5000;

	/**
	 * @description 虛擬人物顯示開關
     * @private
	 * @property {string} _switch
	 * @default 'on'
	 */
    var _switch = 'on';

	/**
	 * @description 會話序列
     * @private
	 * @property {Array} _dialogueArray
	 */
    var _dialogueArray = new Array();

	/**
	 * @description 定時器執行后ID
     * @private
	 * @property {int} _timeId
	 */
    var _timeId;

	/**
	 * @description 當前會話所在集合的位置
     * @private
	 * @property {int} _dialogueId
     * @default 0
	 */
    var _dialogueId = 0;

	/**
	 * @description 會話是否已經顯示(0沒有顯示，1顯示過，2等待)
     * @private
	 * @property {int} _messageIsShowed
     * @default 0
	 */
    var _messageIsShowed = 0;

	/**
	 * @description 會話是否執行循環播放
     * @private
	 * @property {bool} _isCirculation
     * @default false
	 */
    var _isCirculation = false;

	/**
	 * @description 循環事件執行后的回調函數定義對象
     * @private
	 * @property {object} _callbackFunc
	 */
    var _callbackFunc;

	/**
	 * @description 頁面虛擬人物實例
     * @private
	 * @property {object} $teacher
	 */
    var $teacher;

	/**
	 * @description 頁面虛擬人物的顯示寬度
     * @private
	 * @property {int} _figureWidth
     * @default 0
	 */
    var _figureWidth = 0;

	/**
	 * @description 會話顯示的時間點
     * @private
	 * @property {int} _smarkTime
     * @default 0
	 */
    var _smarkTime = 0;

	/**
	 * @description 會話隱藏的時間點
     * @private
	 * @property {int} _emarkTime
     * @default 0
	 */
    var _emarkTime = 0;

    /**
     * @description 簡單會話處理
     * 1.當前會話保持顯示5秒鐘
     * 2.5秒后隱藏當前會話
     * 3.按照上述處理過程逐個處理會話列表中的內容
     * 4.關閉虛擬人物時停止上述處理但保留會話狀態
     * 5.會話輪詢處理分為循環播放和非循環播放（在調用時設定該播放參數）
     * @private
     */
    this._dialogue = function () {
        // 如果虛擬人物被關閉則暫停會話（不清除之前的回話狀態）
        if (_switch == 'off') {
            // 停止計時
            clearTimeout(_timeId);
            return;
        }

        // 判斷是否循環播放
        if (!_isCirculation) {
            // 如果會話播放完畢則釋放當前會話列表的內容
            if (_dialogueId >= _dialogueArray.length) {
                _dialogueArray.length = 0;
            }
        } else {
            // 會話循環模式開啟
            if (_dialogueId >= _dialogueArray.length) {
                _dialogueId = 0;
            }
        }

        // 當前沒有回話則停止計時器
        if (_dialogueArray.length == 0) {
            // 在停止計時之前執行回調函數（如果有回調函數的情況則執行）
            if (typeof _callbackFunc != 'undefined' && _callbackFunc instanceof Function) {
                // 執行回調函數
                _callbackFunc();
            }
            // 停止計時
            clearTimeout(_timeId);
            return;
        }

        // 判斷是否已經顯示過會話，如果是（0沒有執行顯示）則激活當前會話并執行顯示
        if (_messageIsShowed == 0) {
            $teacher.attr('data-original-title', _dialogueArray[_dialogueId]);
            $teacher.tooltip('show');
            // 設置為已經執行過顯示（避免遞歸時再次執行上述顯示處理）
            _messageIsShowed = 1;
        }

        //console.log('s:' + (new Date().getTime() - _smarkTime));
        // 保持上述顯示處理（延遲時間為默認的5秒鐘），5秒后執行後續處理
        if ((new Date().getTime() - _smarkTime) < _offSet) {
            _timeId = setTimeout(_dialogue, 1000);
            return;
        }

        // 清除開始執行的時間點
        _smarkTime = 0;
        // 判斷是否已經顯示過會話，如果是（1已經執行過顯示）則隱藏當前會話
        if (_messageIsShowed == 1) {
            // 用於隱藏處理的時間點初始化設定
            _emarkTime = new Date().getTime();
            // 隱藏會話
            $teacher.tooltip('hide');
            $teacher.attr('data-original-title', "");
            // 設置為等待執行狀態（避免遞歸時再次執行上述顯示或隱藏處理）
            _messageIsShowed = 2;
        }

        //console.log('e:' + (new Date().getTime() - _emarkTime));
        // 保持上述隱藏處理（延遲時間為默認的5秒鐘），5秒后執行後續處理
        if ((new Date().getTime() - _emarkTime) < _offSet) {
            _timeId = setTimeout(_dialogue, 1000);
            return;
        }

        // 會話指針
        _dialogueId++;
        // 回復顯示初始狀態（0表示沒有執行過顯示處理）
        _messageIsShowed = 0;
        _smarkTime = new Date().getTime();
        // 再次輪詢會話（情況1：會話結束 情況2：會話重新開始）
        _timeId = setTimeout(_dialogue, 1000);
    },

        /**
         * @description 開啟或者關閉虛擬人物
         * @method showMrTony
         * @param onoff {string} 顯示開關
         * @param isSpecial {bool} 是否顯示動畫特效
         */
        showMrTony = function (onoff, isSpecial = true) {
            _switch = onoff;

            if (_switch == 'off') {
                $teacher.tooltip('hide');
                $teacher.attr('data-original-title', '');

                if (isSpecial) {
                    // 關閉虛擬人物
                    $teacher.animate({
                        width: 0
                    }, "slow", "swing", function () { $teacher.hide(); });
                }
            } else {
                // 如果虛擬人物的會話已經結束，則不顯示人物
                if (_dialogueArray.length == 0) {
                    return;
                }
                // 人物顯示
                $teacher.show();
                if (isSpecial) {
                    // 自動播放
                    $teacher.animate({
                        width: _figureWidth
                    }, "slow", "swing", autoPlay(5000));
                }
            }
        },

        /**
         * @description 全局參數初期化設置
         * @private
         * @param setDialogueArrayFunc {string} 會話序列
         * @param dialogueId {string} 當前會話所在集合的位置
         * @param messageIsShowed {string} 會話是否已經顯示
         * @param isCirculation {string} 是否無限循環執行
         * @param callbackFunc {string} 循環事件執行后的回調函數定義對象
         */
        this._initParameter = function (setDialogueArrayFunc, dialogueId, messageIsShowed, isCirculation, callbackFunc) {
            // 會話序列
            _dialogueArray = setDialogueArrayFunc();
            // 當前會話所在集合的位置
            _dialogueId = dialogueId;
            // 會話是否已經顯示
            _messageIsShowed = messageIsShowed;
            // 是否無限循環執行
            _isCirculation = isCirculation;
            // 循環事件執行后的回調函數定義對象
            _callbackFunc = callbackFunc;
        },

        /**
         * @description 循環定時播放
         * @method autoPlay
         * @param delay {int} 執行間隔時間
         */
        autoPlay = function (delay) {
            // 虛擬人物被關閉，播放停止
            if (_switch == 'off') {
                if (_timeId != null) {
                    // 停止計時
                    clearTimeout(_timeId);
                }
                return;
            }

            // 執行間隔時間
            _offSet = delay;
            // 記錄會話開始的時間
            _smarkTime = new Date().getTime();
            _timeId = setTimeout(_dialogue, delay);
        },

        /**
         * @description 初期設定
         * @method initialize
         * @param setDialogueArrayFunc {Object} 自定義會話規則
         */
        initialize = function (setDialogueArrayFunc) {
            $teacher = $("#divShakeHead");
            $teacher.tooltip({ html: true });
            _figureWidth = parseInt($teacher.css('width'));

            // 參數初期化設置
            _initParameter(function () {
                _dialogueArray = [];
                if ((setDialogueArrayFunc != 'undefined' || setDialogueArrayFunc != null || setDialogueArrayFunc != '') && typeof (setDialogueArrayFunc) == 'function') {
                    setDialogueArrayFunc(_dialogueArray);
                } else {
                    _dialogueArray.push(String.format("<h3>{0}</h3>", RESOURCES.I0007));
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0008));
                }
                return _dialogueArray;
            }, 0, false, true, null);
            // 自動播放
            autoPlay(5000);
        },

        /**
         * @description 準備操作已經完成
         * @method readyComplete
         * @param setDialogueArrayFunc {Object} 自定義會話規則
         */
        readyComplete = function (setDialogueArrayFunc) {
            // 寬度設置（避免題目被虛擬人物遮擋）
            $teacher.width(300);
            $teacher.children(":first").width(300);

            // 將當前的會話隱藏
            $teacher.tooltip('hide');
            // 參數初期化設置
            _initParameter(function () {
                _dialogueArray = [];
                if ((setDialogueArrayFunc != 'undefined' || setDialogueArrayFunc != null || setDialogueArrayFunc != '') && typeof (setDialogueArrayFunc) == 'function') {
                    setDialogueArrayFunc(_dialogueArray);
                } else {
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0004));
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0005));
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0006));
                }
                return _dialogueArray;
            }, 0, false, false, null);
            // 自動播放
            autoPlay(5000);
        },

        /**
         * @description 交卷操作已經完成
         * @method theirPapersComplete
         * @param score {int} 執行間隔時間
         * @param setDialogueArrayFunc {Object} 自定義會話規則
         */
        theirPapersComplete = function (score, setDialogueArrayFunc) {
            _dialogueArray = [];

            if ((setDialogueArrayFunc != 'undefined' || setDialogueArrayFunc != null || setDialogueArrayFunc != '') && typeof (setDialogueArrayFunc) == 'function') {
                setDialogueArrayFunc(_dialogueArray);
            } else {
                if (score >= 5 && score < 10) {
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0001));
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0002));
                } else if (score < 5) {
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0002));
                    _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0003));
                }
            }

            // 將當前的會話隱藏
            $teacher.tooltip('hide');
            // 參數初期化設置
            _initParameter(function () {
                return _dialogueArray;
            }, 0, false, true, null);
            // 自動播放
            autoPlay(5000);
        },

        /**
         * @description 設定會話列表
         * @method setDialogueArray
         * @param dialogue {string} 新設定的會話內容
         */
        setDialogueArray = function (dialogue) {
            _dialogueArray = [];
            _dialogueArray.push(dialogue);
        },

        /**
         * @description 幫助提示
         * @method help
         */
        help = function () {
            // 參數初期化設置
            _initParameter(function () {
                return _dialogueArray;
            }, 0, false, false, null);
            // 自動播放
            autoPlay(3000);
        },

        /**
         * @description 恭喜你,滿分過關
         * @method doCelebrate
         * @param setDialogueArrayFunc {Object} 自定義會話規則
         */
        doCelebrate = function (setDialogueArrayFunc) {
            // 將當前的會話隱藏
            $teacher.tooltip('hide');
            // 定義循環事件執行后的回調函數(1.向右躲閃 2.整體隱藏)
            var callbackFunc = function () {
                $teacher.animate({
                    width: 0
                }, "slow", "swing", function () { $teacher.hide(); })
            };

            // 循環事件執行并最後完成上述回調事件
            $teacher.animate({
                top: 150,
                right: 500
            }, 500, "easeOutQuint", function () {
                // 參數初期化設置
                _initParameter(function () {
                    _dialogueArray = [];
                    if ((setDialogueArrayFunc != 'undefined' || setDialogueArrayFunc != null || setDialogueArrayFunc != '') && typeof (setDialogueArrayFunc) == 'function') {
                        setDialogueArrayFunc(_dialogueArray);
                    } else {
                        _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0009));
                        _dialogueArray.push(String.format("<h5>{0}</h5>", RESOURCES.I0010));
                    }
                    return _dialogueArray;
                }, 0, false, false, callbackFunc);
                // 自動播放
                autoPlay(3000);
            });
        };

    return {
        // 初期化設定
        initialize: initialize,
        // 恭喜答對滿分過關
        doCelebrate: doCelebrate,
        // 準備操作已經完成
        readyComplete: readyComplete,
        // 自動播放
        autoPlay: autoPlay,
        // 虛擬人物開關
        showMrTony: showMrTony,
        // 交卷操作已經完成
        theirPapersComplete: theirPapersComplete,
        // 設定會話列表
        setDialogueArray: setDialogueArray,
        // 幫助提示
        help: help
    };
}());